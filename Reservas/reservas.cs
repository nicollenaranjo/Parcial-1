using System;

namespace Universidad.Reservas
{
    public class Reserva
    {
        #region Properties
        public int idReserva;
        public int hora;
        public int dia;

        #endregion Properties 

        #region Initialize 
        public Reserva()
        {
            Random rand = new Random(); 
            idReserva = rand.Next(100, 1000);
        }
        #endregion initialize
    }
}