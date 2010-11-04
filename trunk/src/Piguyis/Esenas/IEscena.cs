using System;

namespace AlumnoEjemplos.Piguyis.Esenas
{
    interface IEscena
    {
        String GetTitle();
        String GetDescription();
        void InitEscena();
        void Render(float elapsedTime);
        void CloseEscena();
    }
}
