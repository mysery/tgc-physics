using System;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;
using TgcViewer;
using AlumnoEjemplos.PiguYis.Matematica;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena10 : EscenaBase
    {
        protected override void CreateBodys()
        {
            #region BigSphere
            const float radiusBigYLocation = 10f;
            const float radiusBig = 100.0f;
            BodyBuilder bigBuilder = new BodyBuilder(new Vector3(0f,
                                                                 -radiusBig + radiusBigYLocation,
                                                                 0f),
                                                        new Vector3(),
                                                        float.PositiveInfinity);
            bigBuilder.SetBoundingSphere(radiusBig);
            Bodys.Add(bigBuilder.Build());

            #endregion
        }
        private readonly Random _random = new Random();
        private int _pos;
        public override void Render(float elapsedTime)
        {
            Boolean newBody = GuiController.Instance.D3dInput.buttonUp(TgcViewer.Utils.Input.TgcD3dInput.MouseButtons.BUTTON_RIGHT);
            if (newBody)
            {
                BodyBuilder builder = new BodyBuilder();
                builder.SetPosition(new Vector3((FastMath.Cos(_pos++/30f * FastMath.PI) * 50f), 
                                                60f,
                                                (50f * FastMath.Sin(_pos++/30f * FastMath.PI))));
                builder.SetForces(0f, (float)_random.NextDouble() * -10f, 0f);
                this.World.AddBody(builder.Build());
                if (_pos > 60)
                    _pos = 0;

            }

            base.Render(elapsedTime * 1.25f);
        }

        public override void InitEscena()
        {
            base.InitEscena();
            GuiController.Instance.FpsCamera.setCamera(new Vector3(0.0f, 75.0f, -650.0f), new Vector3(0.0f, -30.0f, 100.0f));
        }

        public override string GetTitle()
        {
            return "Escena10 - Motor Fisica";
        }

        public override string GetDescription()
        {
            return "Una gran esfera inmovil. se le agregan esferas con CLIC DERECHO.";
        }
    }
}
