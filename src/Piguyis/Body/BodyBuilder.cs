using System;
using System.Collections.Generic;
using System.Text;
using AlumnoEjemplos.Piguyis.Colisiones;
using AlumnoEjemplos.Piguyis.Fisica;
using Microsoft.DirectX;

namespace AlumnoEjemplos.Piguyis.Body
{
    public class BodyBuilder
    {
        private BoundingVolume bounding;
        private Vector3 position;
        private Vector3 velocity;
        private float mass;
        private float restitution = DEFAULT_RESTITUTION;
        private Fuerza forces;
        private const float DEFAULT_MASS = 1f;
        private const float DEFAULT_RESTITUTION = 1f;

        public BodyBuilder()
        {
            this.position = new Vector3();
            this.velocity = new Vector3();
            this.mass = DEFAULT_MASS;
        }

        public BodyBuilder(Vector3 initPosition, Vector3 initVelocity, float mass)
        {
            this.position = initPosition;
            this.velocity = initVelocity;
            if (mass < 0.0f)
            {
                throw new ArgumentException("Mass should not be negative", "mass");
            }
            this.mass = mass;
        }

        public void setForces(Vector3 v)
        {
            this.forces = new Fuerza(v);
        }
        public void setForces(float x, float y, float z)
        {
            this.forces = new Fuerza(x, y, z);
        }

        public void setPosition(Vector3 pos)
        {
            this.position = pos;
        }

        public void setVelocity(Vector3 vel)
        {
            this.velocity = vel;
        }
        /// <summary>
        /// Asigna un volumen contenedor esferico al cuerpo.
        /// //TODO asignacion automatica es posible con analisis de mesh, y aca haria dicho analisis.
        /// //REFACTOR to setBoundingVolume()
        /// </summary>
        /// <param name="radius"></param>
        public void setBoundingSphere(float radius)
        {
            this.bounding = new BoundingSphere(radius);
        }

        public RigidBody build()
        {
            RigidBody rigidBody = new RigidBody(position, velocity, mass);
            rigidBody.BoundingVolume = bounding;
            rigidBody.FuersasInternas = forces;
            rigidBody.Restitution = restitution;
            return rigidBody;
        }

        internal void setRestitution(float res)
        {
            this.restitution = res;
        }
    }
}
