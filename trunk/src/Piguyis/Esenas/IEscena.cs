using System;
using System.Collections.Generic;
using System.Text;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    interface IEscena
    {
        void initEscena();
        void render(float elapsedTime);
        void closeEscena();
    }
}