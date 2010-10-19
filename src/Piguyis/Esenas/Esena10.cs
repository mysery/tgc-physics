using System;
using System.Collections.Generic;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;
using AlumnoEjemplos.Piguyis.Colisiones;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;
using AlumnoEjemplos.Piguyis.Fisica;
using TgcViewer;
using AlumnoEjemplos.PiguYis.Matematica;

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
        private int pos = 0;
        public override void render(float elapsedTime)
        {
            Boolean newBody = GuiController.Instance.D3dInput.buttonUp(TgcViewer.Utils.Input.TgcD3dInput.MouseButtons.BUTTON_RIGHT);
            if (newBody)
            {
                BodyBuilder builder = new BodyBuilder();
                builder.setPosition(new Vector3((FastMath.Cos(pos++/30f * FastMath.PI) * 50f), 
                                                60f,
                                                (50f * FastMath.Sin(pos++/30f * FastMath.PI))));
                builder.setForces(0f, (float)random.NextDouble() * -10f, 0f);
                this.world.addBody(builder.build());
                if (pos > 60)
                    pos = 0;

            }

            base.render(elapsedTime);
        }

        public override void initEsena()
        {
            base.initEsena();
            GuiController.Instance.FpsCamera.setCamera(new Vector3(0.0f, 75.0f, -650.0f), new Vector3(0.0f, -30.0f, 100.0f));
        }
    }
}
