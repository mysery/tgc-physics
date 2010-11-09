using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;
using TgcViewer;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena08 : EscenaBase
    {
        public override void Render(float elapsedTime)
        {
            base.Render(elapsedTime * 5f);
        }

        protected override void CreateBodys()
        {
            #region Spheres moviles

            // creo una grilla de cuerpos.
            int numberSpheresPerSide = 10;
            const float radius = 5.0f;
            float separationBetweenSpheres = 2.0f;

            const float xCentre = 0.0f;
            const float zCentre = -120.0f;
            float yLocation = 40.0f;

            float initialX = xCentre - (((numberSpheresPerSide - 1) * ((radius * 2.0f) + separationBetweenSpheres)) / 2.0f) - (separationBetweenSpheres / 2.0f);
            float initialZ = zCentre - (((numberSpheresPerSide - 1) * ((radius * 2.0f) + separationBetweenSpheres)) / 2.0f) - (separationBetweenSpheres / 2.0f);

            for (int x = 0; x < numberSpheresPerSide; ++x)
            {
                for (int z = 0; z < numberSpheresPerSide; ++z)
                {
                    BodyBuilder builder = new BodyBuilder(
                                                        new Vector3(initialX + (x * ((radius * 2) + separationBetweenSpheres)),
                                                                    yLocation,
                                                                    initialZ + (z * ((radius * 2) + separationBetweenSpheres))),
                                                        new Vector3(),
                                                        5.0f);
                    builder.SetBoundingSphere(radius);
                    builder.SetForces(0.0f, -1.0f, 0.0f);
                    Bodys.Add(builder.Build());
                }
            }

            #endregion

            #region Spheres inmoviles

            yLocation = 20f;
            numberSpheresPerSide = 5;
            separationBetweenSpheres = 10f;

            initialX = xCentre - (((numberSpheresPerSide - 1) * ((radius * 2.0f) + separationBetweenSpheres)) / 2.0f) - (separationBetweenSpheres / 2.0f);
            initialZ = zCentre - (((numberSpheresPerSide - 1) * ((radius * 2.0f) + separationBetweenSpheres)) / 2.0f) - (separationBetweenSpheres / 2.0f);

            for (int x = 0; x < numberSpheresPerSide; ++x)
            {
                for (int z = 0; z < numberSpheresPerSide; ++z)
                {
                    BodyBuilder builder = new BodyBuilder(
                                                        new Vector3(initialX + (x * ((radius * 2) + separationBetweenSpheres)),
                                                                    yLocation,
                                                                    initialZ + (z * ((radius * 2) + separationBetweenSpheres))),
                                                        new Vector3(),
                                                        float.PositiveInfinity);
                    builder.SetBoundingSphere(radius);
                    Bodys.Add(builder.Build());
                }
            }

            #endregion

            #region BigSphere

            const float radiusBigYLocation = 10f;
            const float radiusBig = 100.0f;
            BodyBuilder bigBuilder = new BodyBuilder(new Vector3(xCentre,
                                                                    -radiusBig + radiusBigYLocation,
                                                                    zCentre),
                                                        new Vector3(),
                                                        float.PositiveInfinity);
            bigBuilder.SetBoundingSphere(radiusBig);
            Bodys.Add(bigBuilder.Build());

            #endregion
        }

        public override void InitEscena()
        {
            base.InitEscena();
            GuiController.Instance.FpsCamera.setCamera(new Vector3(0.0f, 75.0f, -650.0f), new Vector3(0.0f, -30.0f, 100.0f));
        }

        public override string GetTitle()
        {
            return "Escena08 - Motor Fisica";
        }

        public override string GetDescription()
        {
            return "Una gran esfera y una grilla de esferas inmoviles, mientras que otra grilla de esferas caen entre las mismas.";
        }
    }
}
