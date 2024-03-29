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
        
        protected override void CreateBodys()
        {
            const float radius = 20.0f;

            // sphere 1.
            BodyBuilder builderLeft = new BodyBuilder(new Vector3(-radius * 2, 0.0f, 0.0f),
                                                    new Vector3(10.0f, 0.0f, 0.0f), 1.0f);
            builderLeft.SetBoundingSphere(radius);            
            Bodys.Add(builderLeft.Build());

            // sphere 2.
            BodyBuilder builderRight = new BodyBuilder(new Vector3(radius * 2, 0.0f, 0.0f),
                                                    new Vector3(-10.0f, 0.0f, 0.0f), 1.0f);
            builderRight.SetBoundingSphere(radius);
            Bodys.Add(builderRight.Build());
        }

        public override string GetTitle()
        {
            return "Escena01 - Motor Fisica";
        }

        public override string GetDescription()
        {
            return "Esferas de velocidad constante chocan.";
        }
    }
}
