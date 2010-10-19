using System;
using System.Collections.Generic;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;
using AlumnoEjemplos.Piguyis.Colisiones;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;
using AlumnoEjemplos.Piguyis.Fisica;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena02 : EscenaBase
    {
        public override string getTitle()
        {
            return "Escena02 - Motor Fisica";
        }

        public override string getDescription()
        {
            return "no implementado";
        }

        protected override void createBodys()
        {
            const float radius = 20.0f;

            #region Primer Fila
            // sphere 1.
            BodyBuilder builder1 = new BodyBuilder(new Vector3(-radius * 3, radius * 3, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builder1.setBoundingSphere(radius);
            bodys.Add(builder1.build());

            // sphere 2.
            BodyBuilder builder2 = new BodyBuilder(new Vector3(radius * 3, radius * 3, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builder2.setBoundingSphere(radius);
            bodys.Add(builder2.build());
            
            // sphere 3.
            BodyBuilder builder3 = new BodyBuilder(new Vector3(0f, radius * 3, 0.0f),
                                                    new Vector3(10.0f, 0f, 0.0f), 1.0f);
            builder3.setBoundingSphere(radius);
            bodys.Add(builder3.build());
            #endregion

            #region Segunda Fila
            // sphere 4.
            BodyBuilder builder4 = new BodyBuilder(new Vector3(-radius * 3, 0f, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builder4.setBoundingSphere(radius);
            builder4.setRestitution(0.8f);
            bodys.Add(builder4.build());

            // sphere 5.
            BodyBuilder builder5 = new BodyBuilder(new Vector3(radius * 3, 0f, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builder5.setBoundingSphere(radius);
            builder5.setRestitution(0.8f);
            bodys.Add(builder5.build());

            // sphere 6.
            BodyBuilder builder6 = new BodyBuilder(new Vector3(),
                                                    new Vector3(10.0f, 0f, 0.0f), 1.0f);
            builder6.setBoundingSphere(radius);
            builder6.setRestitution(0.8f);
            bodys.Add(builder6.build());
            #endregion

            #region Tercera Fila
            // sphere 7.
            BodyBuilder builder7 = new BodyBuilder(new Vector3(-radius * 3, -radius * 3, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builder7.setBoundingSphere(radius);
            builder7.setRestitution(1.8f);
            bodys.Add(builder7.build());

            // sphere 8.
            BodyBuilder builder8 = new BodyBuilder(new Vector3(radius * 3, -radius * 3, 0.0f),
                                                    new Vector3(), float.PositiveInfinity);
            builder8.setBoundingSphere(radius);
            builder8.setRestitution(0.8f);
            bodys.Add(builder8.build());

            // sphere 9.
            BodyBuilder builder9 = new BodyBuilder(new Vector3(0f, -radius * 3, 0.0f),
                                                    new Vector3(10.0f, 0f, 0.0f), 1.0f);
            builder9.setBoundingSphere(radius);
            builder9.setRestitution(0.8f);
            bodys.Add(builder9.build());
            #endregion

            
        }
    }
}
