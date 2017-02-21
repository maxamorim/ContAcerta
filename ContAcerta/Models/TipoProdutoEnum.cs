using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContAcerta.Models
{
    public enum TiposProdutoEnum
    {
        [Display(Name = "Lanche")]
        Lanche = 1,

        [Display(Name = "Bebida")]
        Bebida = 2,

        [Display(Name = "Sobremesa")]
        Sobremesa = 3,
    }
}