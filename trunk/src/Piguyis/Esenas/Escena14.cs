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
    public class Escena14 : EscenaBase
    {
        protected override void createBodys()
        {
            #region Spheres en grilla

            // creo una grilla de cuerpos.
            int numberSpheresPerSide = 5;
            float radius = 5f;
            float separationBetweenSpheres = 2.0f;

            float xCentre = 0.0f;
            float zCentre = -120.0f;
            float yLocation = 30.0f;

            float initialX = xCentre - (((numberSpheresPerSide - 1) * ((radius * 2.0f) + separationBetweenSpheres)) / 2.0f) - (separationBetweenSpheres / 2.0f);
            float initialZ = zCentre - (((numberSpheresPerSide - 1) * ((radius * 2.0f) + separationBetweenSpheres)) / 2.0f) - (separationBetweenSpheres / 2.0f);

            float restitucion = 0.2f;
            for (int x = 0; x < numberSpheresPerSide; ++x)
            {
                for (int z = 0; z < numberSpheresPerSide; ++z)
                {
                    BodyBuilder builder = new BodyBuilder(
                                                        new Vector3(initialX + (x * ((radius * 2) + separationBetweenSpheres)),
                                                                    yLocation,
                                                                    initialZ + (z * ((radius * 2) + separationBetweenSpheres))),
                                                        new Vector3(),
                                                        1.0f);
                    builder.setBoundingSphere(radius);
                    builder.setForces(0.0f, -5.0f, 0.0f);
                    builder.setRestitution(restitucion);
                    bodys.Add(builder.build());
                }
                restitucion = restitucion + 0.2f;
            }

            #endregion

            #region Spheres en escalera

            numberSpheresPerSide = 15;
            radius = 2.0f;
            separationBetweenSpheres = 1.5f;

            xCentre = 0.0f;
            zCentre = -120.0f;
            yLocation = 0.0f;

            initialX = xCentre - (((numberSpheresPerSide - 1) * ((radius * 2.0f) + separationBetweenSpheres)) / 2.0f) - (separationBetweenSpheres / 2.0f);
            initialZ = zCentre - (((numberSpheresPerSide - 1) * ((radius * 2.0f) + separationBetweenSpheres)) / 2.0f) - (separationBetweenSpheres / 2.0f);

            for (int x = 0; x < numberSpheresPerSide; ++x)
            {
                yLocation -= separationBetweenSpheres;
                for (int z = 0; z < numberSpheresPerSide; ++z)
                {
                    BodyBuilder builder = new BodyBuilder(
                                                        new Vector3(initialX + (x * ((radius * 2) + separationBetweenSpheres)),
                                                                    yLocation,
                                                                    initialZ + (z * ((radius * 2) + separationBetweenSpheres))),
                                                        new Vector3(),
                                                        float.PositiveInfinity);
                    builder.setBoundingSphere(radius);
                    bodys.Add(builder.build());
                }
            }

            #endregion
        }
        public override void initEscena()
        {
            base.initEscena();
            GuiController.Instance.FpsCamera.setCamera(new Vector3(0.0f, 75.0f, -300.0f), new Vector3(0.0f, -50.0f, 100.0f));
        }

        public override string getTitle()
        {
            return "Escena14 - Motor Fisica";
        }

        public override string getDescription()
        {
            return "Esferas alineadas caen sobre una plataforma inclinada. Con restitucion incremental 0.2 a 1.0";
        }
    }
}