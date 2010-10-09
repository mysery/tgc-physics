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
    public class Esena2 : EsenaBase
    {
        protected override void createBodys()
        {
            const float radius = 20.0f;

            // sphere 1.
            RigidBody rigidBodyLeft = new RigidBody(new Vector3(-radius * 2, 0.0f, 0.0f),
                                                    new Vector3(), 10.0f);
            BoundingSphere sphereLeft = new BoundingSphere(rigidBodyLeft, radius);
            bodys.Add(rigidBodyLeft);
            rigidBodyLeft.FuersasInternas = new Fuerza(100.0f, 0.0f, 0.0f);

            // sphere 2.
            RigidBody rigidBodyRight = new RigidBody(new Vector3(radius * 2, 0.0f, 0.0f),
                                                    new Vector3(), 1.0f);
            BoundingSphere sphereRight = new BoundingSphere(rigidBodyRight, radius);
            bodys.Add(rigidBodyRight);
            rigidBodyRight.FuersasInternas = new Fuerza(-10.0f, 0.0f, 0.0f);
        }
    }
}
