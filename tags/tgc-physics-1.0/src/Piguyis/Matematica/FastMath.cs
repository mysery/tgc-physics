using System;

namespace AlumnoEjemplos.PiguYis.Matematica
{
    public class FastMath
    {
        private const float Tolerance = 1e-5f;

        private const float Pi = (float)Math.PI;
        private const float Pi2 = Pi * 2;
        private const float Pid2 = Pi / 2;
        private const float Pisqr = Pi * Pi;
        private const float Pid180 = Pi / 180;

        private const float B = 4 / Pi;
        private const float C = -4 / Pisqr;

        // Extra precision
        private const float P = 0.225f;
/*
        private const float Q = 0.775f;
*/

        #region Setters & Getters

        public static float PI
        {
            get { return Pi; }
        }

        public static float PI2
        {
            get { return Pi2; }
        }

        public static float PID2
        {
            get { return Pid2; }
        }

        public static float PISQR
        {
            get { return Pisqr; }
        }

        public static float PID180
        {
            get { return Pid180; }
        }

        #endregion

        public static bool IsEqualWithinTol(float val1, float val2)
        {
            return Math.Abs(val1 - val2) < Tolerance;
        }

        public static bool MinusTolerance(float val1)
        {
            return val1 < Tolerance;
        }

        /// <summary>
        /// Aproximacion rapida de la funcion trigonometrica Sin()
        /// </summary>
        /// <param name="radians">Angulo en radianes</param>
        public static float Sin(float radians)
        {
            // Los valores de PI deben estar entre -PI y PI para poder utilizar la siguiente aproximacion
            if (radians > PI)
            {
                radians -= PI2;
            }
            if (radians < -PI)
            {
                radians += PI2;
            }

            float y = B * radians + C * radians * Math.Abs(radians);

            // Extra precision
            y = P * (y * Math.Abs(y) - y) + y;   // Q * y + P * y * Math.Abs(y)

            return y;
        }


        /// <summary>
        /// Aproximacion rapida de la funcion trigonometrica Cos()
        /// </summary>
        /// <param name="radians">Angulo en radianes</param>
        public static float Cos(float radians)
        {
            return Sin(radians + PI / 2); // Cos(x) = Sin(x + PI / 2)
        }

        /// <summary>
        /// Optimizacion sacada de http://www.koders.com/java/fidDA85948E47E0F011F9CC1F7CBF3A3BB04CA0D722.aspx
        /// </summary>
        /// <param name="fValue"></param>
        /// <returns></returns>
        public static float Acos(float fValue)
        {
            if (-1.0f < fValue)
            {
                if (fValue < 1.0f)
                    return (float)Math.Acos(fValue);

                return 0.0f;
            }

            return PI;
        }
    }    
}
