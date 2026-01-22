using Web.Models.DTOs.UsuarioDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Web.Models.DTOs.PagoDTO;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        
        public string urlBase = "";

        public UsuarioController(IConfiguration configuration)
        {
            urlBase = configuration.GetValue<string>("urlBase")+ "/UsuarioWebAPI";

        }

        // GET: UsuarioController
        public IActionResult Index()

        {
            IEnumerable<ListadoUsuarioDTO> listadoUausrios = new List<ListadoUsuarioDTO>();
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol))
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                HttpClient usuario = new HttpClient();
                usuario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                Task<HttpResponseMessage> tarea = usuario.GetAsync(urlBase);
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> body = contenido.ReadAsStringAsync();
                    body.Wait();
                    string datos = body.Result;
                    listadoUausrios = JsonConvert.DeserializeObject<IEnumerable<ListadoUsuarioDTO>>(datos);
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

            return View(listadoUausrios);

        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient cliente = new HttpClient();
                    Task<HttpResponseMessage> solicitud = cliente.PostAsJsonAsync(urlBase+"/Login", usuarioLoginDTO);
                    solicitud.Wait();
                    HttpResponseMessage respuesta = solicitud.Result;
                    if (respuesta.IsSuccessStatusCode)
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string>body = contenido.ReadAsStringAsync();
                        string datos = body.Result;
                        UsusarioLogueadoDTO ususarioLogueadoDTO=JsonConvert.DeserializeObject<UsusarioLogueadoDTO>(datos);
                        if (ususarioLogueadoDTO != null)
                        {
                            HttpContext.Session.SetInt32("Id", ususarioLogueadoDTO.Id);
                            HttpContext.Session.SetString("Rol", ususarioLogueadoDTO.Rol);
                            HttpContext.Session.SetString("Token", ususarioLogueadoDTO.Token);
                            HttpContext.Session.SetString("Mail", ususarioLogueadoDTO.Mail);

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["Error"] = "Datos incorrectos.";
                        }
                    }
                    else
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> body = contenido.ReadAsStringAsync();
                        string datos = body.Result;
                        TempData["Error"] = datos;
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error.";
            }
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(nameof(Login));
        }


        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            BuscarUsuarioPorIdDTO detalleUsuarioDTO = new BuscarUsuarioPorIdDTO();
            
            if (HttpContext.Session.GetString("Rol") == null || (HttpContext.Session.GetString("Rol") != "Administrador"))
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if(id <= 0)
                {
                    return BadRequest("Id incorrecto");
                }
                HttpClient usuario = new HttpClient();
                usuario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                Task<HttpResponseMessage> tarea = usuario.GetAsync($"{urlBase}/usuarioPorId/{id}");
                tarea.Wait();

                HttpResponseMessage respuesta = tarea.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> body = contenido.ReadAsStringAsync();
                    body.Wait();
                    string datos = body.Result;
                    detalleUsuarioDTO = JsonConvert.DeserializeObject<BuscarUsuarioPorIdDTO>(datos);
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

            return View(detalleUsuarioDTO);
        }
            


        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            AltaUsuarioDTO usuarioDTO = new AltaUsuarioDTO();
            //var rol = HttpContext.Session.GetString("Rol");
            //if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administrador"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            try
            {

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }


            return View(usuarioDTO);
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AltaUsuarioDTO usuarioDTO)
        {
            //var rol = HttpContext.Session.GetString("Rol");
            //if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administrador"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            try
            {
                if (ModelState.IsValid)
                {
                    //Hago el llamado a la API
                    HttpClient usuario = new HttpClient();
                    //Task<HttpResponseMessage> tarea = usuario.PostAsJsonAsync("https://localhost:7086/api/UsuarioWebAPI", usuarioDTO);
                    Task<HttpResponseMessage> tarea = usuario.PostAsJsonAsync($"{urlBase}/Alta", usuarioDTO);

                    //Le digo que espere a obtener el llamado
                    tarea.Wait();
                    //Acceso a la response
                    HttpResponseMessage respuesta = tarea.Result;
                    //Pregunto si el codigo esta en el rango del 200
                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status400BadRequest || (int)respuesta.StatusCode == StatusCodes.Status409Conflict)
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> body = contenido.ReadAsStringAsync();
                        body.Wait();
                        string datos = body.Result;
                        ViewBag.Mensaje = datos;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }

            return RedirectToAction(nameof(Index));
        }



        
        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            BuscarUsuarioPorIdDTO detalleUsuarioDTO = new BuscarUsuarioPorIdDTO();
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || (rol != "Administrador"))
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if(id <= 0)
                {
                    TempData["Error"] = "El id ingresado no es correcto";
                    return RedirectToAction("Index", "Home");
                }
                HttpClient usuario = new HttpClient();
                usuario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                Task<HttpResponseMessage> tarea = usuario.GetAsync($"{urlBase}/usuarioPorId/{id}");
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> body = contenido.ReadAsStringAsync();
                    body.Wait();
                    string datos = body.Result;
                    detalleUsuarioDTO = JsonConvert.DeserializeObject<BuscarUsuarioPorIdDTO>(datos);
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
                    TempData["Error"] = "No se encontro el usuario.";
                    return RedirectToAction("Index", "Usuario");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error";
            }

            return View(detalleUsuarioDTO);
        }




        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BuscarUsuarioPorIdDTO dto)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || rol != "Administrador")
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");

            }

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Los datos recibidos no son correctos";
                return View(dto); // volver a la vista con errores
            }

            try
            {
                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                Task<HttpResponseMessage> tarea = cliente.PutAsJsonAsync($"{urlBase}/Contrasenia", dto);
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    string datos = respuesta.Content.ReadAsStringAsync().Result;
                    var resultado = JsonConvert.DeserializeObject<dynamic>(datos);
                    string nuevaPass = resultado.nuevaContrasena;

                    TempData["Exito"] = nuevaPass;
                    return RedirectToAction(nameof(Index));
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
                else
                {
                    TempData["Error"] = "Error al cambiar la contraseña";
                    return View(dto);
                }
            }
            catch(Exception ex) 
            {
                TempData["Error"] = "Error";
            }
                return View(dto);
        }














        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administrador"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(rol) || !(rol == "Gerente" || rol == "Administrador"))
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
    }
}
