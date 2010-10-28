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
            orientation = o;
            if (Orientations.XYplane.Equals(o))
            {
                normal = new Vector3(0f, 0f, -1f);
                box = new TgcBoundingBox(new Vector3(-WIDTH, -HEIGTH, 0f), new Vector3(WIDTH, HEIGTH, 0f));
            }
            else if (Orientations.XZplane.Equals(o)) {
                normal = new Vector3(0f, -1f, 0f);
                box = new TgcBoundingBox(new Vector3(-WIDTH, 0f, -HEIGTH), new Vector3(WIDTH, 0f, HEIGTH));
            }
            else {
                normal = new Vector3(-1f, 0f, 0f);
                box = new TgcBoundingBox(new Vector3(0f, -WIDTH, -HEIGTH), new Vector3(0f, WIDTH, HEIGTH));
            }                
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="n"></param>
        public BoundingPlane(Vector3 n)
        {
            normal = n;
            box = new TgcBoundingBox(new Vector3(0f, -WIDTH, -HEIGTH), new Vector3(0f, WIDTH, HEIGTH));
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
                Vector3 pos = box.calculateBoxCenter();
                if (Orientations.XYplane.Equals(this.orientation))
                    return new Plane(0f, 0f, -1f, pos.Z);
                else if (Orientations.XZplane.Equals(this.orientation))
                    return new Plane(0f, -1f, 0f, pos.Y);
                else
                    return new Plane(-1f, 0f, 0f, pos.X);
            }
        }

        public Vector3 Normal
        {
            get
            {
                return normal;                
            }
            set
            {
                this.normal = value;
            }
        }

        #region Implements BoundingVolume Methods
        public override void setPosition(Vector3 position)
        {
            box.scaleTranslate(position, new Vector3(1f, 1f, 1f));
        }

        public override Vector3 getPosition()
        {
            return box.calculateBoxCenter();
        }

        public override void render()
        {            
            box.render();
        }

        public override void dispose()
        {
            box.dispose();
        }

        #endregion

		#region Member Variables
        private Orientations orientation;
        private TgcBoundingBox box;
        private Vector3 normal;
        private static float HEIGTH = 100f;
        private static float WIDTH = 100f;

		#endregion Member Variables

       
    }
}
