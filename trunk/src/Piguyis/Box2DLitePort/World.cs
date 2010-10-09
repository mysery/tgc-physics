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
    /// Port of the Box2D World class.
    /// </summary>
    public class World
    {
        #region Private Member Variables
       
        private List<RigidBody> rigidBodys;
        private Dictionary<ArbiterKey, Arbiter> arbiters = new Dictionary<ArbiterKey, Arbiter>();

        #endregion Private Member Variables

        #region Object Lifetime

        /// <summary>
        /// Constructs an instance of <see cref="World" />
        /// </summary>
        /// <param name="spheres"></param>
        public World(List<RigidBody> rigidBodys)
        {
            this.rigidBodys = rigidBodys;
        }

        #endregion Object Lifetime

        /// <summary>
        /// Comienzo caliente.
        /// //TODO ver utilidad por Box2D
        /// </summary>
        public bool WarmStarting
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Fase Inicial de manejo de coliciones.
        /// </summary>
        public void CollidePhase()
        {
            //TODO performar esta deteccion O(n^2)
            for (int i = 0; i < rigidBodys.Count; ++i)
            {
                RigidBody bodyOuter = rigidBodys[i];
                for (int j = i + 1; j < rigidBodys.Count; ++j)
                {
                    RigidBody bodyInner = rigidBodys[j];

                    if (float.IsInfinity(bodyOuter.Mass)
                        && float.IsInfinity(bodyInner.Mass))
                    {   
                        continue;
                    }

                    Arbiter arbiter = new Arbiter(this.WarmStarting, 
                                                   CollisionManager.testCollision((BoundingSphere)bodyOuter.BoundingVolume, (BoundingSphere)bodyInner.BoundingVolume),
                                                   bodyOuter, bodyInner);
                    ArbiterKey arbiterKey = new ArbiterKey(bodyOuter, bodyInner);

                    //TODO contact != null es lo mismo que una colision.
                    if (arbiter.Contact != null)
                    {
                        if (!arbiters.ContainsKey(arbiterKey))
                        {
                            arbiters.Add(arbiterKey, arbiter);
                        }
                        else
                        {
                            arbiters[arbiterKey].Update(arbiter.Contact);
                        }
                    }
                    else
                    {
                        if (arbiters.ContainsKey(arbiterKey))
                        {
                            arbiters.Remove(arbiterKey);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// calcula un paso de tiempo en el mundo.
        /// </summary>
        /// <param name="timeStep"></param>
        public void Step(float timeStep)
        {
            //Deteccion de colisiones y armado de arbitros.
            CollidePhase();

            float inverseTimeStep = timeStep > 0f ? (1f / timeStep) : 0f;
            
            //Calculo de fuerzas.            
            foreach (RigidBody rigidBody in rigidBodys)
            {
                if (float.IsInfinity(rigidBody.Mass))
                {
                    continue;
                }
                rigidBody.IntegrateForceSI(timeStep);
            }

            //Perform pre-steps.
            foreach (Arbiter arbiter in arbiters.Values)
            {
                arbiter.PreStep(inverseTimeStep);
            }

            // TODO: joints (encadenamientos y fuerzas de atraccion entre cuerpos)
            //Aca lo agrega el Box2D

            //Perform iterations
            int numberIterations = 50; // TODO: configurar
            for (int i = 0; i < numberIterations; ++i)
            {
                foreach (Arbiter arbiter in arbiters.Values)
                {
                    arbiter.ApplyImpulse();
                }
            }

            //Calcular Velocidades
            foreach (RigidBody rigidBody in rigidBodys)
            {
                rigidBody.IntegrateVelocitySI(timeStep);
            }
        }
    }
}
