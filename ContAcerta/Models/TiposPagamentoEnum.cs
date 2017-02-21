using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContAcerta.Models
{
    public enum TiposPagamentoEnum
    {
        [Display(Name = "Dinheiro")]
        Dinheiro = 1,

        [Display(Name = "Débito")]
        Debito = 2,

        [Display(Name = "Crédito")]
        Crédito = 3,
    }
}