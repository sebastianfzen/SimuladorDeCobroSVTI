using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cotizadorFull.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Granel()
        {

            return View();
        }

        public ActionResult Break()
        {


            return View();
        }

        //Listarlos
        public JsonResult cargaServicios()
        {
            Models.CotizarDAL co = new Models.CotizarDAL();

            return Json(co.retornarServicios(null));
        }
        //Calcularlos
        // tarr, ca20, ca40, ,to20,to40, almacenaje,if20,if40, impor_expor,tons_otro,cant_otro, tipo_carga
        public JsonResult calculoServicio(int tar, int ca20, int ca40, int to20, int to40, int almacenaje, bool if20, bool if40, char impor_expor, int tons_otro, int cant_otro, string tipo_carga, DateTime desde, DateTime hasta)
        {
            Models.CotizarDAL co = new Models.CotizarDAL();
            if (almacenaje!=0)
            {
                return Json(co.calcularServicio(almacenaje, ca20, ca40, to20, to40, if20, if40, impor_expor, tons_otro, cant_otro, tipo_carga, desde, hasta));
            }

            return Json(co.calcularServicio(tar, ca20, ca40, to20, to40, if20, if40, impor_expor, tons_otro, cant_otro, tipo_carga, desde, hasta)); 
        
        }

    }
}