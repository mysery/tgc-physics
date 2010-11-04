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

using System.Collections.Generic;
using AlumnoEjemplos.Piguyis.Colisiones;
using AlumnoEjemplos.Piguyis.Body;
using AlumnoEjemplos.Piguyis.Estructuras.Octree;
using System.Collections;
using Microsoft.DirectX;

namespace AlumnoEjemplos.Piguyis.Box2DLitePort
{
    /// <summary>
    /// Port of the Box2D World class.
    /// </summary>
    public class World
    {
        #region Private Member Variables
       
        private Octree _octreeRigidBodys;
        private readonly List<RigidBody> _rigidBodysList;
        private readonly List<Arbiter> _arbiters = new List<Arbiter>();
        private const float MaxRadius = 5f;
        private const float MinValue = -5f;
        private const float MaxValue = 5f;
        private const int ImpulseIterations = 10; // TODO: configurar

        #endregion Private Member Variables

        #region Object Lifetime

        /// <summary>
        /// Constructs an instance of <see cref="World" />
        /// </summary>
        /// <param name="rigidBodys"></param>
        public World(List<RigidBody> rigidBodys)
        {
            this._octreeRigidBodys = new Octree(MaxValue, MinValue, MaxValue, MinValue, MaxValue, MinValue, rigidBodys.Count + 20);
            foreach (RigidBody body in rigidBodys)
	        {
                this._octreeRigidBodys.AddNode(body.Location, body);
	        }
            this._rigidBodysList = rigidBodys;
        }

        /// <summary>
        /// Constructs an instance of <see cref="World" />
        /// </summary>
        public World()
        {
            this._octreeRigidBodys = new Octree(MaxValue, MinValue, MaxValue, MinValue, MaxValue, MinValue, 20);
            this._rigidBodysList = new List<RigidBody>(20);
        }

        #endregion Object Lifetime

        public void AddBody(RigidBody body)
        {
            if (_rigidBodysList.Count > this._octreeRigidBodys.top.maxItems)
            {
                this._octreeRigidBodys = new Octree(MaxValue, MinValue, MaxValue, MinValue, MaxValue, MinValue, _rigidBodysList.Count + 21);
                foreach (RigidBody b in _rigidBodysList)
                    this._octreeRigidBodys.AddNode(b.Location, b);
            }

            this._octreeRigidBodys.AddNode(body.Location, body);
            this._rigidBodysList.Add(body);
        }

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
            foreach (RigidBody bodyPivot in _rigidBodysList)
            {
                ArrayList nearList = _octreeRigidBodys.GetNodes(bodyPivot.Location, bodyPivot.BoundingVolume.GetRadius() * MaxRadius);
                foreach (RigidBody bodyNear in nearList)
                {
                    if (bodyPivot.Equals(bodyNear) ||
                        (float.IsInfinity(bodyPivot.Mass)
                        && float.IsInfinity(bodyNear.Mass)))
                    {   
                        continue;
                    }
                    Contact contact = CollisionManager.TestCollision(bodyPivot.BoundingVolume,
                                                                     bodyNear.BoundingVolume,
                                                                     bodyPivot.Velocity,
                                                                     bodyPivot.Aceleracion);


                    //TODO contact == null No hay colision.
                    if (contact == null) continue;
                    
                    Arbiter arbiter = new Arbiter(this.WarmStarting,
                                                  contact,
                                                  bodyPivot, bodyNear);
                    _arbiters.Add(arbiter);
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
            if ((bool)TgcViewer.GuiController.Instance.Modifiers.getValue("colisionDetect"))
                CollidePhase();

            float inverseTimeStep = timeStep > 0f ? (1f / timeStep) : 0f;
            
            //Calculo de fuerzas.            
            foreach (RigidBody rigidBody in _rigidBodysList)
            {
                if (float.IsInfinity(rigidBody.Mass))
                {
                    continue;
                }
                rigidBody.IntegrateForceSI(timeStep);
            }
            if ((bool)TgcViewer.GuiController.Instance.Modifiers.getValue("applyPhysics"))
            {
                //Perform pre-steps.
                foreach (Arbiter arbiter in _arbiters)
                {
                    arbiter.PreStep(inverseTimeStep);
                }

                // TODO: joints (encadenamientos y fuerzas de atraccion entre cuerpos)
                //Aca lo agrega el Box2D

                //Perform iterations
                for (int i = 0; i < ImpulseIterations; ++i)
                {
                    foreach (Arbiter arbiter in _arbiters)
                    {
                        arbiter.ApplyImpulse();
                    }
                }
            }
            //Calcular Velocidades
            foreach (RigidBody rigidBody in _rigidBodysList)
            {
                if (float.IsInfinity(rigidBody.Mass))
                {
                    continue;
                }
                Vector3 oldLocation = new Vector3(rigidBody.Location.X, rigidBody.Location.Y, rigidBody.Location.Z);
                rigidBody.IntegrateVelocitySI(timeStep);
                //if (Vector3.Subtract(oldLocation, rigidBody.Location).LengthSq() > 25f)
                //{
                    _octreeRigidBodys.RemoveNode(oldLocation, rigidBody);
                    _octreeRigidBodys.AddNode(rigidBody.Location, rigidBody);                    
                //}
            }

            _arbiters.Clear();
        }
    }
}
