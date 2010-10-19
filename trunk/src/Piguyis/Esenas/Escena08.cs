using System;
using System.Collections.Generic;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;
using AlumnoEjemplos.Piguyis.Colisiones;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;
using AlumnoEjemplos.Piguyis.Fisica;
using TgcViewer;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena08 : EscenaBase
    {
        private const float zLocation = -40.0f;
        public override void render(float elapsedTime)
        {
            base.render(elapsedTime * 5f);
        }
        protected override void createBodys()
        {
            #region Spheres
            
            // creo una grilla de cuerpos.
            const int numberSpheresPerSide = 12;
            const int MAX_VALUE_RANDOM = 60;            
            const float radius = 5.0f;
            const float separationBetweenSpheres = 2.0f;

            const float xCentre = 0.0f;
            const float zCentre = -120.0f;
            float yLocation = 30.0f;
            Random random = new Random();

            float initialX = xCentre - (((numberSpheresPerSide - 1) * ((radius * 2.0f) + separationBetweenSpheres)) / 2.0f) - (separationBetweenSpheres / 2.0f);
            float initialZ = zCentre - (((numberSpheresPerSide - 1) * ((radius * 2.0f) + separationBetweenSpheres)) / 2.0f) - (separationBetweenSpheres / 2.0f);

            for (int x = 0; x < numberSpheresPerSide; ++x)
            {
                for (int z = 0; z < numberSpheresPerSide; ++z)
                {
                    BodyBuilder builder = new BodyBuilder(
                                                        new Vector3(initialX + (x * ((radius * 2) + separationBetweenSpheres)),
                                                                    yLocation + random.Next(MAX_VALUE_RANDOM),
                                                                    initialZ + (z * ((radius * 2) + separationBetweenSpheres))),
                                                        new Vector3(), 
                                                        1.0f);
                    builder.setBoundingSphere(radius);
                    builder.setForces(0.0f, -1.0f, 0.0f);
                    bodys.Add(builder.build());
                }
            }

            #endregion

            #region BigSphere

            float radiusBigYLocation = 10f;
            float radiusBig = 150.0f;
            BodyBuilder bigBuilder = new BodyBuilder(new Vector3(xCentre,
                                                                    -radiusBig + radiusBigYLocation,
                                                                    zCentre),
                                                        new Vector3(),
                                                        float.PositiveInfinity);
            bigBuilder.setBoundingSphere(radiusBig);
            bodys.Add(bigBuilder.build());
            #endregion
        }

        public override void initEscena()
        {
            base.initEscena();
            GuiController.Instance.FpsCamera.setCamera(new Vector3(0.0f, 75.0f, -650.0f), new Vector3(0.0f, -30.0f, 100.0f));
        }

        public override string getTitle()
        {
            return "Escena08 - Motor Fisica";
        }

        public override string getDescription()
        {
            return "Muchas esferas posicionadas aleatoria mente son afectadas por una fuerza caen en una gran esfera inmovil.";
        }
    }
}
