using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContAcerta.Contexto;
using ContAcerta.Models;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;

namespace ContAcerta.Controllers
{
    public class PedidoController : Controller
    {
        private DBContext db = new DBContext();
        
        public ActionResult Index()
        {
            return View(db.Pedidos.Where(x => x.BtAtivo == false).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Pedido pedido)
        {
            if (pedido.DataAte.HasValue)
            {
                pedido.DataAte = pedido.DataAte.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            var query = db.Pedidos.Where(x => x.Data >= pedido.DataDe && x.Data <= pedido.DataAte && x.BtAtivo == false);
            return PartialView(query.ToList());
        }

        public ActionResult List()
        {
            return View(db.Pedidos.Where(x => x.BtAtivo == true).ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }
        
        public ActionResult Create()
        {
            var model = new Pedido();
            model.TiposPagamento = TiposPagamentoEnum.Dinheiro;

            return View("Create", model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pedido pedido)
        {
                for (int i=0; i < pedido.Produtos.Count(); i++)
                {
                    pedido.DcPedido += pedido.Produtos[i].DcProduto + ", ";
                }
                DateTime dtRef = DateTime.Now;
                if (pedido.Data == null || pedido.Data < dtRef)
                {
                    pedido.Data = DateTime.Now;
                }
                pedido.TpPagamento = pedido.TiposPagamento.ToString();
                pedido.BtAtivo = true;
                db.Pedidos.Add(pedido);
                db.SaveChanges();
                TempData["notice"] = "Pedido realizado com sucesso!";
                return RedirectToAction("Create");
        }

        public JsonResult PesquisarProdutos(string descricao)
        {
            try
            {
                var produtos = new List<Produto>();
                produtos = db.Produtos.Where(p => p.DcProduto.Contains("/" + descricao + "/")).ToList();
                return Json(produtos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pedido pedido)
        {
            db.Entry(pedido).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("List");
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult GerarRelacaoRelatorioPedidos(Pedido model)
        {
            var relatorioModel = GerarRelatorioPedidos(model);

            return PartialView("_RelatorioModal", relatorioModel);
        }

        private RelatorioViewModel GerarRelatorioPedidos(Pedido model)
        {
            if (model.DataAte.HasValue)
            {
                model.DataAte = model.DataAte.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            var lst = db.Pedidos.Where(x => x.Data >= model.DataDe && x.Data <= model.DataAte).ToList();
            decimal? faturamentoTotal = 0;
            decimal contaLucro = 0;
            decimal? taxaCredTotal = 0;
            decimal? taxaDebTotal = 0;
            decimal lucroTotal = 0;
            decimal totalDespesas = 0;
            for (int i = 0; i < lst.Count(); i++)
            {
                faturamentoTotal += lst[i].Valor;
                if (lst[i].ValorCredito.HasValue)
                {
                    faturamentoTotal += lst[i].ValorCredito;
                }
                if (lst[i].ValorDebito.HasValue)
                {
                    faturamentoTotal += lst[i].ValorDebito;
                }
            }
            for (int i = 0; i < lst.Count(); i++)
            {
                contaLucro += lst[i].Valor;
            }
            for (int i = 0; i < lst.Count(); i++)
            {
                if (lst[i].ValorCredito.HasValue)
                {
                    taxaCredTotal += lst[i].ValorCredito;
                }
            }
            for (int i = 0; i < lst.Count(); i++)
            {
                if (lst[i].ValorDebito.HasValue)
                {
                    taxaDebTotal += lst[i].ValorDebito;
                }
            }

            decimal porcLucro = 30;
            decimal porcDesp = 70;

            lucroTotal = Math.Round((contaLucro * porcLucro / 100), 2);
            totalDespesas = Math.Round((contaLucro * porcDesp / 100), 2);

            var reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Server.MapPath("~/Relatórios/RelatorioPedidos.rdlc");
            var reportDataSource = new ReportDataSource();
            reportDataSource.Name = "RelatorioPedidosDataSet";
            reportDataSource.Value = lst;
            reportViewer.LocalReport.DataSources.Add(reportDataSource);

            reportViewer.LocalReport.SetParameters(new ReportParameter("FaturamentoTotal", "R$" + faturamentoTotal.ToString()));
            reportViewer.LocalReport.SetParameters(new ReportParameter("TaxaCredTotal", "R$" + taxaCredTotal.ToString()));
            reportViewer.LocalReport.SetParameters(new ReportParameter("TaxaDebTotal", "R$" + taxaDebTotal.ToString()));
            reportViewer.LocalReport.SetParameters(new ReportParameter("LucroTotal", "R$" + lucroTotal.ToString()));
            reportViewer.LocalReport.SetParameters(new ReportParameter("TotalDespesas", "R$" + totalDespesas.ToString()));
            reportViewer.LocalReport.SetParameters(new ReportParameter("DataAtual", DateTime.Now.ToShortDateString()));

            var relatorioModel = new RelatorioViewModel();
            relatorioModel.Relatorio = reportViewer;
            return relatorioModel;
        }

        public JsonResult FecharCaixa()
        {
            try
            {
                var pedidos = new List<Pedido>();
                var dinheiro = new List<Pedido>();
                var dtRef = DateTime.Now;
                decimal valTotal = 0;
                pedidos = db.Pedidos.Where(x => x.Data.Month == dtRef.Month && x.Data.Day == dtRef.Day && x.Data.Year == dtRef.Year && x.BtAtivo == true).ToList();
                dinheiro = db.Pedidos.Where(x => x.Data.Month == dtRef.Month && x.Data.Day == dtRef.Day && x.Data.Year == dtRef.Year && x.TpPagamento == "Dinheiro" && x.BtAtivo == true).ToList();
                for (int i = 0; i < pedidos.Count(); i++)
                {
                    valTotal += pedidos[i].Valor;
                }
                for (int i=0; i < pedidos.Count(); i++)
                {
                    pedidos[i].BtAtivo = false;
                }

                db.SaveChanges();
                return Json(valTotal, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
