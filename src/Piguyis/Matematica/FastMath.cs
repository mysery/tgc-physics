using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace AlumnoEjemplos.PiguYis.Matematica
{
    public class FastMath
    {
        private const float Tolerance = 1e-8F;

        private const float pi = (float)Math.PI;
        private const float pi2 = pi * 2;
        private const float pid2 = pi / 2;
        private const float pisqr = pi * pi;
        private const float pid180 = pi / 180;

        private const float B = 4 / pi;
        private const float C = -4 / pisqr;

        // Extra precision
        private const float P = 0.225f;
        private const float Q = 0.775f;

        #region Setters & Getters

        public static float PI
        {
            get { return pi; }
        }

        public static float PI2
        {
            get { return pi2; }
        }

        public static float PID2
        {
            get { return pid2; }
        }

        public static float PISQR
        {
            get { return pisqr; }
        }

        public static float PID180
        {
            get { return pid180; }
        }

        #endregion

        public static bool IsEqualWithinTol(double val1,
            double val2, double tolerance)
        {
            return Math.Abs(val1 - val2) < tolerance;
        }

        /// <summary>
        /// Aproximacion rapida de la funcion trigonometrica Sin()
        /// </summary>
        /// <param name="degrees">Angulo en radianes</param>
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
        /// <param name="degrees">Angulo en radianes</param>
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
