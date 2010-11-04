using System.Drawing;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.Piguyis.Colisiones
{
    /// <summary>
    /// Representa un Bounding-Volume
    /// </summary>
    public abstract class BoundingVolume : IRenderObject
    {
        public static readonly int DefaultColor = Color.Yellow.ToArgb();

        public abstract void SetPosition(Vector3 position);
        public abstract Vector3 GetPosition();
        public abstract float GetRadius();
        public abstract void render();
        public abstract void dispose();
        
    }
}
