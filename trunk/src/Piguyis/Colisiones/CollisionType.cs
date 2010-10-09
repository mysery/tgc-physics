// Copyright Gary Evans 2006-2008

using System;
using System.Collections.Generic;
using System.Text;

namespace AlumnoEjemplos.Piguyis.Colisiones
{
    /// <summary>
    /// Holds information on whether a collision has occurred.
    /// </summary>
    public enum CollisionType
    {
        /// <summary>
        /// The objects aren't in contact. They may be within
        ///  touching distance, but are moving away from each other.
        /// </summary>
        None,
        /// <summary>
        /// The objects are within touching distance, their
        ///  relative velocity is zero, they are not forced
        ///  together but happen to be in close proximity.
        /// </summary>
        /// <remarks>May be useful for many body collision
        /// or for resolving stacking</remarks>
        TouchingContact,
        /// <summary>
        /// The objects are within touching distance, their
        ///  relative velocity is zero, and they are being
        ///  forced together.
        /// </summary>
        ForcedContact,
        /// <summary>
        /// The objects are within touching distance and the
        ///  relative velocity of the bodies is towards each other.
        /// </summary>
        Collision,
        /// <summary>
        /// The objects are intersecting (physically impossible
        ///  for rigid bodies)
        /// </summary>
        Intersection
    }
}
