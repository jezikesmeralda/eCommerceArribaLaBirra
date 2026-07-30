using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    internal class CategoriaNegocio
    {
        public Categoria BuscarPorId(int Id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, Nombre, Activo FROM CATEGORIAS WHERE Id = @id");
                datos.SetearParametro("@Id", Id);
                datos.EjecutarLectura();

                if(datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Activa = (bool)datos.Lector["Activa"];

                    return aux;
                }

                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
