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
    public class Esena10 : EsenaBase
    {
        protected override void createBodys()
        {
            #region BigSphere
            float radiusBigYLocation = 10f; // the y offset for the big sphere
            float radiusBig = 100.0f;
            BodyBuilder bigBuilder = new BodyBuilder(new Vector3(0f,
                                                                 -radiusBig + radiusBigYLocation,
                                                                 0f),
                                                        new Vector3(),
                                                        float.PositiveInfinity);
            bigBuilder.setBoundingSphere(radiusBig);
            bodys.Add(bigBuilder.build());

            #endregion
        }
        private Random random = new Random();
        public override void render(float elapsedTime)
        {
            Boolean newBody = GuiController.Instance.D3dInput.buttonUp(TgcViewer.Utils.Input.TgcD3dInput.MouseButtons.BUTTON_RIGHT);
            if (newBody)
            {
                BodyBuilder builder = new BodyBuilder();
                builder.setPosition(new Vector3((float)(50f*Math.Pow(-1,random.Next(1,3))), 
                                                (float)(random.NextDouble() * 50f) + 10f,
                                                (float)(50f * Math.Pow(-1,random.Next(1, 3)))));
                builder.setForces(0f, (float)random.NextDouble() * -10f, 0f);
                this.world.addBody(builder.build());
            }

            base.render(elapsedTime);
        }
    }
}
