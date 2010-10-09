// Copyright Gary Evans 2006-2008

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AlumnoEjemplos.Piguyis.Box2DLitePort;
using AlumnoEjemplos.Piguyis.Body;
using Microsoft.DirectX;

namespace AlumnoEjemplos.Piguyis.Colisiones
{
    /// <summary>
    /// Maneja la deteccion de coliciones.
    /// </summary>
    public class CollisionManager
    {
        public static Contact testCollision(BoundingSphere sphere1, BoundingSphere sphere2)
        {
            Debug.Assert(sphere1 != null);
            Debug.Assert(sphere2 != null);

            float EPSILON = 1.2e-7f;

            Vector3 lineOfSeparation = Vector3.Subtract(sphere2.getPosition(), sphere1.getPosition());
            float separationDistanceSqr = Vector3.Dot(lineOfSeparation, lineOfSeparation);
            float radiusSum = sphere1.Radius + sphere2.Radius;
            if ((separationDistanceSqr > (radiusSum * radiusSum)))
            {
                return null;
            }

            Contact contact = new Contact();

            float separation;
            if (separationDistanceSqr < EPSILON)
            {
                separation = -radiusSum;
                //estoy casi adentro del cuerpo!!!
                //Ver el significado del tomar este vector de contacto. (evita una div por 0)
                contact.Normal = new Vector3(0.0f, 0.0f, 1.0f);
            }
            else
            {
                float distance = (float) Math.Sqrt(separationDistanceSqr);
                separation = distance - radiusSum;
                float a = 1.0f / distance;
                contact.Normal = Vector3.Multiply(lineOfSeparation, a);
            }

            //Los puntos de contacto estan simplificados a 1 ahora.
            //contact.PointCount = 1;
            contact.ContactPoint = Vector3.Subtract(sphere2.getPosition(), Vector3.Multiply(contact.Normal, (float) sphere2.Radius));

            // TODO: check whether these are actually the same thing.
            contact.Position = contact.ContactPoint;

            return contact;
        }
    }
}
