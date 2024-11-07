namespace HeladeriaManager.Entidades
{
    public class Producto
    {
        public int IdHelado { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Kilos { get; set; }
        public int IdUsuarioAlta { get; set; }
        public DateTime FechaAlta { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdUsuarioBaja { get; set; }
        public DateTime? FechaBaja { get; set; }


    }
}
