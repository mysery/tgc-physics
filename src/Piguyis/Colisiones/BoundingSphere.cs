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
        /// <param name="radius">The sphere's radius</param>
        public BoundingSphere(float radius)
        {
            if (radius < 0.0f)
            {
                throw new ArgumentException("Radius should not be negative", "radius");
            }
            sphare = new TgcBoundingSphere(new Vector3(), radius);
        }

        #endregion Object Lifetime


        #region Implements BoundingVolume Methods
        public override void setPosition(Vector3 position)
        {
            sphare.setCenter(position);
        }

        public override Vector3 getPosition()
        {
            return sphare.Center;
        }

        public override float getRadius()
        {
            return sphare.Radius;
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

        private TgcBoundingSphere sphare;

		#endregion Member Variables

       
    }
}
