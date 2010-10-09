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
    public class Esena5 : EsenaBase
    {
        private const float zLocation = -40.0f;

        protected override void createBodys()
        {
            #region Spheres
            
            // creo una grilla de cuerpos.
            const int numberSpheresPerSide = 12;
            const float radius = 5.0f;
            const float separationBetweenSpheres = 2.0f;

            const float xCentre = 0.0f;
            const float zCentre = -120.0f;
            float yLocation = 30.0f;

            float initialX = xCentre - (((numberSpheresPerSide - 1) * ((radius * 2.0f) + separationBetweenSpheres)) / 2.0f) - (separationBetweenSpheres / 2.0f);
            float initialZ = zCentre - (((numberSpheresPerSide - 1) * ((radius * 2.0f) + separationBetweenSpheres)) / 2.0f) - (separationBetweenSpheres / 2.0f);

            for (int x = 0; x < numberSpheresPerSide; ++x)
            {
                for (int z = 0; z < numberSpheresPerSide; ++z)
                {                    
                    RigidBody rigidBody = new RigidBody(
                                                        new Vector3(initialX + (x * ((radius * 2) + separationBetweenSpheres)),
                                                                    yLocation,
                                                                    initialZ + (z * ((radius * 2) + separationBetweenSpheres))),
                                                        new Vector3(), 
                                                        1.0f);
                    BoundingSphere sphereLeft = new BoundingSphere(rigidBody, radius);
                    bodys.Add(rigidBody);
                    rigidBody.FuersasInternas = new Fuerza(0.0f, -1.0f, 0.0f);
                }
            }

            #endregion

            #region BigSphere

            float radiusBigYLocation = 10f; // the y offset for the big sphere
            float radiusBig = 100.0f;
            RigidBody bigBody = new RigidBody(new Vector3(  xCentre,
                                                                    -radiusBig + radiusBigYLocation,
                                                                    zCentre),
                                                        new Vector3(),
                                                        float.PositiveInfinity);
            BoundingSphere bigBoundingSphere = new BoundingSphere(bigBody, radiusBig);
            bodys.Add(bigBody);

            #endregion
        }
    }
}
