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
    public class Esena6 : EsenaBase
    {
        protected override void createBodys()
        {
            const float radius = 2.0f;
            const float separationDistance = 12.0f;
            const int numberOfSpheresPerBaseLayer = 7;
            const float initialYLocation = -50.0f;
            const float zOffset = -10f;

            for (int y = 0; y < numberOfSpheresPerBaseLayer; ++y)
            {
                for (int x = 0; x < numberOfSpheresPerBaseLayer - y; ++x)
                {
                    for (int z = 0; z < numberOfSpheresPerBaseLayer - y; ++z)
                    {
                        RigidBody rigidBody = new RigidBody(
                                                            new Vector3((radius * 2f * x) + (y * radius),
                                                                        initialYLocation + (separationDistance * y),
                                                                        zOffset + (radius * 2 * z) + (y * radius)),
                                                            new Vector3(), 
                                                            y != 0 ? 1.0f : float.PositiveInfinity);
                        BoundingSphere sphere = new BoundingSphere(rigidBody, radius);
                        bodys.Add(rigidBody);
                        if (y != 0)
                            rigidBody.FuersasInternas = new Fuerza(0.0f, -1.0f, 0.0f);
                        }                        
                    }
                }
        }


    }
}
