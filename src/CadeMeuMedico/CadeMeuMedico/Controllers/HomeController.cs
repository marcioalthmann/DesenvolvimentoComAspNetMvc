using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CadeMeuMedico.Models;

namespace CadeMeuMedico.Controllers
{
    public class HomeController : Controller
    {
        private EntidadesCadeMeuMedicoBD db = new EntidadesCadeMeuMedicoBD();

        public ActionResult Login()
        {
            ViewBag.Title = "Seja bem vindo(a)";
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Especialidades = new SelectList(db.Especialidades, "IDEspecialidade", "Nome");
            ViewBag.Cidades = new SelectList(db.Cidades, "IDCidade", "Nome");

            return View();
        }

        public ActionResult Pesquisar(Pesquisa pesquisa)
        {
            var medicos = from m in db.Medicos
                where m.IDCidade == pesquisa.IdCidade && m.IDEspecialidade == pesquisa.IdEspecialidade
                select new ResultadoPesquisa {Nome = m.Nome, Crm = m.CRM};

            return Json(medicos, JsonRequestBehavior.AllowGet);
        }
    }


}
