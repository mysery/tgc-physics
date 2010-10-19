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
    public class Escena01 : EscenaBase
    {
        protected override void createBodys()
        {
            const float radius = 20.0f;

            // sphere 1.
            BodyBuilder builderLeft = new BodyBuilder(new Vector3(-radius * 2, 0.0f, 0.0f),
                                                    new Vector3(), 1.0f);
            builderLeft.setBoundingSphere(radius);
            builderLeft.setForces(10.0f, 0.0f, 0.0f);
            bodys.Add(builderLeft.build());

            // sphere 2.
            BodyBuilder builderRight = new BodyBuilder(new Vector3(radius * 2, 0.0f, 0.0f),
                                                    new Vector3(), 1.0f);
            builderRight.setBoundingSphere(radius);
            builderRight.setForces(-10.0f, 0.0f, 0.0f);
            bodys.Add(builderRight.build());
            
        }
    }
}
