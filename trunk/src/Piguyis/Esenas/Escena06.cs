using System;
using System.Collections.Generic;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;
using AlumnoEjemplos.Piguyis.Colisiones;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;
using AlumnoEjemplos.Piguyis.Fisica;
using TgcViewer;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena06 : EscenaBase
    {
        protected override void createBodys()
        {
            const float radius = 20.0f;

            // sphere 1.
            BodyBuilder builderLeft = new BodyBuilder(new Vector3(-radius * 2, 0.0f, 0.0f),
                                                    new Vector3(), 10.0f);
            builderLeft.SetBoundingSphere(radius);
            builderLeft.SetForces(100.0f, 0.0f, 0.0f);
            bodys.Add(builderLeft.Build());

            // sphere 2.
            BodyBuilder builderRight = new BodyBuilder(new Vector3(radius * 2, 0.0f, 0.0f),
                                                    new Vector3(), 1.0f);
            builderRight.SetBoundingSphere(radius);
            builderRight.SetForces(-10.0f, 0.0f, 0.0f);
            bodys.Add(builderRight.Build());
        }

        public override string getTitle()
        {
            return "Escena06 - Motor Fisica";
        }

        public override string getDescription()
        {
            return "Dos esferas con aceleracion constante (fuerza constante pero DISTINTA) de DISTINTA masa chocan linealmente y rebotan segun el impulso";
        }
    }
}
