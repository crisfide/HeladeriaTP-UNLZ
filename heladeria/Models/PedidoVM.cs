using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace heladeria.Models
{
    public class PedidoVM
    {
 
        public Pedido pedido{ get; set; }
        public SelectList listaSabores { get; set; }
        
    }
}
