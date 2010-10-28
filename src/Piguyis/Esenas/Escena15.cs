using System;
using System.Collections.Generic;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;
using AlumnoEjemplos.Piguyis.Colisiones;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;
using AlumnoEjemplos.Piguyis.Fisica;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena15 : EscenaBase
    {
        public override string getTitle()
        {
            return "Escena15 - Motor Fisica";
        }

        public override string getDescription()
        {
            return "Esfera contra plano";
        }

        protected override void createBodys()
        {
            const float radius = 20.0f;

            // sphere 1.
            BodyBuilder builderLeft = new BodyBuilder(new Vector3(-radius * 2, radius * 3, 0.0f),
                                                    new Vector3(), 1.0f);
            builderLeft.setForces(10f, 0f, 0f);
            builderLeft.setBoundingSphere(radius);
            bodys.Add(builderLeft.build());

            // sphere 2.
            BodyBuilder builder2 = new BodyBuilder(new Vector3(-radius * 2, 0.0f, 0.0f),
                                                    new Vector3(10.0f, 0.0f, 0.0f), 1.0f);
            builder2.setBoundingSphere(radius);
            bodys.Add(builder2.build());

            // sphere 3.
            BodyBuilder builder3 = new BodyBuilder(new Vector3(-radius * 2, -radius * 3, 0.0f),
                                                    new Vector3(10.0f, -5.0f, 0.0f), 1.0f);
            builder3.setBoundingSphere(radius);
            bodys.Add(builder3.build());

            //plane
            BodyBuilder builderRight = new BodyBuilder(new Vector3(radius * 2, 0.0f, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builderRight.setBoundingPlane(BoundingPlane.Orientations.YZplane);
            bodys.Add(builderRight.build());
        }
    }
}
