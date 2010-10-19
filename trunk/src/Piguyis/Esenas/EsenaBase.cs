using System;
using System.Collections.Generic;
using System.Text;
using AlumnoEjemplos.Piguyis.Colisiones;
using AlumnoEjemplos.PiguYis.Matematica;
using AlumnoEjemplos.Piguyis.Body;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Box2DLitePort;
using TgcViewer;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public abstract class EsenaBase : IEsena
    {
        protected List<RigidBody> bodys;
        protected World world = null;

        #region Implementacion IEsena
        public virtual void initEsena()
        {
            bodys = new List<RigidBody>();
            this.createBodys();
            this.world = new World(bodys);

            GuiController.Instance.FpsCamera.Enable = true;
            GuiController.Instance.FpsCamera.MovementSpeed = 100f;
            GuiController.Instance.FpsCamera.JumpSpeed = 100f;
            GuiController.Instance.FpsCamera.setCamera(new Vector3(0.0f, 75.0f, -200.0f), new Vector3(0.0f, -30.0f, 100.0f));
        }

        protected abstract void createBodys();

        public virtual void render(float elapsedTime)
        {
            this.world.Step(elapsedTime);

            foreach (RigidBody body in bodys)
            {
                body.render();
            }
        }

        public virtual void closeEsena()
        {
            foreach (RigidBody body in bodys)
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
            builder.setBoundingSphere(radius);
            RigidBody result = builder.build();
            bodys.Add(result);
            return result;
        }
            
        public enum Density
        {
            Low = 1,
            Medium = 2,
            High = 3
        }
    }
}
