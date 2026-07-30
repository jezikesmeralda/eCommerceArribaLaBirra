using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace negocio
{
    public class ProductoNegocio
    {
        public List<Producto> Listar()
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT P.Id, P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.Stock, P.IdMarca, M.Nombre AS NombreMarca, P.IdCategoria, C.Nombre AS NombreCategoria FROM PRODUCTOS P LEFT JOIN MARCAS M ON P.IdMarca = M.Id LEFT JOIN CATEGORIAS C ON P.IdCategoria = C.Id WHERE P.Activo = 1");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();

                    if (!(datos.Lector["Id"] is DBNull))
                        aux.Id = (int)datos.Lector["Id"];

                    if (!(datos.Lector["Codigo"] is DBNull))
                        aux.Codigo = (string)datos.Lector["Codigo"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["Precio"];

                    if(!(datos.Lector["Stock"] is DBNull))
                        aux.Stock = (int)datos.Lector["Stock"];
                    
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        aux.Categoria = new Categoria();
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        aux.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];
                    }

                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        aux.Marca = new Marca();
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                        aux.Marca.Nombre = (string)datos.Lector["NombreMarca"];
                    }

                    listaProductos.Add(aux);
                }

                datos.CerrarConexion();

                if (listaProductos.Count > 0)
                {
                    // Extraemos todos los IDs de los productos que acabamos de traer
                    List<int> ids = listaProductos.Select(p => p.Id).ToList();

                    // Instanciamos el negocio de imágenes
                    ImagenNegocio imagenNegocio = new ImagenNegocio();

                    // Llamamos a tu método masivo pasándole la lista entera de IDs
                    List<Imagen> listaImagenesMasiva = imagenNegocio.ListarPorIdsDeProductos(ids);

                    // Asociamos las imágenes a cada producto en memoria RAM
                    foreach (Producto prod in listaProductos)
                    {
                        // Filtramos de la lista masiva las que coincidan con el ID del producto actual
                        prod.Imagenes = listaImagenesMasiva.Where(img => img.IdProducto == prod.Id).ToList();
                    }
                }

                return listaProductos;
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

        public void AgregarProducto(Producto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO PRODUCTOS(Codigo, Nombre, Descripcion, Precio, Stock, IdCategoria, IdMarca) VALUES (@Codigo, @Nombre, @Descripcio, @Precio, @Stock, @Categoria, @Marca)");
                datos.SetearParametro("@Codigo", nuevo.Codigo);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Descripcion", nuevo.Descripcion);
                datos.SetearParametro("@Precio", nuevo.Precio);
                datos.SetearParametro("@Stock", nuevo.Stock);
                datos.SetearParametro("@Categoria", nuevo.Categoria != null ? nuevo.Categoria.Id : (object)DBNull.Value);
                datos.SetearParametro("@Marca", nuevo.Marca != null ? nuevo.Marca.Id : (object)DBNull.Value);
                //datos.SetearParametro("@Categoria)", nuevo.Categoria);
                //datos.SetearParametro("@Marca", nuevo.Marca);

                datos.EjecutarAccion();
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

        public void ModificarProducto(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE PRODUCTOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, Stock = @Stock, IdCategoria = @Categoria, IdMarca = @Marca WHERE Id = @Id");
                datos.SetearParametro("@Codigo", producto.Codigo);
                datos.SetearParametro("@Nombre", producto.Nombre);
                datos.SetearParametro("@Descripcion", producto.Descripcion);
                datos.SetearParametro("@Precio", producto.Precio);
                datos.SetearParametro("@Stock", producto.Stock);
                datos.SetearParametro("@Categoria", producto.Categoria != null ? producto.Categoria.Id : (object)DBNull.Value);
                datos.SetearParametro("@Marca", producto.Marca != null ? producto.Marca.Id : (object)DBNull.Value);
                //datos.SetearParametro("@Categoria", producto.Categoria);
                //datos.SetearParametro("@Marca", producto.Marca);

                datos.EjecutarAccion();
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

        public void eliminarLogico(int id)
        {
            
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE PRODUCTOS set Activo = 0 WHERE Id = @Id");
                datos.SetearParametro("@Id", id);
                datos.EjecutarAccion();
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

        public void eliminarFisico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("DELETE FROM PRODUCTOS WHERE Id = @Id");
                datos.SetearParametro("@Id", id);
                datos.EjecutarAccion();
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

        public List<Producto> BuscarProducto(string campo, string criterio, string filtro)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT P.Id, P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.Stock,
                            P.IdMarca, M.Nombre AS NombreMarca,
                            P.IdCategoria, C.Nombre AS NombreCategoria
                            FROM PRODUCTOS P
                            LEFT JOIN MARCAS M ON M.Id = P.IdMarca
                            LEFT JOIN CATEGORIAS C ON C.Id = P.IdCategoria WHERE 1=1  ";

                if (campo == "Nombre")
                {
                    if (criterio == "Contiene")
                        consulta += " AND P.Nombre LIKE '%" + filtro + "%'";
                    else if (criterio == "Comienza con")
                        consulta += " AND P.Nombre LIKE '" + filtro + "%'";
                }
                else if (campo == "Marca")
                {
                    consulta += " AND M.Nombre LIKE '%" + filtro + "%'";
                }
                else if (campo == "Precio")
                {
                    if (criterio == "Mayor a")
                        consulta += " AND P.Precio > " + filtro;
                    else if (criterio == "Menor a")
                        consulta += " AND P.Precio < " + filtro;
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (datos.Lector["Codigo"] is DBNull) ? "" : (string)datos.Lector["Codigo"];
                    aux.Nombre = (datos.Lector["Nombre"] is DBNull) ? "" : (string)datos.Lector["Nombre"];
                    aux.Descripcion = (datos.Lector["Descripcion"] is DBNull) ? "" : (string)datos.Lector["Descripcion"];
                    aux.Precio = (datos.Lector["Precio"] is DBNull) ? 0 : (decimal)datos.Lector["Precio"];

                    if (!(datos.Lector["Stock"] is DBNull))
                        aux.Stock = (int)datos.Lector["Stock"];

                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        aux.Marca = new Marca();
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                        if (!(datos.Lector["NombreMarca"] is DBNull))
                            aux.Marca.Nombre = (string)datos.Lector["NombreMarca"];
                    }

                    if (!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        aux.Categoria = new Categoria();
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        if (!(datos.Lector["NombreCategoria"] is DBNull))
                            aux.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];
                    }

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
