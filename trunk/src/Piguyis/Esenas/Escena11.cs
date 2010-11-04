using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;
using TgcViewer;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena11 : EscenaBase
    {
        public override void Render(float elapsedTime)
        {
            base.Render(elapsedTime * 2f);
        }
        protected override void CreateBodys()
        {
            const float radius = 2.0f;
            const float separationDistance = 12.0f;
            const int numberOfSpheresPerBaseLayer = 7;
            const float initialYLocation = -50.0f;
            const float zOffset = -10f;

            for (int y = 0; y < numberOfSpheresPerBaseLayer; ++y)
            {
                for (int x = 0; x < numberOfSpheresPerBaseLayer - y; ++x)
                {
                    for (int z = 0; z < numberOfSpheresPerBaseLayer - y; ++z)
                    {
                        BodyBuilder builder = new BodyBuilder(
                                                            new Vector3((radius * 2f * x) + (y * radius),
                                                                        initialYLocation + (separationDistance * y),
                                                                        zOffset + (radius * 2 * z) + (y * radius)),
                                                            new Vector3(),
                                                            y != 0 ? 1.0f : float.PositiveInfinity);
                        builder.SetBoundingSphere(radius);
                        if (y != 0)
                            builder.SetForces(0.0f, -1.0f, 0.0f);
                        Bodys.Add(builder.Build());
                    }
                }
            }
        }

        public override void InitEscena()
        {
            base.InitEscena();
            GuiController.Instance.FpsCamera.setCamera(new Vector3(-50.0f, 25.0f, -100.0f), new Vector3(90.0f, -50.0f, 100.0f));
        }

        public override string GetTitle()
        {
            return "Escena11 - Motor Fisica";
        }

        public override string GetDescription()
        {
            return "Muchas esferas apiladas en forma de piramide caen sobre esferas inmoviles. con restitucion 1.0";
        }
    }
}
