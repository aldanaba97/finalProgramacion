using FinalProgramacion.Models;
using FinalProgramacion.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProgramacion.Controllers
{
    public class ExamenController : Controller
    {
        // GET: Examen
        public ActionResult AltaExamen()
        {
            List<MateriaEscuela> lista = AccesoDatos.listaMateria();
            List<SelectListItem> ItemsMateria = lista.ConvertAll(ma =>
            {
                return new SelectListItem()
                {
                    Text = ma.nombre,
                    Value = ma.id.ToString(),
                    Selected = false

                };
            }
            );

            ViewBag.item = ItemsMateria;
            return View();
           
        }
        [HttpPost]
    public ActionResult AltaExamen(examen modelo)
    {
        if (ModelState.IsValid)
        {
            bool resultado = AccesoDatos.InsertarNuevoExamen(modelo);
            if (resultado)
            {
                   
                    return  RedirectToAction("Listado", "Examen"); 
            }
            else
            {
                return View(modelo);
            }
        }


        return View(modelo);

     }

        public ActionResult Listado()
        {
            List<listaExamenes> lista = AccesoDatos.ListadoExamenes();
            return View(lista);
        }
        public ActionResult Reporte()
        {
            List<Reporte> promedio= AccesoDatos.PromedioDeAprobados();
            return View(promedio);
        }

    }

}
