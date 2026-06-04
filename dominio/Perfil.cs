using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
   enum Rol     {
        Administrador,
        Cliente,    
        Vendedor
    }
    public class Perfil
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
