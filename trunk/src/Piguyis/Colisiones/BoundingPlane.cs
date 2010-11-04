// Copyright Gary Evans 2006-2008

using Microsoft.DirectX;
using TgcViewer.Utils.TgcGeometry;


namespace AlumnoEjemplos.Piguyis.Colisiones
{
    /// <summary>
    /// Represents a Sphere class, for Collision detection.
    /// </summary>
    public class BoundingPlane : BoundingVolume
    {
        #region Object Lifetime

        /// <summary>
        /// plano.
        /// </summary>
        /// <param name="o">Orientacion xy, xz o yz</param>
        public BoundingPlane(Orientations o)
        {
            _orientation = o;
            if (Orientations.XYplane.Equals(o))
            {
                _normal = new Vector3(0f, 0f, -1f);
                _box = new TgcBoundingBox(new Vector3(-Width, -Heigth, 0f), new Vector3(Width, Heigth, 0f));
            }
            else if (Orientations.XZplane.Equals(o)) {
                _normal = new Vector3(0f, -1f, 0f);
                _box = new TgcBoundingBox(new Vector3(-Width, 0f, -Heigth), new Vector3(Width, 0f, Heigth));
            }
            else {
                _normal = new Vector3(-1f, 0f, 0f);
                _box = new TgcBoundingBox(new Vector3(0f, -Width, -Heigth), new Vector3(0f, Width, Heigth));
            }                
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="n"></param>
        public BoundingPlane(Vector3 n)
        {
            _normal = n;
            _box = new TgcBoundingBox(new Vector3(0f, -Width, -Heigth), new Vector3(0f, Width, Heigth));
        }

        public enum Orientations
        {
            /// <summary>
            /// Pared vertical a lo largo de X
            /// </summary>
            XYplane,
            /// <summary>
            /// Pared horizontal
            /// </summary>
            XZplane,
            /// <summary>
            /// Pared vertical a lo largo de Z
            /// </summary>
            YZplane,
        }

        #endregion Object Lifetime

        public Plane Plane
        {
            get 
            {
                Vector3 pos = _box.calculateBoxCenter();
                if (Orientations.XYplane.Equals(this._orientation))
                    return new Plane(0f, 0f, -1f, pos.Z);
                return Orientations.XZplane.Equals(this._orientation) ? 
                            new Plane(0f, -1f, 0f, pos.Y) : 
                            new Plane(-1f, 0f, 0f, pos.X);
            }
        }

        public Vector3 Normal
        {
            get
            {
                return _normal;                
            }
            set
            {
                this._normal = value;
            }
        }

        #region Implements BoundingVolume Methods
        public override void SetPosition(Vector3 position)
        {
            _box.scaleTranslate(position, new Vector3(1f, 1f, 1f));
        }

        public override Vector3 GetPosition()
        {
            return _box.calculateBoxCenter();
        }

        public override float GetRadius()
        {
            return _box.calculateBoxRadius();
        }

        public override void render()
        {            
            _box.render();
        }

        public override void dispose()
        {
            _box.dispose();
        }

        #endregion

		#region Member Variables
        private readonly Orientations _orientation;
        private readonly TgcBoundingBox _box;
        private Vector3 _normal;
        private const float Heigth = 100f;
        private const float Width = 100f;

		#endregion Member Variables

       
    }
}
