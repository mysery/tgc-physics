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
    public class Esena9 : EsenaBase
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
                    bodys.Add(builder.build());
                }
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
                    builder.setForces(0.0f, -1.0f, 0.0f);
                    bodys.Add(builder.build());
                }
            }

            #endregion
        }
        public override void initEsena()
        {
            base.initEsena();
            GuiController.Instance.FpsCamera.setCamera(new Vector3(0.0f, 75.0f, -300.0f), new Vector3(0.0f, -50.0f, 100.0f));
        }

    }
}
