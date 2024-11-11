namespace heladeria.Models
{
    public class Pedido
    {
        public int? IdPedido { get; set; }

        public int Kilos { get; set; }

        public int IdHelado { get; set; }

        public int IdUsuarioAlta { get; set; }
    }

    public class PedidoCompleto : Pedido
    {
        public string Descripcion { get; set; } = string.Empty;

    }



}
