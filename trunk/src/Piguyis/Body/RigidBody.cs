using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Fisica;
using TgcViewer.Utils.TgcGeometry;
using AlumnoEjemplos.Piguyis.Colisiones;

namespace AlumnoEjemplos.Piguyis.Body
{
    public class RigidBody
    {
        
        #region Variables

        private float mass;
        private float restitution = 1.0f;
        private Fuerza fuersasInternas;
        private Fuerza fuersasExternas = new Fuerza();
        private Vector3 location = new Vector3();
        private Vector3 velocity = new Vector3();
        private BoundingVolume boundingVolume = new BoundingNullObject();
        /// <summary>
        /// The biased velocity (velocidad parcial) - see the Box2D Port classes.
        /// TODO ver aplicacion.
        /// </summary>
        public Vector3 BiasedVelocity;

        #endregion Member Variables

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="location">posicion inical</param>
        /// <param name="velocity">velocidad inicial</param>
        /// <param name="mass">masa</param>
        public RigidBody(Vector3 location, Vector3 velocity, float masa)
        {
            if (masa == 0.0)
            {
                throw new ArgumentException("mass cannot be zero");
            }

            this.location = location;
            this.velocity = velocity;
            this.mass = masa;
        }

        #endregion Constructor

        #region Getters y setters

        public BoundingVolume BoundingVolume
        {
            get
            {
                return this.boundingVolume;
            }
            set
            {
                this.boundingVolume = value;
            }
        }

        public Vector3 Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
                boundingVolume.setPosition(this.location);
            }
        }

        /// <summary>
        /// Retrieves the current velocity.
        /// </summary>
        public Vector3 Velocity
        {
            get
            {
                return this.velocity;
            }
            set
            {
                this.velocity = value;
            }
        }

        /// <summary>
        /// La aceleracion dependiendo de las fuersas.
        /// </summary>
        public Vector3 Aceleracion
        {
            get
            {
                if (this.fuersasInternas == null)// || fuersasExternas == null)
                {
                    return new Vector3();
                }//+ fuersasExternas TODO fuerzas externas!!!
                return (fuersasInternas * (1.0 / mass)).Vector;
            }
        }

        public Fuerza FuersasInternas
        {
            get
            {
                return this.fuersasInternas;
            }
            set
            {
                this.fuersasInternas = value;
            }
        }
        public Fuerza FuersasExternas
        {
            get
            {
                return this.fuersasExternas;
            }
            set
            {
                this.fuersasExternas = value;
            }
        }
        /// <summary>
        /// Returns the object's mass.
        /// </summary>
        public float Mass
        {
            get
            {
                return this.mass;
            }
        }
        
        /// <summary>
        /// Returns the inverse mass.
        /// </summary>
        public float inverseMass
        {
            get
            {
                return 1f/this.mass;
            }
        }

        /// <summary>
        /// The coefficient of restitution.
        /// </summary>
        public float Restitution
        {
            get
            {
                return restitution;
            }
            set
            {
                restitution = value;
            }
        }

        #endregion Getters y setters

        #region Public Methods

        /// <summary>
        /// Allows the body to update its physical state.
        /// </summary>
        /// <param name="deltaTime">Time increment, in seconds.</param>
        public void Update(float deltaTime)
        {
            this.Velocity = this.Velocity + (this.Aceleracion * deltaTime);
            this.Location = this.Location + (this.Velocity * deltaTime);
        }

        /// <summary>
        /// Integrates acceleration.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void IntegrateForceSI(float deltaTime)
        {
            this.Velocity = Vector3.Add(this.Velocity, Vector3.Multiply(this.Aceleracion, (float)deltaTime));
            // TODO: angular velocity

            //Biased velocities are reset to zero each step.            
            BiasedVelocity = new Vector3();
        }

        /// <summary>
        /// Integrates velocity.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void IntegrateVelocitySI(float deltaTime)
        {
            this.Location = Vector3.Add(this.Location,
                                        Vector3.Multiply(
                                                    Vector3.Add(velocity, BiasedVelocity), 
                                                    (float)deltaTime));
            //TODO: rotacion
            //TODO: reset fuerza?
            //TODO: reset torque?
        }

        #endregion Public Methods
    }
}
