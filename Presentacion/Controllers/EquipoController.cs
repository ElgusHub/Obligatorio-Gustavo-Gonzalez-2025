using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models.DTOs.EquipoDTO;

namespace Web.Controllers
{
    public class EquipoController : Controller
    {
       
        // GET: EquipoController
        public ActionResult Index()
        {
            IEnumerable<EquipoDTOs> listaEquipo = new List<EquipoDTOs>();
            try
            {

            }
            catch (Exception ex) 
            {
                ViewBag.Mensaje = "Error";
            }
            return View(listaEquipo);
        }

        // GET: EquipoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EquipoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EquipoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EquipoDTOs equipoDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
                return View(equipoDTO);
        }

        // GET: EquipoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EquipoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EquipoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EquipoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
