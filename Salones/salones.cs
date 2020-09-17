using System;
using Universidad.Edificios;
using System.Collections.Generic;
using Universidad.Reservas;

namespace Universidad.Salones
{
  public class Salon
  {
    #region Properties 
    public int idSalon; //nombre del salon
    public bool disponible { get; set; } //estado de disponiblidad del salon

    public bool estado { get; set; }  //estado de mantenimiento del salon
    public List<Reserva> listReservas = new List<Reserva>(); //lista de reservas para cada salon
    public int temperatura { get; set; } 
    public bool luz { get; set; }

    public int tiempoActual { get; set; }
   
    #endregion Properties
    
    #region Initialize
    public Salon()
    {
      Random rand = new Random(); 
      idSalon = rand.Next(1000, 10000);
      temperatura = 26; 
      tiempoActual = 0;
      luz = false;
      disponible = true; // "false" es ocupado y "true" es que esta disponible
      estado = false; // "false" es libre y "true" es que esta en mantenimiento
    }
    #endregion 
    
    #region Getter and Setters
    public int Id
    {
      get { return idSalon; }
    }

    #endregion Getter and Setters

    #region Methods 

    

    /*
      Esta funcion reserva el salon en el horario que se busca 
      a partir del nombre(id) ingresado
    */
    public void reservarSalon(Edificio edif, int nombre)
    {
      string day;
      string val1;
      int time, x;
      bool veli, val = false, h;
      foreach( Salon s in edif.listSalones )
      {
        if(s.idSalon == nombre)
        {
          bool flag = false;
          while(flag != true)
          {
          Console.WriteLine( "1) Lunes \n2) Martes \n3) Miercoles \n4) Jueves \n5) Viernes" );
          Console.WriteLine( "Ingrese el número del dia que desea reservar en esta semana: "); 
          day = Console.ReadLine();
          veli = validarDia(day);
          int.TryParse(day, out x); //validador: verifica se haga la conversion 
          if(veli == true ) //Ingresa que sea un int y a su vez que esté en el rango permitido
          {
            while( val != true )
            {          
              Console.WriteLine( "Ingrese la hora en la que desea reservar el salon (7h a 17h): "); 
                time = Convert.ToInt16(Console.ReadLine());
                h = validarHora( time );
                if( h == true )
                {
                  val = true;
                  flag = verificarReserva(x, time);
                  if( flag == true )
                  {
                    Reserva r1 = new Reserva()
                    {
                      hora = time,
                      dia = x
                    };

                    listReservas.Add(r1);
                    Console.WriteLine( "¡La reserva fue exitosa! Esta es la informacion de tu reservada: " );
                    Console.WriteLine( "==================" );
                    Console.Write( "ID Reserva: " );
                    Console.WriteLine( r1.idReserva );
                    Console.Write( "Salon: " );
                    Console.WriteLine( nombre );
                    Console.Write( "Hora: " );
                    Console.WriteLine( r1.hora );
                    Console.Write( "Dia: " );
                    Console.WriteLine( r1.dia );
                    Console.WriteLine( "==================" );
                  }
                  else
                  {
                    Console.WriteLine( "Hubo un problema reservando..." );
                    Console.WriteLine( "El dia o a la hora ingresada el salon se encuentra ocupado. ¿Desea ingresar otra fecha? ");
                    val1 = Console.ReadLine();
                    int comparison = String.Compare(val1, "si", comparisonType: StringComparison.OrdinalIgnoreCase);
                    int comparison2 = String.Compare(val1, "no", comparisonType: StringComparison.OrdinalIgnoreCase);
                    if(comparison == 0)
                    {
                      flag = false;
                      val = false;
                    }
                    else if(comparison2 == 0 )
                    { 
                      Console.WriteLine( "Que tenga un buen dia" );
                      flag = true;
                    }
                    else
                    {
                      Console.WriteLine("Por favor ingrese solo Si o No");
                      //falta el while para saber si se ingresa algo que no sea si o no  
                    }
                  }
                }
                else
                {
                  Console.WriteLine( "Se debe ingresar una hora dentro de los horarios establecidos! Por favor, vuelva a intentarlo.");
                }
              
            }
          }
          else
          {
            Console.WriteLine("Lo ingresado no es valido. Vuelva a intentarlo");
          }
        }
      }
    }
  }


    /* 
       Recibe la informacion del salon que se desea poner en mantenimiento, 
       si este esta disponible su estado pasa a ocupado, debido a que esta
       en mantenimiento. 
    */

    public void habilitarMantenimiento(Edificio edif, int i) 
    {
      int time, x;
      bool res, flag = false, val = false, d, h;
      string day;

      while( flag != true )
      {
        Console.WriteLine( "1) Lunes \n2) Martes \n3) Miercoles \n4) Jueves \n5) Viernes" );
        Console.WriteLine( "Ingrese el numero del dia que desea hacer el mantenimiento: " );
        day = Console.ReadLine();
        d = validarDia(day);
        int.TryParse(day, out x);
        if( d == true )
        {
          while( val != true )  
          {
            Console.WriteLine( "Ingrese el horario en el cual desea hacer el mantenimiento (7h a 17h): " );
            time =  Convert.ToInt16(Console.ReadLine());
            h = validarHora( time );  
            if( h == true )
            {
              res = edif.consultarSalon(i, x, time );
              if( res == true )
              {
                disponible = false; //el salon que ocupado por mantenimiento
                estado = true;
                val = true;
                flag = true;
                Console.WriteLine( "El procedimiento fue exitoso. El salon ingresado quedo en mantenimiento: " );
                Console.Write( "Salon: " );
                Console.Write( i );
                Console.WriteLine( " [EN MANTENIMIENTO]" );

              }
              else
              {
                Console.WriteLine( "Hubo un problema con el procedimiento..." );
                Console.WriteLine("El salon ingresado no existe en el edificio o se encuentra ocupado.");
              }
            }
            else
            {
              Console.WriteLine( "Se debe ingresar una hora dentro de los horarios establecidos! Por favor, vuelva a intentarlo.");
            }
          }
        }
        else
        {
          Console.WriteLine( "Por favor, ingrese el numero del dia correcto. Vuelva a intentarlo." );
        }
      }
    }

    /* 
      Recibe la informacion de el salon, al cual se quiere desahibilitar
      el mantenimiento y lo deja disponible
    */

    public void deshabilitarMantenimiento(Edificio edif, int i) 
    {
      int time, x;
      bool res, flag = false, val = false, d, h;
      string day;

      while( flag != true )
      {
        Console.WriteLine( "1) Lunes \n2) Martes \n3) Miercoles \n4) Jueves \n5) Viernes" );
        Console.WriteLine( "Ingrese el numero del dia en el cual el salon que busca quedo en mantenimiento: " );
        day = Console.ReadLine();
        int.TryParse(day, out x);
        d = validarDia( day );
        if( d == true)
        {
          while( val != true)
          {
            
              Console.WriteLine( "Ingrese el horario en el cual puso en mantenimiento el salon que esta buscando (7h a 17h): " );
              time =  Convert.ToInt16(Console.ReadLine());
              h = validarHora( time );
              if( h == true )
              {
                res = edif.consultarSalon(i, x, time );
                if( res == true && estado == true )
                {
                  estado = false; //el salon en mantenimiento queda libre
                  Console.WriteLine( "¡El procedimiento fue exitoso! El salon ingresado ahora esta disponible para reservar." );
                  val = true;
                  flag = true;
                }
                else
                {
                  Console.WriteLine( "Hubo un problema con el procedimiento..." );
                  Console.WriteLine( "El salon ingresado no existe en el edificio o no se encuentra en mantenimiento." );
                }  
              }
              else
              {
                Console.WriteLine( "Se debe ingresar una hora dentro de los horarios establecidos! Por favor, vuelva a intentarlo." );
              }
          }
        }
        else
        {
          Console.WriteLine( "Por favor, ingrese el numero del dia correcto. Vuelva a intentarlo." );
        }
      } 
    }
 
    /*
      Cambia la temperatura de un salon, de acuerdo a la informacion
      del salon y la reserva ingresada
    */

    public void cambiarTemperatura(Edificio edif, int ID)
    {
      int time, x,t;
      bool res, flag = false, flagg = false, val = false, d, h; 
      string day;

      while( flag != true )
      {
        Console.WriteLine( "1) Lunes \n2) Martes \n3) Miercoles \n4) Jueves \n5) Viernes" );
        Console.WriteLine( "Ingrese el numero del dia que desea modificar la temperatura: " );
        day = Console.ReadLine();
        int.TryParse(day, out x);
        d = validarDia( day );
        if( d == true )
        {
          while( val != false )
          {
              Console.WriteLine( "Ingrese el horario en el cual desea modificar la temperatura (7h a 17h): " );
              time =  Convert.ToInt16(Console.ReadLine());
              h = validarHora( time );
              if( h == true )
              {
                res = edif.consultarSalon(ID, x, time );
                if( res == true && estado == true )
                {
                  while( flagg != true )
                  {
                    Console.WriteLine( "Ingrese la temperatura que desea: " );
                    t = Convert.ToInt16(Console.ReadLine());   
                    if( t >= 16 && t <= 26 )
                    {
                      temperatura = t; 
                      flagg = true;
                      val = true;
                      flag = true;
                      Console.WriteLine( "¡El procedimiento fue exitoso! La temperatura es optima para la reserva." );
                    }
                    else
                    {
                      Console.WriteLine( "La temperatura ingresada no es permetida en el salon,Intentelo de nuevo" );
                    }
                  }
                }
              }
              else
              {
                Console.WriteLine( "Se debe ingresar una hora dentro de los horarios establecidos! Por favor, vuelva a intentarlo." );
              }
          }
        }
        else
        {
          Console.WriteLine( "Por favor, ingrese el numero del dia correcto. Vuelva a intentarlo." );
        }
      } 
    }

 /* 
  Abrir salon corresponde al cambio de estado de las luces y temperatura
  de un determinado salon. 
        
  */
    public void abrirSalon(Edificio edif, int ID )
    {
      int time, x;
      bool res, flag = false, val = false, d, h;
      string day;

      while( flag != true )
      {
        Console.WriteLine( "1) Lunes \n2) Martes \n3) Miercoles \n4) Jueves \n5) Viernes" );
        Console.WriteLine( "Ingrese el numero del dia que desea abrir el salon: " );
        day = Console.ReadLine();
        int.TryParse(day, out x);
        d = validarDia( day );
        if( d == true )
        {
          while( val != true )    
          {
              Console.WriteLine( "Ingrese el horario en el cual tiene la reserva del salon: " );
              time =  Convert.ToInt16(Console.ReadLine());
              h = validarHora( time );
              if( h == true )
              {
                actual();
                time *= 100;
                res = edif.consultarSalon( ID, time, x );
                if( res == false && tiempoActual == time )
                {
                  temperatura = 23;
                  luz = true; 
                  listReservas.RemoveAt(0);
                }
                else
                {
                  Console.WriteLine( "Hubo un error con el procedimiento..." );
                  Console.WriteLine("El salon o reserva ingresado no existe en el edificio o se encuentra ocupado.");
                }
                flag = true;
                val = true;
              }
              else
              {
                Console.WriteLine( "Se debe ingresar una hora dentro de los horarios establecidos! Por favor, vuelva a intentarlo." );
              }
        }
      }
      else
      {
        Console.WriteLine( "Por favor, ingrese el numero del dia correcto. Vuelva a intentarlo." );
      }
    } 
  }
     public void cerrarSalon(Edificio edif, int ID)
    {
       int time, x;
      bool res, flag = false, val = false, d, h;
      string day;

      while( flag != true )
      {
        Console.WriteLine( "1) Lunes \n2) Martes \n3) Miercoles \n4) Jueves \n5) Viernes" );
        Console.WriteLine( "Ingrese el numero del dia que desea cerrar un salon ingresado: " );
        day = Console.ReadLine();
        int.TryParse(day, out x);
        d = validarDia( day );
        if( d ==  true )
        {
          while( val != true )  
          {
              Console.WriteLine( "Ingrese el horario en el cual desea cerrar el salon: " );
              time =  Convert.ToInt16(Console.ReadLine());
              h = validarHora( time );
              if( h == true )
              {
                res = edif.consultarSalon(ID, x, time );
                if( res == true )
                {
                  //el salon que ocupado por mantenimiento
                  estado = true;
                  temperatura = 26;
                  luz = false; 
                }
                else
                {
                  Console.WriteLine( "Hubo un error con el procedimiento..." );
                  Console.WriteLine( "El salon ingresado no existe en el edificio o se encuentra ocupado." );
                }
              }
              else
              {
                Console.WriteLine( "Se debe ingresar una hora dentro de los horarios establecidos! Por favor, vuelva a intentarlo." );
              }
          }
        }
        else
        {
          Console.WriteLine( "Por favor, ingrese el numero del dia correcto. Vuelva a intentarlo." );
        }
      } 
    }

    public void actual()
    {
      foreach( Reserva r in listReservas )
      {
        tiempoActual = r.hora * 100;
        break;
      }
    }
    //Verificadores

    /* 
       Recibe la informacion del dia y hora de la reserva que se quiere hacer
       y verifica en la lista de reservas si esta disponible.
    */  
    public bool verificarReserva(int d, int h) //dia y hora
    {
      foreach( Reserva r in listReservas)
      {
        if( r.dia == d && r.hora == h )
        {
          return false;
        }
      }
      return true;
    } 

    public bool validarDia( string day )
    {
      int x;
      bool veli;
      veli = int.TryParse(day, out x); //validador: verifica se haga la conversion 
      if(veli == true && x <= 5 && x >= 1 )
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public bool validarHora( int t )
    {
      if( t >= 7 && t <= 17 )
      {
        return true;
      }
      return false;
    }
        
    #endregion Methods
  }
}