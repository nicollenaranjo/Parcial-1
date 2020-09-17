using System;
using Universidad.Salones;
using Universidad.Reservas;
using Universidad.Edificios;

namespace Universidad
{
    class Program
    {
        static void Main(string[] args)
        {
            Edificio edif = new Edificio();
            edif.crearSalones( 1 );
            edif.menu(edif);
        }
    }
}
