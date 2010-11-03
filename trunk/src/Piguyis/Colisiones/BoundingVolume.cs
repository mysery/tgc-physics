using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.Piguyis.Colisiones
{
    /// <summary>
    /// Representa un Bounding-Volume
    /// </summary>
    public abstract class BoundingVolume : IRenderObject
    {
        public static readonly Material DEFAULT_MATERIAL = new Material();
        public static readonly int DEFAULT_COLOR = Color.Yellow.ToArgb();

        public abstract void setPosition(Vector3 position);
        public abstract Vector3 getPosition();
        public abstract float getRadius();
        public abstract void render();
        public abstract void dispose();
        
    }
}
