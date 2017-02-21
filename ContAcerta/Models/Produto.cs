using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContAcerta.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }

        [Required]
        [Display(Name = "Produto")]
        public string DcProduto { get; set; }

        [Display(Name = "Tipo do Produto")]
        public string TpProduto { get; set; }

        [Required]
        [Display(Name = "Valor do Produto")]
        public decimal ValProduto { get; set; }

        [Required]
        [Display(Name = "Data da Inclusão")]
        public DateTime DtInclusao { get; set; }

        [NotMapped]
        public TiposProdutoEnum TiposProduto { get; set; }
    }
}