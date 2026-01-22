using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models.DTOs.MetodoPagoDTO;

namespace Web.Controllers
{
    public class MetodoPagoController : Controller
    {
        
        // GET: MetodoPagoController
        public ActionResult Index()
        {
            IEnumerable<ListadoMetodoPagoDTO> listadoMetodoPagos = new List<ListadoMetodoPagoDTO>();
            try
            {

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }

            return View(listadoMetodoPagos);
        }

        // GET: MetodoPagoController/Details/5
        public ActionResult Details(int id)
        {
            DetalleMetodoPagoDTO detalleMetodoPagoDTO = new DetalleMetodoPagoDTO();

            try
            {
                if(id > 0)
                {

                }
                else
                {
                    throw new ArgumentException("El id no es correcto");
                }
            }
            catch(Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }

            return View(detalleMetodoPagoDTO);
        }

        // GET: MetodoPagoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MetodoPagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MetodoPagoDTO metodoPagoDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.Mensaje = "Datos Incorrectos";
                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View();
        }

        // GET: MetodoPagoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MetodoPagoController/Edit/5
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

        // GET: MetodoPagoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MetodoPagoController/Delete/5
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
