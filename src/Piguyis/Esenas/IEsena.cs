using System;
using System.Collections.Generic;
using System.Text;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    interface IEsena
    {
        void initEsena();
        void render(float elapsedTime);
        void closeEsena();
    }
}
