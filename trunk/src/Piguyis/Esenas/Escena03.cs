using System;
using System.Collections.Generic;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;
using AlumnoEjemplos.Piguyis.Colisiones;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;
using AlumnoEjemplos.Piguyis.Fisica;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena03 : EscenaBase
    {
        public override void render(float elapsedTime)
        {
            base.render(elapsedTime*4f);
        }
        protected override void createBodys()
        {
            float locationY = -78.0f;
            const float radius = 7.0f;
            const float initialMovingVelocityX = 2.0f;
            const float initialMovingLocationX = -25.0f;
            const float initialStationaryLocationX = 15.0f;
            this.AddBody(Density.Low, new Vector3(initialMovingLocationX, locationY, 0f), new Vector3(initialMovingVelocityX,0f, 0f), radius);
            this.AddBody(Density.Low, new Vector3(initialStationaryLocationX, locationY, 0f), new Vector3(), radius);

            locationY = -52.0f;
            this.AddBody(Density.Low, new Vector3(initialMovingLocationX, locationY, 0f), new Vector3(initialMovingVelocityX,0f, 0f), radius);
            this.AddBody(Density.Medium, new Vector3(initialStationaryLocationX, locationY, 0f), new Vector3(), radius);

            locationY = -34.0f;
            float radiusForDoubleVolume = radius * 1.2599210499f;
            this.AddBody(Density.Low, new Vector3(initialMovingLocationX, locationY, 0f), new Vector3(initialMovingVelocityX,0f, 0f), radius);
            this.AddBody(Density.Low, new Vector3(initialStationaryLocationX + radiusForDoubleVolume - radius, locationY, 0f), new Vector3(), radiusForDoubleVolume);

            locationY = -18.0f;
            this.AddBody(Density.Low, new Vector3(initialMovingLocationX, locationY, 0f), new Vector3(initialMovingVelocityX,0f, 0f), radius);
            this.AddBody(Density.High, new Vector3(initialStationaryLocationX, locationY, 0f), new Vector3(), radius);

            locationY = -2.0f;
            this.AddBody(Density.Medium, new Vector3(initialMovingLocationX, locationY, 0f), new Vector3(initialMovingVelocityX,0f, 0f), radius);
            this.AddBody(Density.High, new Vector3(initialStationaryLocationX, locationY, 0f), new Vector3(), radius);

            locationY = 14.0f;
            this.AddBody(Density.High, new Vector3(initialMovingLocationX, locationY, 0f), new Vector3(initialMovingVelocityX,0f, 0f), radius);
            this.AddBody(Density.Medium, new Vector3(initialStationaryLocationX, locationY, 0f), new Vector3(), radius);

            locationY = 30.0f;
            this.AddBody(Density.High, new Vector3(initialMovingLocationX, locationY, 0f), new Vector3(initialMovingVelocityX,0f, 0f), radius);
            this.AddBody(Density.Low, new Vector3(initialStationaryLocationX, locationY, 0f), new Vector3(), radius);
        }

        public override string getTitle()
        {
            return "Escena03 - Motor Fisica";
        }

        public override string getDescription()
        {
            return "Objetos que colicionan con diferentes masas, Choque linea contra objetos quietos.";
        }
    }
}
