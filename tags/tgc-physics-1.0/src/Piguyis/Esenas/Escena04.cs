using Microsoft.DirectX;
using TgcViewer;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena04 : EscenaBase
    {
        private const float ZLocation = -40.0f;
        public override void Render(float elapsedTime)
        {
            base.Render(elapsedTime * 4f);
        }
        protected override void CreateBodys()
        {
            float locationY = -80.0f;
            float locationX = 0.0f;
            AddMultipleCollisionSpheres(locationX, locationY, Density.Low, Density.Low, Density.Low);

            locationY = -45.0f;
            locationX = 30.0f;
            AddMultipleCollisionSpheres(locationX, locationY, Density.Medium, Density.Low, Density.Low);

            locationY = -10.0f;
            locationX = 0.0f;
            AddMultipleCollisionSpheres(locationX, locationY, Density.Low, Density.Medium, Density.Low);

            locationY = 25.0f;
            locationX = 30.0f;
            AddMultipleCollisionSpheres(locationX, locationY, Density.Low, Density.Medium, Density.High);

            locationY = 55f;
            const float radius = 7.0f;            
            const float startingLocationXStationary = 0.0f;
            const float startingLocationXMoving = -50.0f;
            const int numberOfStationarySpheres = 4;
            const int numberOfMovingSpheres = 2;

            for (int i = 0; i < numberOfMovingSpheres; ++i)
            {
                this.AddBody(Density.Medium,
                    new Vector3(startingLocationXMoving + (i * 2.0f * radius), locationY, ZLocation),
                    new Vector3(2.0f, 0.0f, 0.0f),
                    radius);
            }

            for (int i = 0; i < numberOfStationarySpheres; ++i)
            {
                this.AddBody(Density.Medium,
                    new Vector3(startingLocationXStationary + (i * 2.0f * radius), locationY, ZLocation),
                    new Vector3(), radius);
            }
        }

        private void AddMultipleCollisionSpheres(float locationX, float locationY, Density densityMoving, Density densityStationaryTop,
            Density densityStationaryBottom)
        {
            const float stationarySpheresSeparation = 0.1f;
            const float radius = 7.0f;
            const float initialXLocationMoving = -20.0f;
            const float initialXLocationStationary = 20.0f;

            const float movingVelocityX = 2.0f;
            this.AddBody(densityMoving,
                new Vector3(locationX + initialXLocationMoving, locationY, ZLocation),
                new Vector3(movingVelocityX, 0.0f, 0.0f),
                radius);

            this.AddBody(densityStationaryTop,
                new Vector3(locationX + initialXLocationStationary, locationY + (radius + (stationarySpheresSeparation) / 2.0f), ZLocation),
                new Vector3(), radius);

            this.AddBody(densityStationaryBottom,
                new Vector3(locationX + initialXLocationStationary, locationY - (radius + (stationarySpheresSeparation) / 2.0f), ZLocation),
                new Vector3(), radius);
        }

        public override void InitEscena()
        {
            base.InitEscena();
            GuiController.Instance.FpsCamera.setCamera(new Vector3(0.0f, 75.0f, -250.0f), new Vector3(0.0f, -30.0f, 100.0f));
        }

        public override string GetTitle()
        {
            return "Escena04 - Motor Fisica";
        }

        public override string GetDescription()
        {
            return "Colicion de objetos en fila que propaga el choque y otros que colicionan en distintos angulos.";
        }
    }
}
