using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eCommerce
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Esto es lo que pasa cuando haces clic
            string textoBusqueda = txtBuscar.Text;
            // Aquí después programaremos la redirección a una página de resultados
            Response.Write("Buscaste: " + textoBusqueda);
        }
    }
}