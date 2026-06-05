using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    internal class ProductoNegocio
    {
        //Agregar imagen a producto, agregar marcar a producto y completar los metodos ABM con ambos.
        public List<Producto> listar()
        {
            // corregir la consulta SQL para incluir el IdCategoria y el Nombre de la categoria, imagenURL y marca,
            // utilizando un JOIN entre las tablas Productos y Categorias.
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT Id, Nombre, Descripcion, Precio, Stock, Categoria FROM Productos");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();

                    if (!(datos.Lector["Id"] is DBNull))
                        aux.Id = (int)datos.Lector["Id"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["Precio"];

                    if(!(datos.Lector["Stock"] is DBNull))
                        aux.Stock = (int)datos.Lector["Stock"];
                    /*
                    if(!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        aux.Categoria = new Categoria();

                        if(!(datos.Lector["Nombre"] is DBNull))
                             aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        else
                    }
                        aux.Categoria = ()datos.Lector["IdCategoria"];
                    */
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
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
