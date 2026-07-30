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
        public List<Imagen> ListarPorIdsDeProductos(List<int> idsProductos)
        {
            List<Imagen> listaImagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            // Si la lista de IDs viene vacía, ni te molestes en ir a la base de datos
            if (idsProductos == null || idsProductos.Count == 0)
                return listaImagenes;

            try
            {
                // Convertimos la lista de IDs en un string separado por comas (ej: "1, 2, 5, 8")
                string idsConcatenados = string.Join(",", idsProductos);

                // La magia del IN: Trae todas las imágenes de esos productos en UN SOLO VIAJE
                datos.SetearConsulta("SELECT Id, IdProducto, ImagenUrl FROM Imagenes WHERE IdProducto IN (" + idsConcatenados + ")");
                datos.EjecutarLectura();

                // AQUÍ USAMOS EL WHILE: Recorremos las filas que trajo la consulta masiva
                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdProducto = (int)datos.Lector["IdProducto"]; // ¡Este es el puente vital!

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    listaImagenes.Add(aux);
                }

                return listaImagenes;
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
