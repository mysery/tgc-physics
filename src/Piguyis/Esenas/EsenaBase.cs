using System;
using System.Collections.Generic;
using System.Text;
using AlumnoEjemplos.Piguyis.Colisiones;
using AlumnoEjemplos.PiguYis.Matematica;
using AlumnoEjemplos.Piguyis.Body;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Box2DLitePort;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public abstract class EsenaBase : IEsena
    {
        protected List<RigidBody> bodys;
        protected World world = null;

        #region Implementacion IEsena
        public void initEsena()
        {
            bodys = new List<RigidBody>();
            this.createBodys();
            this.world = new World(bodys);
        }

        protected abstract void createBodys();

        public void render(float elapsedTime)
        {
            this.world.Step(elapsedTime);

            foreach (RigidBody body in bodys)
            {
                body.BoundingVolume.render();
            }
        }

        public void closeEsena()
        {
            foreach (RigidBody body in bodys)
            {
                body.BoundingVolume.dispose();
            }
        }
        #endregion Implementacion IEsena

        protected BoundingSphere AddBody(Density density, Vector3 initialLocation, Vector3 initialVelocity, float radius)
        {
            float densityValue = (int)density;
            
            float mass = densityValue * (1.33333f) * FastMath.PI * (radius * radius * radius);

            // sphere 2.
            RigidBody rigidBody = new RigidBody(initialLocation, initialVelocity, mass);
            BoundingSphere sphere = new BoundingSphere(rigidBody, radius);
            bodys.Add(rigidBody);
            return sphere;
        }
            
        public enum Density
        {
            Low = 1,
            Medium = 2,
            High = 3
        }
    }
}
