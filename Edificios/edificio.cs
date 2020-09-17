using System.Collections.Generic;
using Universidad.Salones;
using Universidad.Reservas;
using System;

namespace Universidad.Edificios
{
  public class Edificio
  {
    #region Properties
    public List<Salon> listSalones = new List<Salon>();
    private int clave;
    #endregion Properties

    #region Initialize
    public Edificio()
    {
      clave = 123; // Clave para abrir la interfaz de admin
    }
    #endregion

    #region Methods 
    public void consultarSalonesDisponibles()
    {
      bool res;
      int i, j;
      Console.WriteLine( "Buscando salones disponibles acontinuacion se les mostrará los salones disponibles para reserva... ");
      Console.WriteLine( "====================================" );
      Console.WriteLine( "SALONES DISPONIBLES EN EL EDIFICIO" );
      Console.WriteLine( "====================================" );
      foreach( Salon s in listSalones )
      {
        for( i = 1; i <= 5 ; i++ )
        {
          for( j = 7; j <= 17 ; j+= 2 )
          {
            res = s.verificarReserva( i, j );
            if( res == true )
            {
              Console.Write( "| Dia:     ");
              Console.Write( i );
              Console.WriteLine( "  |");
              Console.Write( "| Hora: ");
              Console.Write( j );
              Console.WriteLine( ":00 |");
              Console.Write( "| ID:    ");
              Console.Write( s.idSalon );
              Console.WriteLine( " |");
              Console.WriteLine( "---------------");
            }
          }
        }
      }
    }

    public bool consultarSalon( int ID, int h, int d )
    {
      bool res;
      foreach( Salon s in listSalones )
      {
        if( s.idSalon == ID )
        {
          res = s.verificarReserva( d, h );
          if( res == false )
          {
            return false;
          }
          else
          {
            return true;
          }
        }
      }
      return false;
    }

    public void imprimirSalon( int ID )
    {
      bool res;
      foreach( Salon s in listSalones )
      {
        if( s.idSalon == ID )
        {
          if( s.luz == true )
          {
            Console.WriteLine( "El salón actualmente está en uso." );
          }
          else
          {
            Console.WriteLine( "El salón actualmente no se encuentra en uso." );
          }
          Console.Write( "Temperatura actual: " );
          Console.WriteLine( s.temperatura );

          for( int i = 1; i <= 5; i++ )
          {
            for( int j = 7; j <= 17; j += 2 )
            {
              res = s.verificarReserva( i, j );
              if( res == true )
              {
                Console.Write( "| Dia:     ");
                Console.Write( i );
                Console.WriteLine( "  |");
                Console.Write( "| Hora: ");
                Console.Write( j );
                Console.WriteLine( ":00 |");
                Console.Write( "| ID:    ");
                Console.Write( s.idSalon );
                Console.WriteLine( " |");
                Console.WriteLine( "---------------");
              }
            }
          }
        }
      }
    }
    public void crearSalones(int n)//Solo se puede modificar la cantidad de salones desde aqui (Ningun tipo de usuario lo hace)
    {
      for( int i = 0; i < n; i++ )
      {
        Salon s = new Salon();
        listSalones.Add(s);
      }
    }

   /* public Salon verSalon( int ID )
    {
      foreach( Salon s in listSalones )
      {
        if( s.idSalon == ID )
        {
          return s;
        }
      }
    }*/

    public void menu(Edificio edif) //GUARDAR USER HASTA CREAR EL SALON PARA IDENTIFICAR QUE TIPO DE USER ES
      {
        int opcUsuario, id, i;
        Salon s = new Salon();
        string opc;
        do
        {
          Console.WriteLine( "=========================================================" );
          Console.WriteLine( " BIENVENIDO AL SISTEMA DE CONFIGURACION DEL EDIFICIO" );
          Console.WriteLine( "=========================================================" );
          Console.WriteLine( "                UNIVERSIDAD JAVERIANA CALI               " );
          Console.WriteLine( "=========================================================");
          Console.WriteLine( "A continuacion si es un usuario digite 1, si es administrador digite la clave: " );
          opcUsuario = Convert.ToInt16(Console.ReadLine());
          if( opcUsuario == clave )
          {
            do
            {
              Console.WriteLine( "=======================");
              Console.WriteLine( " Menu de administrador " );
              Console.WriteLine( "=======================");
              Console.WriteLine( "1) Mostrar salones disponibles" );
              Console.WriteLine( "2) Reservar un salon" );
              Console.WriteLine( "3) Abrir un salon" );
              Console.WriteLine( "4) Cerrar un salon" );
              Console.WriteLine( "5) Poner un salon en mantenimiento" );
              Console.WriteLine( "6) Quitar un salon de mantenimiento " );
              Console.WriteLine( "7) Buscar salon especifico" );//Imprimir salon
              Console.WriteLine( "8) Cambiar la temperatura de un salon" );
              Console.WriteLine( "9) Salir" );
              Console.WriteLine( "Ingrese la opcion que desea: " );
              opc = Console.ReadLine();
              int.TryParse( opc, out i );
              switch (i)
                {
                case 1:
                {
                  consultarSalonesDisponibles();
                  break;
                }
                case 2:
                  Console.WriteLine( "Ingrese el id del salon a reservar: ");
                  id = Convert.ToInt16(Console.ReadLine());
                  s.reservarSalon(edif, id);
                  break;

                case 3:
                   Console.WriteLine( "Ingrese el id del salon que desea abrir: ");
                   id = Convert.ToInt16(Console.ReadLine());
                   s.abrirSalon(edif, id );
                  break;

                case 4:
                  Console.WriteLine( "Ingrese el id del salon que desea cerrar" );
                  id = Convert.ToInt16(Console.ReadLine());
                  s.cerrarSalon(edif, id );
                  break;
                case 5:
                  Console.WriteLine("Ingrese el id del salon que desea poner en mantenimiento: ");
                  id = Convert.ToInt16(Console.ReadLine());
                  s.habilitarMantenimiento(edif, id);
                  break;
                case 6:
                  Console.WriteLine("Ingrese el id del salon cuyo mantenimiento desea deshabilitar: ");
                  id  = Convert.ToInt16(Console.ReadLine());
                  s.deshabilitarMantenimiento(edif, id);
                  break;
                case 7:
                  Console.WriteLine( "Ingrese el id del salon que quiere buscar: ");
                  id = Convert.ToInt16(Console.ReadLine());
                  imprimirSalon(id);
                  break;
                case 8:
                  Console.WriteLine("Case 2");
                  Console.WriteLine( "Ingrese el id del salon a reservar: ");
                  id = Convert.ToInt16(Console.ReadLine());
                  s.cambiarTemperatura(edif, id );
                  break;
                case 9:
                  Console.WriteLine("¡¡Gracias por usar nuestro programa!!" );
                  break;
                default:
                  Console.WriteLine( "Opcion no valida" );
                  break;
              }
            }while( i != 9 );
            break;
          }
          else if( opcUsuario == 1 )
          {
            /*Console.WriteLine( "1) Reservar Salon" );
            Console.WriteLine( "2) Buscar salon" );
            */
            do
            {
              Console.WriteLine( "=======================");
              Console.WriteLine( " Menu de Usuario " );
              Console.WriteLine( "=======================");
              Console.WriteLine( "1) Mostrar salones disponibles" );
              Console.WriteLine( "2) Reservar un salon" );
              Console.WriteLine( "3) Buscar salon especifico" );//Imprimir salon
              Console.WriteLine( "4) Salir" );
              Console.WriteLine( "Ingrese la opcion que desea: " );
              opc = Console.ReadLine();
              int.TryParse( opc, out i );
              switch (i)
                {
                case 1:
                {
                  consultarSalonesDisponibles();
                  
                  break;
                }
                case 2:
                  Console.WriteLine( "Ingrese el id del salon a reservar: ");
                  id = Convert.ToInt16(Console.ReadLine());
                  s.reservarSalon(edif, id);
                  break;
                case 3:
                  Console.WriteLine( "Ingrese el id del salon que quiere buscar: ");
                  id = Convert.ToInt16(Console.ReadLine());
                  imprimirSalon(id);
                  break;
                case 4:
                  Console.WriteLine("¡¡Gracias por usar nuestro programa!!" );
                  break;
                default:
                  Console.WriteLine( "Opcion no valida" );
                  break;
              }
            }while( i != 4);
            break;
          }
        }while( opcUsuario != 1 || opcUsuario != clave );
      }

    #endregion Methods
  }
}