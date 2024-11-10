using heladeria.Models;
using heladeria.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace heladeria.Controllers
{
    public class ProductoController : Controller
    {
        private ProductoRepository ProductoRepository { get; set; }

        public ProductoController(ProductoRepository productoRepository)
        {
            ProductoRepository = productoRepository;
        }


        // GET: ProductoController
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error","Home", new { message = "No sos un usuario" });                
            }


            if (User.Claims.First(c => c.Type == "UNLZRole").Value != "Administrador")
            {
                return RedirectToAction("Error","Home", new { message = "No sos admin" });
            }

            return View(ProductoRepository.ObtenerTodos());
        }

        // GET: ProductoController/Details/5
        public ActionResult Details(int id)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home", new { message = "No sos un usuario" });
            }


            if (User.Claims.First(c => c.Type == "UNLZRole").Value != "Administrador")
            {
                return RedirectToAction("Error", "Home", new { message = "No sos admin" });
            }

            return View(ProductoRepository.ObtenerPorId(id));
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home", new { message = "No sos un usuario" });
            }


            if (User.Claims.First(c => c.Type == "UNLZRole").Value != "Administrador")
            {
                return RedirectToAction("Error", "Home", new { message = "No sos admin" });
            }

            return View(new Producto());
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Producto producto = new Producto()
                {
                    Descripcion = collection["Descripcion"],
                    Kilos = int.Parse(collection["Kilos"]),
                    IdUsuarioAlta = 3, //admin
                    FechaAlta = DateTime.Now
                };
                ProductoRepository.Agregar(producto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home", new { message = "No sos un usuario" });
            }


            if (User.Claims.First(c => c.Type == "UNLZRole").Value != "Administrador")
            {
                return RedirectToAction("Error", "Home", new { message = "No sos admin" });
            }

            var producto = ProductoRepository.ObtenerPorId(id);
            return View(producto);
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Producto producto = new Producto()
                {
                    IdHelado = int.Parse(collection["IdHelado"]),
                    Descripcion = collection["Descripcion"],
                    Kilos = int.Parse(collection["Kilos"]),
                    IdUsuarioAlta = 0,
                    FechaAlta = DateTime.Now
                };
                ProductoRepository.Actualizar(producto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home", new { message = "No sos un usuario" });
            }


            if (User.Claims.First(c => c.Type == "UNLZRole").Value != "Administrador")
            {
                return RedirectToAction("Error", "Home", new { message = "No sos admin" });
            }

            var producto = ProductoRepository.ObtenerPorId(id);
            return View(producto);
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                ProductoRepository.Eliminar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
