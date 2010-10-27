// Copyright Gary Evans 2006-2008

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AlumnoEjemplos.Piguyis.Box2DLitePort;
using AlumnoEjemplos.Piguyis.Body;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcGeometry;
using AlumnoEjemplos.PiguYis.Matematica;

namespace AlumnoEjemplos.Piguyis.Colisiones
{
    /// <summary>
    /// Maneja la deteccion de coliciones.
    /// </summary>
    public class CollisionManager
    {
        private const float EPSILON = 3.46e-4f;

        public static Contact testCollision(BoundingSphere sphere1, BoundingSphere sphere2, Vector3 relativeVelocity, Vector3 relativeAcceleration)
        {
            Debug.Assert(sphere1 != null);
            Debug.Assert(sphere2 != null);

            SphereSphereResult result = CollisionManager.classifySphereSphere(sphere1, sphere2, relativeVelocity, relativeAcceleration);
            if (result.Equals(SphereSphereResult.None))
                return null;

            Vector3 lineOfSeparation = Vector3.Subtract(sphere2.getPosition(), sphere1.getPosition());
            if (result.Equals(SphereSphereResult.Intersection))
            {
                float fix = Math.Abs(lineOfSeparation.Length() - sphere1.Radius);
                lineOfSeparation.Normalize();
                return buildContact(sphere2.getPosition(), sphere2.Radius + fix, lineOfSeparation);
            }
            lineOfSeparation.Normalize();
            if (result.Equals(SphereSphereResult.Collision))
            {
                return buildContact(sphere2.getPosition(), sphere2.Radius, lineOfSeparation);
            }
/* por el momento no voy a tener en cuenta esto.
            else if (result.Equals(SphereSphereResult.TouchingContact))
            {
                //en este momento tengo objetos cercanos y no me interesa si no tiene fuerzas ni velocidad pero estan en contacto.
                return null;// buildContact(sphere2.getPosition(), sphere2.Radius, lineOfSeparation);
            }
            else if (result.Equals(SphereSphereResult.ForcedContact))
            {
                return null;//buildContact(sphere2.getPosition(), sphere2.Radius, lineOfSeparation);
            }
*/
            return null;
        }

        private static Contact buildContact(Vector3 positionContact, float distanceContact, Vector3 normal)
        {
            Contact contact = new Contact();
            contact.Normal = normal;
            //Los puntos de contacto estan simplificados a 1 ahora.
            //contact.PointCount = 1;
            contact.ContactPoint = Vector3.Subtract(positionContact, Vector3.Multiply(contact.Normal, distanceContact));

            // TODO: check whether these are actually the same thing.
            contact.Position = contact.ContactPoint;

            return contact;
        }

        /// <summary>
        /// Idica si un BoundingSphere colisiona con un plano
        /// </summary>
        /// <returns>True si hay colisión</returns>
        public static Contact testCollision(BoundingSphere s, BoundingPlane boundingPlane)
        {
            Plane plane = boundingPlane.Plane;
            Vector3 p = TgcCollisionUtils.toVector3(plane);
/*
            double d = -(planeNormal * planeOrigin);
            double numer = planeNormal * rayOrigin + d;
            double denom = planeNormal * rayVector;
            return -(numer / denom);
            */
            // For a normalized plane (|p.n| = 1), evaluating the plane equation
            // for a point gives the signed distance of the point to the plane
            float dist = Vector3.Dot(s.getPosition(), p) + plane.D;
            // If sphere center within +/-radius from plane, plane intersects sphere
            if (Math.Abs(dist) <= s.Radius)
            {
                p.Normalize();
                return buildContact(s.getPosition(), s.Radius, p);
            }

            return null;
        }

        /// <summary>
        /// Checks whether this sphere instance is colliding
        ///  with the sphere passed in.
        /// </summary>
        public static SphereSphereResult classifySphereSphere(BoundingSphere lhs, BoundingSphere rhs, Vector3 relativeVelocity, Vector3 relativeAcceleration)
        {
            SphereSphereResult result = SphereSphereResult.None;

            Vector3 vectorThisCentreToOther = rhs.getPosition() - lhs.getPosition();

            float separation = Vector3.Dot(vectorThisCentreToOther, vectorThisCentreToOther);
            float combinedRadius = lhs.Radius + rhs.Radius;
            combinedRadius = combinedRadius * combinedRadius;
            if (separation > combinedRadius)
            {
                if (FastMath.IsEqualWithinTol(separation, combinedRadius))
                {
                    // The spheres are within touching distance
                    // See whether they're moving towards each other.
                    // relative velocity of the other body with respect to us.
                    float speedAwayFromUs = Vector3.Dot(vectorThisCentreToOther, relativeVelocity);
                    if (FastMath.IsEqualWithinTol(speedAwayFromUs, 0f))
                    {
                        // the object is not moving towards us, check whether it is stationary
                        // or moving away i.e. this is a very near miss)
                        if (FastMath.MinusTolerance(relativeVelocity.Length()))
                        {
                            // We have some sort of contact
                            // Either just touching, or being forced towards.
                            float accelerationAwayFromUs = Vector3.Dot(vectorThisCentreToOther, relativeAcceleration);
                            if (FastMath.IsEqualWithinTol(accelerationAwayFromUs, 0f))
                            {
                                // The objects just happen to be in close proximity.
                                result = SphereSphereResult.TouchingContact;
                            }
                            else if (FastMath.MinusTolerance(accelerationAwayFromUs))
                            {
                                // A negative value indicates that the rhs sphere is being accelerated towards us
                                result = SphereSphereResult.ForcedContact;
                            }
                            // else, the sphere is being accelerated away, is no contact or collision.
                        }
                        // else, object's acceleration is no different to ours, no collision.
                    }
                }
            }
            else if (FastMath.IsEqualWithinTol(separation, combinedRadius))
            {
                result = SphereSphereResult.Collision;
            }
            else
            {
                result = SphereSphereResult.Intersection;
            }

            return result;
        }
    }
}
