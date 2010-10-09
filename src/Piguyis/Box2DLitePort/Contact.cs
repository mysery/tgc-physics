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
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;

namespace AlumnoEjemplos.Piguyis.Box2DLitePort
{
    // TODO: combinacion de Box2D's b2Manifold y b2ContactPoint.
    /// <summary>
    /// Representa el punto de contacto entre dos objetos.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// The contact point.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// Normal for the contact.
        /// </summary>
        public Vector3 Normal;

        /// <summary>
        /// TODO: En 2DBox se usan estos contacts para variar otras fuerzas, 
        /// ahora esta simplificado a un solo punto.
        /// Puntos de contacto de los cuerpos
        /// </summary>
        public Vector3 ContactPoint;
        //public Vector3 ContactPoint2;
        
        /// <summary>
        /// The separation.
        /// </summary>
        public float Separation;

        /// <summary>
        /// Accumulated normal impulse.
        /// </summary>
        public float AccumulatedNormalImpulse;

        /// <summary>
        /// Accumulated tangent impulse.
        /// </summary>
        public float AccumulatedTangentImpulse;

        /// <summary>
        /// Accumulated normal impulse for position bias.
        /// </summary>
        public float AccumulatedNormalImpulseForPositionBias;

        /// <summary>
        /// Mass normal.
        /// </summary>
        public float MassNormal;

        /// <summary>
        /// Mass tangent.
        /// </summary>
        public float MassTangent;

        /// <summary>
        /// Bias
        /// </summary>
        public float Bias;

        // TODO: entender para que se utiliza en 2DBox.
        /// <summary>
        /// diferencia del punto de contacto y el objeto 1
        /// </summary>
        public Vector3 R1;
        /// <summary>
        /// diferencia del punto de contacto y el objeto 2
        /// </summary>
        public Vector3 R2;

    }
}
