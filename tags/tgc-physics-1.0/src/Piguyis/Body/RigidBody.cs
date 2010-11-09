using System;
using AlumnoEjemplos.Piguyis.TGCView;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Fisica;
using AlumnoEjemplos.Piguyis.Colisiones;
using TgcViewer.Utils.TgcSceneLoader;
using System.Drawing;

namespace AlumnoEjemplos.Piguyis.Body
{
    public class RigidBody : IRenderObject
    {
        
        #region Variables

        private readonly float _mass;
        private float _restitution = 1.0f;
        private Fuerza _fuersasInternas;
        private Fuerza _fuersasExternas = new Fuerza();
        private readonly TgcArrow _debugForce;
        private Vector3 _location;
        private Vector3 _velocity;
        private readonly TgcArrow _debugVelocity;
        private BoundingVolume _boundingVolume = new BoundingNullObject();
        private string _meshType;

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
        public RigidBody(Vector3 location, Vector3 velocity, float mass)
        {
            if (mass <= 0f)
            {
                throw new ArgumentException("mass cannot be zero");
            }

            this._location = location;
            this._velocity = velocity;
            this._mass = mass;
            this._debugVelocity = TgcArrow.FromDirection(location, velocity, Color.Green, Color.Green, 0.1f, new Vector2(0.6f, 1.2f));
            this._debugForce = TgcArrow.FromDirection(location, (this._fuersasInternas == null)? new Vector3():this._fuersasInternas.Vector, Color.Red, Color.Red, 0.1f, new Vector2(0.6f, 1.2f));
        }

        #endregion Constructor

        #region Getters y setters
        public string MeshType
        {
            get
            {
                return this._meshType;
            }
            set
            {
                this._meshType = value;
            }
        }

        public BoundingVolume BoundingVolume
        {
            get
            {
                return this._boundingVolume;
            }
            set
            {
                this._boundingVolume = value;
            }
        }
        
        public Vector3 Location
        {
            get
            {
                return this._location;
            }
            set
            {
                this._location = value;
                _boundingVolume.SetPosition(this._location);                
                this._debugVelocity.PStart = this._location;
                this._debugForce.PStart = this._location;
                this._debugForce.PEnd = this._location + ((this._fuersasInternas == null) ? new Vector3() : this._fuersasInternas.Vector);
            }
        }

        /// <summary>
        /// Retrieves the current velocity.
        /// </summary>
        public Vector3 Velocity
        {
            get
            {
                return this._velocity;
            }
            set
            {
                this._velocity = value;
                this._debugVelocity.PEnd = this._location + this._velocity;
            }
        }

        /// <summary>
        /// La aceleracion dependiendo de las fuersas.
        /// </summary>
        public Vector3 Aceleracion
        {
            get
            {
                if (this._fuersasInternas == null)// || fuersasExternas == null)
                {
                    return new Vector3();
                }//+ fuersasExternas TODO fuerzas externas!!!
                return (_fuersasInternas * this.InverseMass).Vector;
            }
        }

        public Fuerza FuersasInternas
        {
            get
            {
                return this._fuersasInternas;
            }
            set
            {
                this._fuersasInternas = value;
                this._debugForce.PEnd = this._location + ((this._fuersasInternas == null) ? new Vector3() : this._fuersasInternas.Vector);
            }
        }
        public Fuerza FuersasExternas
        {
            get
            {
                return this._fuersasExternas;
            }
            set
            {
                this._fuersasExternas = value;
            }
        }
        /// <summary>
        /// Returns the object's mass.
        /// </summary>
        public float Mass
        {
            get
            {
                return this._mass;
            }
        }
        
        /// <summary>
        /// Returns the inverse mass.
        /// </summary>
        public float InverseMass
        {
            get
            {
                return 1f/this._mass;
            }
        }

        /// <summary>
        /// The coefficient of restitution.
        /// o Factor de rebote.
        /// </summary>
        public float Restitution
        {
            get
            {
                return _restitution;
            }
            set
            {
                _restitution = value;
            }
        }

        #endregion Getters y setters

        #region Public Methods

        /// <summary>
        /// Allows the body to update its physical state.
        /// Ahora no lo uso.
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
            this.Velocity = Vector3.Add(this.Velocity, Vector3.Multiply(this.Aceleracion, deltaTime));
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
                                                    Vector3.Add(_velocity, BiasedVelocity), 
                                                    deltaTime));
            //TODO: rotacion
            //TODO: reset fuerza?
            //TODO: reset torque?
        }

        #endregion Public Methods
        
        #region IRenderObject Members

        public void render()
        {
            if ((bool)TgcViewer.GuiController.Instance.Modifiers.getValue("debugMode"))
            {
                this.BoundingVolume.render();
                this._debugVelocity.render();
                this._debugForce.render();
            }
            else
            {
                MeshPool.Instance.GetMeshToRender(this._meshType, this._location, this.BoundingVolume.GetRadius()).render();
            }            
        }

        public void dispose()
        {
            this.BoundingVolume.dispose();
            this._debugVelocity.dispose();
            this._debugForce.render();
        }

        #endregion
    }
}
