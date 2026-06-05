using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{

    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }

        public Categoria Categoria { get; set; }

        //Es necesario agregar ImagenURL, haciendo clase imagen.
        //Es necesario agregar marca? siendo marca tambien una clase.

    }
}
