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
    public class Esena4 : EsenaBase
    {
        private const float zLocation = -40.0f;

        protected override void createBodys()
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
                    new Vector3(startingLocationXMoving + (i * 2.0f * radius), locationY, zLocation),
                    new Vector3(2.0f, 0.0f, 0.0f),
                    radius);
            }

            for (int i = 0; i < numberOfStationarySpheres; ++i)
            {
                this.AddBody(Density.Medium,
                    new Vector3(startingLocationXStationary + (i * 2.0f * radius), locationY, zLocation),
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
                new Vector3(locationX + initialXLocationMoving, locationY, zLocation),
                new Vector3(movingVelocityX, 0.0f, 0.0f),
                radius);

            this.AddBody(densityStationaryTop,
                new Vector3(locationX + initialXLocationStationary, locationY + (radius + (stationarySpheresSeparation) / 2.0f), zLocation),
                new Vector3(), radius);

            this.AddBody(densityStationaryBottom,
                new Vector3(locationX + initialXLocationStationary, locationY - (radius + (stationarySpheresSeparation) / 2.0f), zLocation),
                new Vector3(), radius);
        }
    }
}
