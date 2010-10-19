using System;
using System.Collections.Generic;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;
using AlumnoEjemplos.Piguyis.Colisiones;
using Microsoft.DirectX;
using AlumnoEjemplos.Piguyis.Body;
using AlumnoEjemplos.Piguyis.Fisica;
using TgcViewer;
using AlumnoEjemplos.PiguYis.Matematica;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    public class Escena11 : EscenaBase
    {
        public override void render(float elapsedTime)
        {
            base.render(elapsedTime * 2f);
        }
        protected override void createBodys()
        {
            const float radius = 2.0f;
            const float separationDistance = 12.0f;
            const int numberOfSpheresPerBaseLayer = 7;
            const float initialYLocation = -50.0f;
            const float zOffset = -10f;

            for (int y = 0; y < numberOfSpheresPerBaseLayer; ++y)
            {
                for (int x = 0; x < numberOfSpheresPerBaseLayer - y; ++x)
                {
                    for (int z = 0; z < numberOfSpheresPerBaseLayer - y; ++z)
                    {
                        BodyBuilder builder = new BodyBuilder(
                                                            new Vector3((radius * 2f * x) + (y * radius),
                                                                        initialYLocation + (separationDistance * y),
                                                                        zOffset + (radius * 2 * z) + (y * radius)),
                                                            new Vector3(),
                                                            y != 0 ? 1.0f : float.PositiveInfinity);
                        builder.setBoundingSphere(radius);
                        if (y != 0)
                            builder.setForces(0.0f, -1.0f, 0.0f);
                        bodys.Add(builder.build());
                    }
                }
            }
        }

        public override void initEscena()
        {
            base.initEscena();
            GuiController.Instance.FpsCamera.setCamera(new Vector3(-50.0f, 25.0f, -100.0f), new Vector3(90.0f, -50.0f, 100.0f));
        }

        public override string getTitle()
        {
            return "Escena11 - Motor Fisica";
        }

        public override string getDescription()
        {
            return "Muchas esferas apiladas en forma de piramide caen sobre esferas inmoviles. con restitucion 1.0";
        }
    }
}
