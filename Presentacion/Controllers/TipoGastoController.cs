using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models.DTOs.UsuarioDTOs;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.Arm;
using Web.Models.DTOs.AuditoriaDTO;
using Web.Models.DTOs.TipoGastoDTOs;

namespace Web.Controllers
{
    public class TipoGastoController : Controller
    {
        public string urlBaseAuditoria = "";
        public string urlBaseTipoGasto = "";


        public TipoGastoController(IConfiguration configuration)
        {
            urlBaseAuditoria = configuration.GetValue<string>("urlBase") + "/AuditoriaWebAPI";
            urlBaseTipoGasto = configuration.GetValue<string>("urlBase") + "/TipoGastoWebAPI";

        }


        // GET: TipoGastoController
        public ActionResult Index()
        {
            IEnumerable<ListadoTipoGastoDTO> ListadoTipoGastos = new List<ListadoTipoGastoDTO>();
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || rol != "Administrador")
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                HttpClient tipoGasto = new HttpClient();
                tipoGasto.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                Task<HttpResponseMessage> tarea = tipoGasto.GetAsync($"{urlBaseTipoGasto}/lista-tipo-gasto");
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> body = contenido.ReadAsStringAsync();
                    body.Wait();
                    string datos = body.Result;
                    ListadoTipoGastos = JsonConvert.DeserializeObject<IEnumerable<ListadoTipoGastoDTO>>(datos);
                }
                else if ((int)respuesta.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    TempData["Error"] = "Su sesión ha expirado. Vuelva a iniciar sesión.";
                    return RedirectToAction("Login", "Usuario");
                }
                else if ((int)respuesta.StatusCode == StatusCodes.Status403Forbidden)
                {
                    TempData["Error"] = "No tiene permisos suficientes.";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrió un error consultando la API de tipos de gasto.";
            }

            return View(ListadoTipoGastos);
        }

        // GET: TipoGastoController/Details/5
        public ActionResult AuditoriaTipoGasto(int id)
        {
           IEnumerable< AuditoriaDTO> detalleAuditoria = new List< AuditoriaDTO>();
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || rol != "Administrador")
            {
                TempData["Error"] = "No tiene permisos para ver esta auditoría.";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "El ID proporcionado no es válido.";
                    return RedirectToAction("Index");
                }
                HttpClient tipoGasto = new HttpClient();
                tipoGasto.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                Task<HttpResponseMessage> tarea = tipoGasto.GetAsync($"{urlBaseAuditoria}/auditoria-tipoGasto/{id}");
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> body = contenido.ReadAsStringAsync();
                    body.Wait();
                    string datos = body.Result;
                    detalleAuditoria = JsonConvert.DeserializeObject<IEnumerable<AuditoriaDTO>>(datos);

                }
                else if ((int)respuesta.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    TempData["Error"] = "No fue posible obtener la auditoría del tipo de gasto.";
                    return RedirectToAction("Index");
                }
                else if ((int)respuesta.StatusCode == StatusCodes.Status403Forbidden)
                {
                    TempData["Error"] = "Ocurrió un error procesando la auditoría.";
                    return RedirectToAction("Index");
                }
                else if ((int)respuesta.StatusCode == StatusCodes.Status400BadRequest)
                {
                    TempData["Error"] = $"El Tipo de gasto con id {id} no cuenta con auditoria";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "Hubo un problema consultando la API de Tipo de Gasto.";
            }

            return View(detalleAuditoria);
        }





















        // GET: TipoGastoController/Create
        public ActionResult Create()
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || rol != "Administrador")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: TipoGastoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoGastoDTO tipoGastoDTO)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || rol != "Administrador")
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Mensaje = "Datos incorrectos";
                }
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Error";
            }
            return View();

        }

        // GET: TipoGastoController/Edit/5
        public ActionResult Edit(int id)
        {
            DetalleTipoGastoDTO detalleTipoGasto = new DetalleTipoGastoDTO();
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || rol != "Administrador")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (id > 0)
                {

                }
                else
                {
                    throw new ArgumentException("El id no es correcto");
                }
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Error";
            }

            return View(detalleTipoGasto);
        }

        // POST: TipoGastoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DetalleTipoGastoDTO detalleTipoGasto)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || rol != "Administrador")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (id > 0 && ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(detalleTipoGasto);
        }

        // GET: TipoGastoController/Delete/5
        public ActionResult Delete(int id)
        {
            DetalleTipoGastoDTO detalleTipoGasto = new DetalleTipoGastoDTO();
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || rol != "Administrador")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (id > 0)
                {

                }
                else
                {
                    throw new ArgumentException("El id no es correcto");
                }
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Error";
            }

            return View(detalleTipoGasto);
        }

        // POST: TipoGastoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DetalleTipoGastoDTO detalleTipoGasto)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || rol != "Administrador")
            {
                return RedirectToAction("Index", "Home");
            }
            //Validación básica
            if (id <= 0)
            {
                ViewBag.Mensaje = "El id no es correcto";
                return View(detalleTipoGasto);
            }
            try
            {
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
                return View(detalleTipoGasto);
            }

            //Intento de eliminación con auditoría de resultado
            bool eliminadoOk = false;
            string? error = null;

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Error";
            }
        return View(detalleTipoGasto);
        }
    }
}
