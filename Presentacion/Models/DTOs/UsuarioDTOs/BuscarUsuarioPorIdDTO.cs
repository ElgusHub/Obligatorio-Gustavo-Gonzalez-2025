using System.ComponentModel;

namespace Web.Models.DTOs.UsuarioDTOs
{
    public class BuscarUsuarioPorIdDTO
    {
        [DisplayName("Id de usuario")]
        public int Id { get; set; }
        //[DisplayName("Nombre de usuario")]
        //public string Nombre { get; set; }
        //public string Apellido { get; set; }
        [DisplayName("Email de usuario")]
        public string Mail { get; set; }
        


    }
}
