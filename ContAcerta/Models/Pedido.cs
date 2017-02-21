using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContAcerta.Models
{
    public class Pedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Data do Pedido:")]
        public DateTime Data { get; set; }
        [Display(Name = "Descrição do Pedido:")]
        public string DcPedido { get; set; }
        [Required(ErrorMessage = "É necessário ter ao menos um produto.")]
        [Display(Name = "Valor Total:")]
        public Decimal Valor { get; set; }
        [Display(Name = "Valor Crédito:")]
        public Decimal? ValorCredito { get; set; }
        [Display(Name = "Valor Débito:")]
        public Decimal? ValorDebito { get; set; }
        [Display(Name = "Tipo de Pagamento:")]
        public string TpPagamento { get; set; }
        public bool BtAtivo { get; set; }

        [NotMapped]
        [Display(Name = "Produtos:")]
        public List<Produto> Produtos { get; set; }
        [NotMapped]
        public TiposPagamentoEnum TiposPagamento { get; set; }
        [NotMapped]
        public DateTime? DataDe { get; set; }
        [NotMapped]
        public DateTime? DataAte { get; set; }

        //Hiddens
        [NotMapped]
        public Decimal ValorArmazenado { get; set; }

    }
}