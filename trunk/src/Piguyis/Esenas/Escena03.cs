using Microsoft.DirectX;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena03 : EscenaBase
    {
        public override void Render(float elapsedTime)
        {
            base.Render(elapsedTime*4f);
        }
        protected override void CreateBodys()
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
            const float radiusForDoubleVolume = radius * 1.2599210499f;
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

        public override string GetTitle()
        {
            return "Escena03 - Motor Fisica";
        }

        public override string GetDescription()
        {
            return "Objetos que colicionan con diferentes masas, Choque linea contra objetos quietos.";
        }
    }
}
