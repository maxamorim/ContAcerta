using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContAcerta.Models
{
    public class Itens
    {
        public int Id { get; set; }
        public Pedido Pedido { get; set; }
        public int Quantidade { get; set; }
        public string Produto { get; set; }
        public Decimal ValorUnitartio { get; set; }
    }
}