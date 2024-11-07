using System.ComponentModel.DataAnnotations;

namespace heladeria.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } 

        public string MailUsuario { get; set; }

        public string? GoogleIdentificador { get; set; }
    }






}
