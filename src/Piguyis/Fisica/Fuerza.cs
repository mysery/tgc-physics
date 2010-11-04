using Microsoft.DirectX;

namespace AlumnoEjemplos.Piguyis.Fisica
{
    public class Fuerza
    {
        #region Variables

        private Vector3 _vector;
        
        #endregion Variables

        #region Constructor

        public Fuerza()
        {
            this._vector = new Vector3();
        }

        public Fuerza(float valueX, float valueY, float valueZ)
        {
            this._vector = new Vector3(valueX, valueY, valueZ);
        }

        public Fuerza(Vector3 pvector)
        {
            this._vector = pvector;
        }

        #endregion Constructor

        #region Accessors

        public Vector3 Vector
        {
            get { return _vector; }
            set { _vector = value; }
        }

        #endregion Accessors

        #region Operadores

        /// <summary>
        /// Overloaded addition operator. Adds the right hand
        ///  operand to the left hand operand.
        /// </summary>
        /// <param name="lhs">The left-hand operand</param>
        /// <param name="rhs">The right-hand operand</param>
        /// <returns>
        /// Result of addition, leaving the operands unchanged.
        /// </returns>
        public static Fuerza operator +(Fuerza lhs, Fuerza rhs)
        {
            return new Fuerza(lhs.Vector + rhs.Vector);
        }
        
        /// <summary>
        /// Overloaded addition operator. Adds the right hand
        ///  operand to the left hand operand.
        /// </summary>
        /// <param name="lhs">The left-hand operand</param>
        /// <param name="rhs">The right-hand operand</param>
        /// <returns>
        /// Result of addition, leaving the operands unchanged.
        /// </returns>
        public static Fuerza operator *(Fuerza lhs, double rhs)
        {
            return new Fuerza(lhs.Vector * (float)rhs);
        }

        #endregion Operadores
    }
}
