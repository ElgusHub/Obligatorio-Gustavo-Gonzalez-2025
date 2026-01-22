using Web.Models.DTOs.PagoDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models.DTOs.PagoDTO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Web.Models.DTOs.MetodoPagoDTO;
using Web.Models.DTOs.PagoDTO;
using Web.Models.DTOs.PagoRecurrenteDTO;
using Web.Models.DTOs.PagoUnicoDTO;
using Web.Models.DTOs.TipoGastoDTOs;
using Web.Models.DTOs.UsuarioDTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web.Controllers
{
    public class PagoController : Controller
    {

        public string urlBase = "";
        public string urlBaseMetodoPago = "";
        public string urlBaseTipoGasto = "";
        public string urlBaseUsuario = "";

        public PagoController(IConfiguration configuration)
        {
            urlBase = configuration.GetValue<string>("urlBase") + "/PagoWebAPI";
            urlBaseMetodoPago = configuration.GetValue<string>("urlBase") + "/MetodoPagoWebAPI";
            urlBaseTipoGasto = configuration.GetValue<string>("urlBase") + "/TipoGastoWebAPI";
            urlBaseUsuario = configuration.GetValue<string>("urlBase") + "/UsuarioWebAPI";
        }

        // GET: PagoController
        [HttpGet]
        public ActionResult Index()
        {
            string Rol = HttpContext.Session.GetString("Rol");
            if (Rol == null)
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Login", "Usuario");
            }

            IEnumerable<ListadoPagoDTO> listadoPagos = new List<ListadoPagoDTO>();
            try
            {
                HttpClient pago = new HttpClient();
                pago.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                Task<HttpResponseMessage> tarea = pago.GetAsync(urlBase);
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> body = contenido.ReadAsStringAsync();
                    body.Wait();
                    string datos = body.Result;
                    listadoPagos = JsonConvert.DeserializeObject<IEnumerable<ListadoPagoDTO>>(datos);
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
            catch (Exception ex)
            {
                TempData["Error"] = "Error";
            }

            return View(listadoPagos);
        }



        /// <summary>
        /// Obtengo los pagos de usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PagosPorUsuario()
        {
            int? idUsuario = HttpContext.Session.GetInt32("Id");
            string Rol = HttpContext.Session.GetString("Rol");
            
           IEnumerable< ListaPagosPorUsuarioDTO> listaPagos = new List<ListaPagosPorUsuarioDTO>();

            if (Rol == null || (Rol == "Administrador"))
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (idUsuario == null || idUsuario <= 0)
                {
                    TempData["Error"] = "Los datos recibidos no son correctos.";
                    return RedirectToAction("Index", "Home");
                }

                HttpClient pago = new HttpClient();

                pago.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                
                Task<HttpResponseMessage> tarea = pago.GetAsync($"{urlBase}/pagos-usuario");
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> body = contenido.ReadAsStringAsync();
                    body.Wait();
                    string datos = body.Result;
                    listaPagos = JsonConvert.DeserializeObject<IEnumerable<ListaPagosPorUsuarioDTO>>(datos);
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
                else if ((int)respuesta.StatusCode == StatusCodes.Status400BadRequest)
                {
                    TempData["Error"] = "Usted no tiene pagos realizados";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error";
            }
            return View(listaPagos);
        }



        [HttpGet]
        public IActionResult EquiposPorPagosMayorA()
        {
            return View();
        }



        [HttpPost]
        public IActionResult EquiposPorPagosMayorA(decimal? monto)
        {
            IEnumerable<EquiposPorPagosDTO> listaEquipos = new List<EquiposPorPagosDTO>();
            if (HttpContext.Session.GetString("Rol") == null || HttpContext.Session.GetString("Rol") != "Gerente")
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                HttpClient equipo = new HttpClient();
                equipo.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                Task<HttpResponseMessage> tareaDos = equipo.GetAsync($"{urlBase}/equipos-usuarios/{monto}");
                tareaDos.Wait();
                HttpResponseMessage respuestaEquipo = tareaDos.Result;

                if (respuestaEquipo.IsSuccessStatusCode)
                {
                    HttpContent contenidoTipo = respuestaEquipo.Content;
                    Task<string> bodyTipo = contenidoTipo.ReadAsStringAsync();
                    bodyTipo.Wait();
                    string datosTipo = bodyTipo.Result;
                    listaEquipos = JsonConvert.DeserializeObject<IEnumerable<EquiposPorPagosDTO>>(datosTipo);
                }
                else if ((int)respuestaEquipo.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    TempData["Error"] = "Su sesión ha expirado. Vuelva a iniciar sesión.";
                    return RedirectToAction("Login", "Usuario");
                }
                else if ((int)respuestaEquipo.StatusCode == StatusCodes.Status403Forbidden)
                {
                    TempData["Error"] = "No tiene permisos suficientes.";
                    return RedirectToAction("Index", "Home");
                }
                else if ((int)respuestaEquipo.StatusCode == StatusCodes.Status400BadRequest)
                {
                    TempData["Error"] = "No existen equipos que tengan usuarios que hicieron compas con un valor mayor a " + monto.Value;
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ocurrió un error consultando la API de tipos de gasto.";
            }

            return View("VistaEquipos", listaEquipos);

        }


        [HttpGet]
        public IActionResult VistaEquipos()
        {
            return View();
        }



        // GET: PagoController/PagoUnico/Create
        public IActionResult PagoUnico()
        {
            PagoUnicoDTO pagoUnicoDTO = new PagoUnicoDTO();
            IEnumerable<ListadoTipoGastoDTO> listaTipoGasto = new List<ListadoTipoGastoDTO>();
            if (HttpContext.Session.GetString("Rol") == null)
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Login", "Usuario");
            }
            try
            {
                HttpClient tipoGasto = new HttpClient();

                tipoGasto.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                Task<HttpResponseMessage> tareaDos = tipoGasto.GetAsync(urlBaseTipoGasto + "/lista-tipo-gasto");
                tareaDos.Wait();
                HttpResponseMessage respuestaTipoGasto = tareaDos.Result;

                if (respuestaTipoGasto.IsSuccessStatusCode)
                {
                    HttpContent contenidoTipo = respuestaTipoGasto.Content;
                    Task<string> bodyTipo = contenidoTipo.ReadAsStringAsync();
                    bodyTipo.Wait();
                    string datosTipo = bodyTipo.Result;
                    pagoUnicoDTO.TipoGastos = JsonConvert.DeserializeObject<IEnumerable<ListadoTipoGastoDTO>>(datosTipo);
                }
                else if ((int)respuestaTipoGasto.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    TempData["Error"] = "Su sesión ha expirado. Vuelva a iniciar sesión.";
                    return RedirectToAction("Login", "Usuario");
                }
                else if ((int)respuestaTipoGasto.StatusCode == StatusCodes.Status403Forbidden)
                {
                    TempData["Error"] = "No tiene permisos suficientes.";
                    return RedirectToAction("Index", "Home");
                }
                else if ((int)respuestaTipoGasto.StatusCode == StatusCodes.Status400BadRequest)
                {
                    TempData["Error"] = "La lista de Tipo de Gasto esta vacia";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error";
            }

            return View(pagoUnicoDTO);
        }


        // POST: PagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PagoUnico(PagoUnicoDTO pagoUnicoDTO)
        {
            pagoUnicoDTO.UsuarioMail = HttpContext.Session.GetString("Mail");
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient pagoUnico = new HttpClient();

                    pagoUnico.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                    Task<HttpResponseMessage> tarea = pagoUnico.PostAsJsonAsync(urlBase + "/alta-pago-unico", pagoUnicoDTO);
                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;
                    if (respuesta.IsSuccessStatusCode)
                    {
                        TempData["Exito"] = "Alta de pago único fue exitoso";
                        return RedirectToAction("Index", "Pago");
                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status400BadRequest || (int)respuesta.StatusCode == StatusCodes.Status409Conflict)
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> body = contenido.ReadAsStringAsync();
                        body.Wait();
                        string datos = body.Result;
                        TempData["Error"] = datos;
                        return RedirectToAction("PagoUnico", "Pago");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Exito"] = "Error";
            }

            return View(pagoUnicoDTO);
        }


        //GET de Pago recurrente
        [HttpGet]
        public IActionResult PagoRecurrente()
        {
            PagoRecurrenteDTO pagoRecurrenteDTO = new PagoRecurrenteDTO();
            IEnumerable<ListadoTipoGastoDTO> listaTipoGasto = new List<ListadoTipoGastoDTO>();
            if (HttpContext.Session.GetString("Rol") == null)
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Login", "Usuario");
            }
            try
            {
                HttpClient tipoGasto = new HttpClient();

                tipoGasto.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                Task<HttpResponseMessage> tareaDos = tipoGasto.GetAsync(urlBaseTipoGasto + "/lista-tipo-gasto");
                tareaDos.Wait();
                HttpResponseMessage respuestaTipoGasto = tareaDos.Result;

                if (respuestaTipoGasto.IsSuccessStatusCode)
                {
                    HttpContent contenidoTipo = respuestaTipoGasto.Content;
                    Task<string> bodyTipo = contenidoTipo.ReadAsStringAsync();
                    bodyTipo.Wait();
                    string datosTipo = bodyTipo.Result;
                    pagoRecurrenteDTO.TipoGastos = JsonConvert.DeserializeObject<IEnumerable<ListadoTipoGastoDTO>>(datosTipo);
                }
                else if ((int)respuestaTipoGasto.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    TempData["Error"] = "Su sesión ha expirado. Vuelva a iniciar sesión.";
                    return RedirectToAction("Login", "Usuario");
                }
                else if ((int)respuestaTipoGasto.StatusCode == StatusCodes.Status403Forbidden)
                {
                    TempData["Error"] = "No tiene permisos suficientes.";
                    return RedirectToAction("Index", "Home");
                }
                else if ((int)respuestaTipoGasto.StatusCode == StatusCodes.Status400BadRequest)
                {
                    TempData["Error"] = "La lista de Tipo de Gasto esta vacia";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error";
            }

            return View(pagoRecurrenteDTO);
        }

        // POST: PagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PagoRecurrente(PagoRecurrenteDTO pagoRecurrenteDTO)
        {
            pagoRecurrenteDTO.UsuarioMail = HttpContext.Session.GetString("Mail");
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient pagoRecurrente = new HttpClient();

                    pagoRecurrente.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                    Task<HttpResponseMessage> tarea = pagoRecurrente.PostAsJsonAsync(urlBase + "/alta-pago-recurrente", pagoRecurrenteDTO);
                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        TempData["Exito"] = "Alta de pago recurrente fue exitoso";
                        return RedirectToAction("Index", "Pago");
                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status400BadRequest || (int)respuesta.StatusCode == StatusCodes.Status409Conflict)
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> body = contenido.ReadAsStringAsync();
                        body.Wait();
                        string datos = body.Result;
                        TempData["Error"] = datos;
                        return RedirectToAction("PagoRecurrente", "Pago");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error";
            }

            return View(pagoRecurrenteDTO);
        }


       

      
        
        














        
        
        
        
        
        
        
        // GET: PagoController/Edit/5
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Rol") == null || (HttpContext.Session.GetString("Rol") != "Gerente"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: PagoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            if (HttpContext.Session.GetString("Rol") == null || (HttpContext.Session.GetString("Rol") != "Gerente"))
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PagoController/Delete/5
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Rol") == null || (HttpContext.Session.GetString("Rol") != "Gerente"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: PagoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            if (HttpContext.Session.GetString("Rol") == null || (HttpContext.Session.GetString("Rol") != "Gerente"))
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // GET: PagoController/Details/5
        public ActionResult Details(int id)
        {
            BuscarPagoPorIdDTO detallePago = new BuscarPagoPorIdDTO();
            if (HttpContext.Session.GetString("Rol") == null || (HttpContext.Session.GetString("Rol") != "Gerente"))
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
                    throw new ArgumentNullException("El id no es correcto");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }

            return View(detallePago);
        }

        // POST /Filtrar: filtra por fecha 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Filtrar(DateTime fecha)
        {


            IEnumerable<ListaFiltroPagoXFecha> listaPagosFiltrada = new List<ListaFiltroPagoXFecha>();
            if (HttpContext.Session.GetString("Rol") == null || (HttpContext.Session.GetString("Rol") != "Gerente"))
            {
                return RedirectToAction("Index", "Home");
            }
            // Guardo ambas representaciones
            ViewBag.FechaSeleccionada = fecha.ToString("yyyy-MM-dd");   // para el <input type="date">
            ViewBag.FechaSeleccionadaDisplay = fecha.ToString("dd/MM/yyyy"); // para el H1
            try
            {
                if (fecha != null)
                {

                }
                else
                {
                    throw new ArgumentNullException("La fecha no es correcta");
                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }

            return View(listaPagosFiltrada);
        }


        // GET: muestra el formulario y/o resultados si viene ?valor=
        [HttpGet]
        public IActionResult PagoUnicoPorMontoMayorA(decimal? valor)
        {
            IEnumerable<ListaPagoUnicoMayorADTO> listadoPagos = new List<ListaPagoUnicoMayorADTO>();
            if (HttpContext.Session.GetString("Rol") == null || (HttpContext.Session.GetString("Rol") != "Gerente"))
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (valor.HasValue && valor.Value > 0)
                {

                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(listadoPagos);
        }


        // POST: PagoController/PagoUnico/
        [HttpPost]
        public IActionResult PagoUnicoPorMontoMayorA(decimal valor)
        {
            if (HttpContext.Session.GetString("Rol") == null || (HttpContext.Session.GetString("Rol") != "Gerente"))
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (valor <= 0)
                {
                    TempData["Mensaje"] = "El monto no es correcto";
                    return RedirectToAction(nameof(PagoUnicoPorMontoMayorA));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }

            // Redirige al GET 
            return RedirectToAction(nameof(PagoUnicoPorMontoMayorA), new { valor });
        }

    }
}
