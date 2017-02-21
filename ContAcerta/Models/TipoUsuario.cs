using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContAcerta.Models
{
    public class TipoUsuario
    {
        public int TipoUsuarioId { get; set; }
        public string TipoUsuarioDc { get; set; }
        public DateTime DtInclusao { get; set; }
    }
}