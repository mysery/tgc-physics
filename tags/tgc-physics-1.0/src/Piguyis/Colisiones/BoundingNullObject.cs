using TgcViewer.Utils;
using System.Drawing;

namespace AlumnoEjemplos.Piguyis.Colisiones
{
    public class BoundingNullObject : BoundingVolume
    {
        public override void SetPosition(Microsoft.DirectX.Vector3 position)
        {
            //Bounding sin logica posicional.
            Logger.logInThread("Se esta utilizando un BoundingNullObject", Color.Red);
        }

        public override Microsoft.DirectX.Vector3 GetPosition()
        {
            //Bounding sin logica posicional.
            Logger.logInThread("Se esta utilizando un BoundingNullObject", Color.Red);
            return new Microsoft.DirectX.Vector3();
        }

        public override float GetRadius()
        {
            //Bounding sin logica posicional.
            Logger.logInThread("Se esta utilizando un BoundingNullObject", Color.Red);
            return 0f;
        }

        public override void render()
        {
            //Bounding sin visualicacion.
        }

        public override void dispose()
        {
            //Bounding sin visualicacion.
        }
    }
}
