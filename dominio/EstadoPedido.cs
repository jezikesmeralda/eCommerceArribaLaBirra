using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class EstadoPedido
    {
        //talvez lo mejor es que esto este en la base de datos y no aca . porque parece una tabla intermeia

        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    //Pendiente
//Confirmado
//Preparando
//Enviado
//Entregado
//Cancelado
}
