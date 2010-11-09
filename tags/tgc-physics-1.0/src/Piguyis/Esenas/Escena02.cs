using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena02 : EscenaBase
    {
        public override string GetTitle()
        {
            return "Escena02 - Motor Fisica";
        }

        public override string GetDescription()
        {
            return "Esferas rebotando con velocidad constante. Fila=1 Restitucion= 1.0, Fila=2 Restitucion= 0.9, Fila=3 Restitucion esfera derecha= 1.25 y las otras 0.8";
        }

        protected override void CreateBodys()
        {
            const float radius = 20.0f;

            #region Primer Fila
            // sphere 1.
            BodyBuilder builder1 = new BodyBuilder(new Vector3(-radius * 3, radius * 3, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builder1.SetBoundingSphere(radius);
            Bodys.Add(builder1.Build());

            // sphere 2.
            BodyBuilder builder2 = new BodyBuilder(new Vector3(radius * 3, radius * 3, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builder2.SetBoundingSphere(radius);
            Bodys.Add(builder2.Build());
            
            // sphere 3.
            BodyBuilder builder3 = new BodyBuilder(new Vector3(0f, radius * 3, 0.0f),
                                                    new Vector3(10.0f, 0f, 0.0f), 1.0f);
            builder3.SetBoundingSphere(radius);
            Bodys.Add(builder3.Build());
            #endregion

            #region Segunda Fila
            // sphere 4.
            BodyBuilder builder4 = new BodyBuilder(new Vector3(-radius * 3, 0f, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builder4.SetBoundingSphere(radius);
            builder4.SetRestitution(0.9f);
            Bodys.Add(builder4.Build());

            // sphere 5.
            BodyBuilder builder5 = new BodyBuilder(new Vector3(radius * 3, 0f, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builder5.SetBoundingSphere(radius);
            builder5.SetRestitution(0.9f);
            Bodys.Add(builder5.Build());

            // sphere 6.
            BodyBuilder builder6 = new BodyBuilder(new Vector3(),
                                                    new Vector3(10.0f, 0f, 0.0f), 1.0f);
            builder6.SetBoundingSphere(radius);
            builder6.SetRestitution(0.9f);
            Bodys.Add(builder6.Build());
            #endregion

            #region Tercera Fila
            // sphere 7.
            BodyBuilder builder7 = new BodyBuilder(new Vector3(-radius * 3, -radius * 3, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builder7.SetBoundingSphere(radius);
            builder7.SetRestitution(0.8f);
            Bodys.Add(builder7.Build());

            // sphere 8.
            BodyBuilder builder8 = new BodyBuilder(new Vector3(radius * 3, -radius * 3, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builder8.SetBoundingSphere(radius);
            builder8.SetRestitution(1.25f);
            Bodys.Add(builder8.Build());

            // sphere 9.
            BodyBuilder builder9 = new BodyBuilder(new Vector3(0f, -radius * 3, 0.0f),
                                                    new Vector3(10.0f, 0f, 0.0f), 1.0f);
            builder9.SetBoundingSphere(radius);
            builder9.SetRestitution(0.8f);
            Bodys.Add(builder9.Build());
            #endregion

            
        }
    }
}
