using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace eCommerce
{
    public partial class Home : System.Web.UI.Page
    {
        public List<Producto> ListaProductos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCatalogo();
            }
        }

        private void CargarCatalogo()
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                ListaProductos = negocio.Listar(); // Esto trae productos, marcas, categorías e imágenes de un saque

                repProductos.DataSource = ListaProductos;
                repProductos.DataBind();
            }
            catch(Exception ex)
            {
                // Manejo de errores básico
                Session.Add("error", ex.ToString());
                
                //Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            // Capturamos el ID del producto desde el CommandArgument del botón presionado
            int idProducto = int.Parse(((System.Web.UI.WebControls.Button)sender).CommandArgument);

            // Lógica futura para sumar al carrito de compras en sesión...
        }
    }
}