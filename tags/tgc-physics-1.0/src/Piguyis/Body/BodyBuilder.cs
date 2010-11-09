using System;
using AlumnoEjemplos.Piguyis.Colisiones;
using AlumnoEjemplos.Piguyis.Fisica;
using Microsoft.DirectX;

namespace AlumnoEjemplos.Piguyis.Body
{
    public class BodyBuilder
    {
        private BoundingVolume _bounding;
        private Vector3 _position;
        private Vector3 _velocity;
        private readonly float _mass;
        private float _restitution = DefaultRestitution;
        private Fuerza _forces;
        private const float DefaultMass = 1f;
        private const float DefaultRestitution = 1f;
        private const float DefaultRadius = 5f;
        private string _meshType;

        public BodyBuilder()
        {
            this._position = new Vector3();
            this._velocity = new Vector3();
            this.SetBoundingSphere(DefaultRadius);
            this._mass = DefaultMass;
        }

        public BodyBuilder(Vector3 initPosition, Vector3 initVelocity, float mass)
        {
            this._position = initPosition;
            this._velocity = initVelocity;
            if (mass < 0.0f)
            {
                throw new ArgumentException(@"Mass should not be negative", "mass");
            }
            this._mass = mass;            
        }

        public void SetForces(Vector3 v)
        {
            this._forces = new Fuerza(v);
        }
        public void SetForces(float x, float y, float z)
        {
            this._forces = new Fuerza(x, y, z);
        }

        public void SetPosition(Vector3 pos)
        {
            this._position = pos;
        }

        public void SetVelocity(Vector3 vel)
        {
            this._velocity = vel;
        }
        /// <summary>
        /// Asigna un volumen contenedor esferico al cuerpo.
        /// //TODO asignacion automatica es posible con analisis de mesh, y aca haria dicho analisis.
        /// //REFACTOR to setBoundingVolume()
        /// </summary>
        /// <param name="radius"></param>
        public void SetBoundingSphere(float radius)
        {
            this._bounding = new BoundingSphere(radius);
            _meshType = MeshPool.ShpereType;            
        }

        public void SetBoundingSphere(float radius, string meshType)
        {
            this._bounding = new BoundingSphere(radius);
            _meshType = meshType;
        }

        public RigidBody Build()
        {
            RigidBody rigidBody = new RigidBody(_position, _velocity, _mass);
            rigidBody.BoundingVolume = _bounding;
            rigidBody.BoundingVolume.SetPosition(_position);
            rigidBody.FuersasInternas = _forces;
            rigidBody.Restitution = _restitution;
            rigidBody.MeshType = _meshType;
            return rigidBody;
        }

        public void SetRestitution(float res)
        {
            this._restitution = res;
        }

        internal void SetBoundingPlane(BoundingPlane.Orientations o)
        {
            this._bounding = new BoundingPlane(o);
            if (o.Equals(BoundingPlane.Orientations.XYplane))
                this._meshType = MeshPool.PlaneXYType;
            else if (o.Equals(BoundingPlane.Orientations.XZplane))
                this._meshType = MeshPool.PlaneXZType;
            else if (o.Equals(BoundingPlane.Orientations.YZplane))
                this._meshType = MeshPool.PlaneYZType;
        }
    }
}
