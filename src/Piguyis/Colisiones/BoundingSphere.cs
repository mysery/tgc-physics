// Copyright Gary Evans 2006-2008

using System;
using Microsoft.DirectX;
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
                throw new ArgumentException(@"Radius should not be negative", "radius");
            }
            _sphare = new TgcBoundingSphere(new Vector3(), radius);
        }

        #endregion Object Lifetime


        #region Implements BoundingVolume Methods
        public override void SetPosition(Vector3 position)
        {
            _sphare.setCenter(position);
        }

        public override Vector3 GetPosition()
        {
            return _sphare.Center;
        }

        public override float GetRadius()
        {
            return _sphare.Radius;
        }

        public override void render()
        {            
            _sphare.render();
        }

        public override void dispose()
        {
            _sphare.dispose();
        }

        #endregion

		#region Member Variables

        private readonly TgcBoundingSphere _sphare;

		#endregion Member Variables

       
    }
}
