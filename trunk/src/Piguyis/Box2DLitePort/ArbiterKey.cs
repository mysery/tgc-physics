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
using AlumnoEjemplos.Piguyis.Colisiones;
using AlumnoEjemplos.Piguyis.Body;

namespace AlumnoEjemplos.Piguyis.Box2DLitePort
{
    /// <summary>
    /// Buscar mejor implementacion en el futuro para manjar los arbritros.
    /// </summary>
    public class ArbiterKey : IEqualityComparer<ArbiterKey>
    {
        private RigidBody body1;
        private RigidBody body2;

        /// <summary>
        /// Creates an instance of <see cref="ArbiterKey" />
        /// </summary>
        public ArbiterKey(RigidBody body1, RigidBody body2)
        {
            this.body1 = body1;
            this.body2 = body2;
        }

        #region IEqualityComparer<ArbiterKey> Members

        /// <summary>
        /// Equals override.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals(this, obj as ArbiterKey);
        }

        /// <summary>
        /// GetHashCode override().
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        /// <summary>
        /// Determines whether the arbiters are equal
        /// </summary>
        public bool Equals(ArbiterKey x, ArbiterKey y)
        {
            if (object.ReferenceEquals(x.body1, y.body1)
                && (object.ReferenceEquals(x.body2, y.body2)))
            {
                return true;
            }

            if (object.ReferenceEquals(x.body1, y.body2)
                && (object.ReferenceEquals(x.body2, y.body1)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Implementation of GetHashCode.
        /// </summary>
        public int GetHashCode(ArbiterKey obj)
        {
            unchecked
            {
                return body1.GetHashCode() ^ body2.GetHashCode();
            }
        }

        #endregion
    }
}
