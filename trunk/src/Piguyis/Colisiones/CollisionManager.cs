// Copyright Gary Evans 2006-2008

using System;
using System.Diagnostics;
using AlumnoEjemplos.Piguyis.Box2DLitePort;
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
/*
        private const float Epsilon = 3.46e-4f;
*/

        public static Contact TestCollision(BoundingSphere sphere1, BoundingSphere sphere2, Vector3 relativeVelocity, Vector3 relativeAcceleration)
        {
            Debug.Assert(sphere1 != null);
            Debug.Assert(sphere2 != null);

            SphereSphereResult result = ClassifySphereSphere(sphere1, sphere2, relativeVelocity, relativeAcceleration);
            if (result.Equals(SphereSphereResult.None))
                return null;

            Vector3 lineOfSeparation = Vector3.Subtract(sphere2.GetPosition(), sphere1.GetPosition());
            if (result.Equals(SphereSphereResult.Intersection))
            {
                float fix = Math.Abs(lineOfSeparation.Length() - sphere1.GetRadius());
                lineOfSeparation.Normalize();
                Contact c = BuildContact(sphere2.GetPosition(), sphere2.GetRadius() + fix, lineOfSeparation);
                c.Separation = Math.Abs(fix - sphere2.GetRadius());
                return c;
            }
            lineOfSeparation.Normalize();
            if (result.Equals(SphereSphereResult.Collision))
            {
                return BuildContact(sphere2.GetPosition(), sphere2.GetRadius(), lineOfSeparation);
            }
/* por el momento no voy a tener en cuenta esto.
            else if (result.Equals(SphereSphereResult.TouchingContact))
            {
                //en este momento tengo objetos cercanos y no me interesa si no tiene fuerzas ni velocidad pero estan en contacto.
                return null;// buildContact(sphere2.getPosition(), sphere2.getRadius(), lineOfSeparation);
            }
            else if (result.Equals(SphereSphereResult.ForcedContact))
            {
                return null;//buildContact(sphere2.getPosition(), sphere2.getRadius(), lineOfSeparation);
            }
*/
            return null;
        }

        private static Contact BuildContact(Vector3 positionContact, float distanceContact, Vector3 normal)
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

        public static Contact TestCollision(BoundingPlane boundingPlane, BoundingSphere sphare, Vector3 sphareDirection, Vector3 acceleration)
        {
            return TestCollision(sphare, boundingPlane, sphareDirection, acceleration);
        }

        /// <summary>
        /// Idica si un BoundingSphere colisiona con un plano
        /// </summary>
        /// <returns>True si hay colisión</returns>
        public static Contact TestCollision(BoundingSphere sphare, BoundingPlane boundingPlane, Vector3 sphareDirection, Vector3 acceleration)
        {
            Plane plane = boundingPlane.Plane;
            Vector3 p = TgcCollisionUtils.toVector3(plane);
            // For a normalized plane (|p.n| = 1), evaluating the plane equation
            // for a point gives the signed distance of the point to the plane
            float dist = Vector3.Dot(sphare.GetPosition(), p) + plane.D;
            // If sphere center within +/-radius from plane, plane intersects sphere
            if (Math.Abs(dist) <= sphare.GetRadius())
            {/*
                //Vector3 reflect = 2 * p * Vector3.Dot(boundingPlane.Normal, sphareDirection) - sphareDirection;
                Vector3 inverseDirection = Vector3.Multiply(sphareDirection, -1f);
                float projection = Vector3.Dot(boundingPlane.Normal, inverseDirection);
                Vector3 reflection = 2 * projection * boundingPlane.Normal + sphareDirection;
                float length = reflection.Length();                
                reflection.Normalize();
                return buildContact(sphare.getPosition(), length, reflection);*/
                Contact contact = new Contact();
                contact.Separation = Math.Abs(dist) - sphare.GetRadius();
                contact.Normal = boundingPlane.Normal;
                contact.ContactPoint = Vector3.Subtract(sphare.GetPosition() - boundingPlane.GetPosition(), Vector3.Multiply(contact.Normal, sphare.GetRadius()));
                contact.Position = contact.ContactPoint;
                return contact;
            }

            return null;
        }

        /// <summary>
        /// Checks whether this sphere instance is colliding
        ///  with the sphere passed in.
        /// </summary>
        public static SphereSphereResult ClassifySphereSphere(BoundingSphere lhs, BoundingSphere rhs, Vector3 relativeVelocity, Vector3 relativeAcceleration)
        {
            SphereSphereResult result = SphereSphereResult.None;

            Vector3 vectorThisCentreToOther = rhs.GetPosition() - lhs.GetPosition();

            float separation = Vector3.Dot(vectorThisCentreToOther, vectorThisCentreToOther);
            float combinedRadius = lhs.GetRadius() + rhs.GetRadius();
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
            else if (!FastMath.IsEqualWithinTol(separation, combinedRadius))
            {
                result = SphereSphereResult.Collision;
            }
            else
            {
                result = SphereSphereResult.Intersection;
            }

            return result;
        }

        public static Contact TestCollision(BoundingVolume pivotBoundingVolume, BoundingVolume nearBoundingVolume, Vector3 velocity, Vector3 aceleracion)
        {
            if (pivotBoundingVolume is BoundingSphere && nearBoundingVolume is BoundingSphere)
                return TestCollision((BoundingSphere)pivotBoundingVolume,
                                                                          (BoundingSphere)nearBoundingVolume,
                                                                           velocity,
                                                                           aceleracion);
            if (pivotBoundingVolume is BoundingSphere && nearBoundingVolume is BoundingPlane)
                return TestCollision((BoundingSphere)pivotBoundingVolume,
                                                         (BoundingPlane)nearBoundingVolume,
                                                         velocity,
                                                         aceleracion);
            if (pivotBoundingVolume is BoundingPlane && nearBoundingVolume is BoundingSphere)
                return TestCollision((BoundingSphere)nearBoundingVolume,
                                                         (BoundingPlane)pivotBoundingVolume,
                                                         velocity,
                                                         aceleracion);

            throw new NotSupportedException("No se soportan mas tipos");
        }
    }
}
