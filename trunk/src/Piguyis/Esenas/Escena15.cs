using AlumnoEjemplos.Piguyis.Colisiones;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena15 : EscenaBase
    {
        public override string GetTitle()
        {
            return "Escena15 - Motor Fisica";
        }

        public override string GetDescription()
        {
            return "Esfera contra plano";
        }

        protected override void CreateBodys()
        {
            const float radius = 20.0f;

            // sphere 1.
            BodyBuilder builderLeft = new BodyBuilder(new Vector3(-radius * 2, radius * 3, 0.0f),
                                                    new Vector3(), 1.0f);
            builderLeft.SetForces(10f, 0f, 0f);
            builderLeft.SetBoundingSphere(radius);
            Bodys.Add(builderLeft.Build());

            // sphere 2.
            BodyBuilder builder2 = new BodyBuilder(new Vector3(-radius * 2, 0.0f, 0.0f),
                                                    new Vector3(10.0f, 0.0f, 0.0f), 1.0f);
            builder2.SetBoundingSphere(radius);
            Bodys.Add(builder2.Build());
            
            // sphere 3.
            BodyBuilder builder3 = new BodyBuilder(new Vector3(-radius * 2, -radius * 3, 0.0f),
                                                    new Vector3(10.0f, -5.0f, 0.0f), 1.0f);
            builder3.SetBoundingSphere(radius);
            Bodys.Add(builder3.Build());

            //plane
            BodyBuilder builderRight = new BodyBuilder(new Vector3(radius * 2, 0.0f, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builderRight.SetBoundingPlane(BoundingPlane.Orientations.YZplane);
            Bodys.Add(builderRight.Build());
        }
    }
}
