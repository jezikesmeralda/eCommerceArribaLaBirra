using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using dominio;

namespace negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> ListarImagenesPorProducto(int IdProducto)
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, IdProducto, ImagenURL FROM IMAGENES WHERE IdProducto = @id");
                datos.SetearParametro("@id", IdProducto);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdProducto = (int)datos.Lector["IdProducto"];
                    aux.ImagenUrl = (string)datos.Lector["ImagenURL"];
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

        public void AgregarImagen(int IdProducto, string urlImagen)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Imagenes (IdProducto, ImagenUrl) VALUES (@IdProducto, @ImagenUrl)");
                datos.SetearParametro("@IdProducto", IdProducto);
                datos.SetearParametro("@ImagenUrl", urlImagen);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la imagen: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

    }
}
