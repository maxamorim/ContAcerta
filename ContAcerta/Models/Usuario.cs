using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContAcerta.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string UsuarioDc { get; set; }
        public DateTime DtInclusao { get; set; }
        public int TipoUsuario { get; set; }
    }
}