using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena05 : EscenaBase
    {
        protected override void CreateBodys()
        {
            const float radius = 20.0f;

            // sphere 1.
            BodyBuilder builderLeft = new BodyBuilder(new Vector3(-radius * 2, 0.0f, 0.0f),
                                                    new Vector3(), 1.0f);
            builderLeft.SetBoundingSphere(radius);
            builderLeft.SetForces(10.0f, 0.0f, 0.0f);
            Bodys.Add(builderLeft.Build());

            // sphere 2.
            BodyBuilder builderRight = new BodyBuilder(new Vector3(radius * 2, 0.0f, 0.0f),
                                                    new Vector3(), 1.0f);
            builderRight.SetBoundingSphere(radius);
            builderRight.SetForces(-10.0f, 0.0f, 0.0f);
            Bodys.Add(builderRight.Build());
        }

        public override string GetTitle()
        {
            return "Escena05 - Motor Fisica";
        }

        public override string GetDescription()
        {
            return "Dos esferas con aceleracion constante (fuerza constante e igual) de la misma masa chocan linealmente y rebotan segun el impulso";
        }
    }
}
