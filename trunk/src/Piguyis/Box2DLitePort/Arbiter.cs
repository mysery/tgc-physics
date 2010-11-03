// This code is based on Box2DLite by Erin Catto,
// with restitution and sphere collision adapter from Box2D 2.0.1.

// The original copyright message from Box2DLite is as follows:
/*
* Copyright (c) 2006-2007 Erin Catto http://www.gphysics.com
*
* Permission to use, copy, modify, distribute and sell this software
* and its documentation for any purpose is hereby granted without fee,
* provided that the above copyright notice appear in all copies.
* Erin Catto makes no representations about the suitability 
* of this software for any purpose.  
* It is provided "as is" without express or implied warranty.
*/

// The original copyright message from Box2D is as follows:
/*
* Copyright (c) 2007 Erin Catto http://www.gphysics.com
*
* This software is provided 'as-is', without any express or implied
* warranty.  In no event will the authors be held liable for any damages
* arising from the use of this software.
* Permission is granted to anyone to use this software for any purpose,
* including commercial applications, and to alter it and redistribute it
* freely, subject to the following restrictions:
* 1. The origin of this software must not be misrepresented; you must not
* claim that you wrote the original software. If you use this software
* in a product, an acknowledgment in the product documentation would be
* appreciated but is not required.
* 2. Altered source versions must be plainly marked as such, and must not be
* misrepresented as being the original software.
* 3. This notice may not be removed or altered from any source distribution.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AlumnoEjemplos.Piguyis.Colisiones;
using Microsoft.DirectX;
using AlumnoEjemplos.PiguYis.Matematica;
using AlumnoEjemplos.Piguyis.Body;

namespace AlumnoEjemplos.Piguyis.Box2DLitePort
{
    /// <summary>
    /// Maneja la interaccion entre dos cuerpos.
    /// </summary>
    public class Arbiter
    {
        private RigidBody body1;
        private RigidBody body2;
        private Contact contact = null;

        //TODO: variables del mundo no del arbritro....
        private bool warmStarting = false; //ok, el mundo se lo pasa al arbritro.
        //private float friction = 0.0; todo esto tendria que venir de los cuerpos. (no implementado)

        /// <summary>
        /// El Contacto de los dos cuerpos que manejara este arbritro.
        /// </summary>
        public Contact Contact
        {
            get
            {
                return contact;
            }
        }

        /// <summary>
        /// Constructor <see cref="Arbiter" />
        /// </summary>
        public Arbiter(bool warm, Contact contact, RigidBody body1, RigidBody body2)
        {
            this.warmStarting = warm;
            this.contact = contact;
            this.body1 = body1;
            this.body2 = body2;
        }

        /// <summary>
        /// Actualiza el contacto en caso de que los cuerpos sean los mismos.
        /// </summary>
        /// <param name="contact"></param>
        public void Update(Contact contact)
        {
            Debug.Assert(contact != null);

            Contact oldContact = null;
            if (warmStarting)
            {
                oldContact = this.contact;
            }

            this.contact = contact;

            if (oldContact == null)
            {
                this.contact.AccumulatedNormalImpulse = 0.0f;
                this.contact.AccumulatedNormalImpulseForPositionBias = 0.0f;
                this.contact.AccumulatedTangentImpulse = 0.0f;
            }
            else
            {
                this.contact.AccumulatedNormalImpulse = oldContact.AccumulatedNormalImpulse;
                this.contact.AccumulatedNormalImpulseForPositionBias = oldContact.AccumulatedNormalImpulseForPositionBias;
                this.contact.AccumulatedTangentImpulse = oldContact.AccumulatedTangentImpulse;
            }
        }

        /// <summary>
        /// procesamiento antes de aplicar los impulsos.
        /// </summary>
        /// <param name="inverseTimeStep"></param>
        public void PreStep(float inverseTimeStep)
        {
            // TODO: de box2dlite
            ////const float allowedPenetration = 0.01;
            ////float biasFactor = 0.2; //TODO world::splitImpulses ? 0.8 : 0.2
            // TODO:
            // biasFactor = world::positionCorrection ? biasFactor : 0.0;

            Debug.Assert(contact != null);

            //TODO: loop multi-contacts. ahora simplificado.
            Vector3 r1 = contact.Position - this.body1.Location;
            Vector3 r2 = contact.Position - this.body2.Location;

            // Precompute normal mass, tangent mass, and bias.
            float rn1 = Vector3.Dot(r1, contact.Normal);
            float rn2 = Vector3.Dot(r2, contact.Normal);
            float kNormal = this.body1.inverseMass + this.body2.inverseMass;
            kNormal += (Vector3.Dot(r1, r1) - (rn1 * rn1)) + (Vector3.Dot(r2, r2) - (rn2 * rn2));

            contact.MassNormal = 1.0f / kNormal;

            Vector3 tangent = Vector3.Cross(contact.Normal, new Vector3(1.0f, 1.0f, 1.0f));
            float rt1 = Vector3.Dot(r1, tangent);
            float rt2 = Vector3.Dot(r2, tangent);
            float kTangent = this.body1.inverseMass + this.body2.inverseMass;
            kTangent += (Vector3.Dot(r1, r1) - (rt1 * rt1)) + (Vector3.Dot(r2, r2) - (rt2 * rt2));
            contact.MassTangent = 1.0f / kTangent;

            // box2dlite version:
            //contact.Bias = -biasFactor * inverseTimeStep * Math.Min(0.0, contact.Separation + allowedPenetration);
            // de box2d 2.0.1
            contact.Bias = 0f;
            if (contact.Separation > 0.0)
            {
                contact.Bias = -60.0f * contact.Separation; //TODO: Bias Factor configurable.
            }
            // TODO: incluir angular velocity
            float vrel = Vector3.Dot(contact.Normal, body2.Velocity - body1.Velocity);
            //const float b2VelocityThreashold = 0.2f;
            //if (-Math.Abs(vrel) < -1f)
            //{
            contact.Bias += -Math.Max(body1.Restitution, body2.Restitution) * Math.Abs(vrel);
            //}

            // TODO: if world::accumulateImpulses
            // ....
        }

        /// <summary>
        /// Aplica los impulsos necesarios a los cuerpos.
        /// </summary>
        public void ApplyImpulse()
        {
            //TODO: todabia no le encontre el uso que le da en Box2D, ^^ lo usa para la velocidad angular.
            contact.R1 = contact.Position - body1.Location;
            contact.R2 = contact.Position - body2.Location;

            //Velocidad relativa
            //TODO: velocidad angular.
            //b2Vec2 dv = v2 + b2Cross(w2, ccp->r2) - v1 - b2Cross(w1, ccp->r1);
            Vector3 dv = body2.Velocity - body1.Velocity;

            //impulso normal
            float vn = Vector3.Dot(dv, contact.Normal);
            //TODO: from box2d 2.0.1
            float lambda = -contact.MassNormal * (vn - contact.Bias);

            // TODO: from box2d 2.0.1
            float newImpulse = Math.Max(vn + lambda, 0.0f);
            lambda = newImpulse - vn;
            //Aplica impulso al contacto.
            Vector3 Pn = Vector3.Multiply(contact.Normal, lambda);

            // TODO: de box2dlite
            //float dPn = contact.MassNormal * (-vn + contact.Bias);
            //// TODO: if world::accumulateimpulses
            //// else
            //dPn = Math.Max(dPn, 0.0);
            //// Apply contact impulse
            //Vector Pn = dPn * contact.Normal;

            body1.Velocity = Vector3.Subtract(body1.Velocity, Vector3.Multiply(Pn, body1.inverseMass));
            body2.Velocity = Vector3.Add(body2.Velocity, Vector3.Multiply(Pn, body2.inverseMass));
            
            //No entiendo muy bien esta parte.
            // TODO: if world::splitImpulses
            //Velocidad relativa
            //TODO: velocidad angular.
            //dv = this.body2.Velocity - this.body1.Velocity;
            //TODO: Significado de este cross
            //Vector3 tangent = Vector3.Cross(contact.Normal, new Vector3(1.0f, 1.0f, 1.0f));
            //float vt = Vector3.Dot(dv, tangent);

            // TODO: if world::accumulateImpulses
            // else
//inline float Clamp(float a, float low, float high)
//{
//    return Max(low, Min(a, high));
//}
            //TODO esto es ignorado por el momento.
            //float dPt = 0.0f;
            //Aplica impulso al contacto.
            //Vector3 Pt = Vector3.Multiply(tangent, dPt);

            //this.body1.Velocity.Subtract(Vector3.Multiply(Pt, body1.inverseMass));            
            //this.body2.Velocity.Add(Vector3.Multiply(Pt, body2.inverseMass));            
        }
    }
}
