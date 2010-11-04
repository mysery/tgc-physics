using System;
using System.Collections.Generic;
using AlumnoEjemplos.PiguYis.Matematica;
using AlumnoEjemplos.Piguyis.Body;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Box2DLitePort;
using TgcViewer;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public abstract class EscenaBase : IEscena
    {
        protected List<RigidBody> Bodys;
        protected World World;

        #region Implementacion IEsena
        
        public abstract String GetTitle();
        public abstract String GetDescription();
        
        public virtual void InitEscena()
        {
            Bodys = new List<RigidBody>();
            this.CreateBodys();
            this.World = new World(Bodys);

            GuiController.Instance.FpsCamera.Enable = true;
            GuiController.Instance.FpsCamera.MovementSpeed = 100f;
            GuiController.Instance.FpsCamera.JumpSpeed = 100f;
            GuiController.Instance.FpsCamera.setCamera(new Vector3(0.0f, 75.0f, -200.0f), new Vector3(0.0f, -30.0f, 100.0f));

            GuiController.Instance.Modifiers.addBoolean("debugMode", "Debug Mode", false);
            GuiController.Instance.Modifiers.addBoolean("colisionDetect", "Deteccion de colisiones", true);
            GuiController.Instance.Modifiers.addBoolean("applyPhysics", "Calculos fisicos", true);
        }

        protected abstract void CreateBodys();

        public virtual void Render(float elapsedTime)
        {
            this.World.Step(elapsedTime);

            foreach (RigidBody body in Bodys)
            {
                body.render();
            }
        }

        public virtual void CloseEscena()
        {
            foreach (RigidBody body in Bodys)
            {
                body.dispose();
            }
        }
        #endregion Implementacion IEsena

        protected RigidBody AddBody(Density density, Vector3 initialLocation, Vector3 initialVelocity, float radius)
        {
            float densityValue = (int)density;
            
            float mass = densityValue * (1.33333f) * FastMath.PI * (radius * radius * radius);

            // sphere 2.
            BodyBuilder builder = new BodyBuilder(initialLocation, initialVelocity, mass);
            builder.SetBoundingSphere(radius);
            RigidBody result = builder.Build();
            Bodys.Add(result);
            return result;
        }
            
        public enum Density
        {
            Low = 5,
            Medium = 20,
            High = 250,
            Heavy = 200
        }
    }
}
