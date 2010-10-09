using System;
using System.Collections.Generic;
using System.Text;
using AlumnoEjemplos.Piguyis.Colisiones;
using AlumnoEjemplos.Piguyis.Fisica;
using Microsoft.DirectX;

namespace AlumnoEjemplos.Piguyis.Body
{
    public class BodyFactory
    {
        private RigidBody rigidBody;
        private BoundingVolume bounding;
        private Fuerza forces;
        private const float DEFAULT_MASS = 1f;

        public BodyFactory()
        {
            rigidBody = new RigidBody(new Vector3(), new Vector3(), DEFAULT_MASS);
        }

        public BodyFactory(Vector3 initPosition, Vector3 initVelocity, float mass)
        {
            rigidBody = new RigidBody(initVelocity, initVelocity, mass);
        }

        public void setForces(Vector3 v)
        {
            forces = new Fuerza(v);
        }

        public void setBoundingSphere(float radius)
        {
            bounding = new BoundingSphere(this.rigidBody, radius);
        }

        public RigidBody build()
        {
            //sphereLeft = new BoundingSphere(rigidBody, radius);
            rigidBody.FuersasInternas = forces;
            return rigidBody;
        }
    }
}
