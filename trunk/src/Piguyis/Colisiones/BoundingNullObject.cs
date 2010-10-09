using System;
using System.Collections.Generic;
using System.Text;
using TgcViewer.Utils;
using System.Drawing;

namespace AlumnoEjemplos.Piguyis.Colisiones
{
    public class BoundingNullObject : BoundingVolume
    {
        public override void setPosition(Microsoft.DirectX.Vector3 position)
        {
            //Bounding sin logica posicional.
            Logger.logInThread("Se esta utilizando un BoundingNullObject", Color.Black);
        }

        public override Microsoft.DirectX.Vector3 getPosition()
        {
            //Bounding sin logica posicional.
            Logger.logInThread("Se esta utilizando un BoundingNullObject", Color.Black);
            return new Microsoft.DirectX.Vector3();
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
