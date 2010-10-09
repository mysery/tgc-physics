// Copyright Gary Evans 2006-2008

using System;
using System.Collections.Generic;
using System.Text;
using AlumnoEjemplos.Piguyis.Body;
using Microsoft.DirectX;
using AlumnoEjemplos.PiguYis.Matematica;
using TgcViewer;
using Microsoft.DirectX.Direct3D;
using AlumnoEjemplos.Piguyis.TGCView;


namespace AlumnoEjemplos.Piguyis.Colisiones
{
    /// <summary>
    /// Represents a Sphere class, for Collision detection.
    /// </summary>
    public class BoundingSphere : BoundingVolume
    {
        #region Object Lifetime

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rigidBody">Holds the sphere's location and velocity.</param>
        /// <param name="radius">The sphere's radius</param>
        public BoundingSphere(RigidBody rigidBody, float radius)
        {
            if (rigidBody == null)
            {
                throw new ArgumentNullException("rigidBody");
            }
            if (radius < 0.0)
            {
                throw new ArgumentException("Radius should not be negative", "radius");
            }
            this.rigidBody = rigidBody;            
            sphare = new TgcBoundingSphere(rigidBody.Location, radius);
            //TODO proximo refacor
            this.rigidBody.BoundingVolume = this;
        }

        #endregion Object Lifetime

		#region get y sets

		/// <summary>
		/// Returns the EulerRigidBody instance
		///  that contains details on the object's physical state.
		/// </summary>
        public RigidBody RigidBody
		{
			get
			{
				return this.rigidBody;
			}
		}

        /// <summary>
        /// Returns the Sphere's radius.
        /// </summary>
        public float Radius
        {
            get
            {
                return this.sphare.Radius;
            }
        }

		#endregion Public Properties

        #region Implements BoundingVolume Methods
        public override void setPosition(Vector3 position)
        {
            sphare.setCenter(position);
        }

        public override Vector3 getPosition()
        {
            return sphare.Center;
        }

        public override void render()
        {            
            sphare.render();
        }

        public override void dispose()
        {
            sphare.dispose();
        }

        #endregion

		#region Member Variables

        private readonly RigidBody rigidBody;
        private TgcBoundingSphere sphare;

		#endregion Member Variables

       
    }
}
