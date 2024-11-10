using heladeria.Models;
using heladeria.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace heladeria.Controllers
{
    public class PedidoController : Controller
    {
        private PedidoRepository PedidoRepository { get; set; }

        public PedidoController(PedidoRepository pedidoRepository)
        {
            PedidoRepository = pedidoRepository;
        }

        // GET: PedidoController
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home", new { message = "No sos un usuario" });
            }

            IEnumerable<Pedido> lista = null;
            if(User.Claims.First(c => c.Type == "UNLZRole").Value == "Administrador")
            {
                lista = PedidoRepository.ObtenerTodos();
            }
            else
            {
                int idUsuario = int.Parse(User.Claims.First(c => c.Type == "usuario").Value);
                lista = PedidoRepository.ObtenerPropios(idUsuario);
            }

            return View(lista);
        }

        // GET: PedidoController/Details/5
        public ActionResult Details(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home", new { message = "No sos un usuario" });
            }

            return View(PedidoRepository.ObtenerPorId(id));
        }

        // GET: PedidoController/Create
        public ActionResult Create()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home", new { message = "No sos un usuario" });
            }


            return View(new Pedido());
        }

        // POST: PedidoController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            //try
            //{

            int idUsuario = int.Parse(User.Claims.First(c => c.Type == "usuario").Value);
            Pedido pedido = new Pedido()
            {
                Kilos = int.Parse(collection["Kilos"]),
                IdHelado = int.Parse(collection["IdHelado"]),
                IdUsuarioAlta = idUsuario
            };
                PedidoRepository.Agregar(pedido);

                return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: PedidoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PedidoController/Edit/5
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

        // GET: PedidoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PedidoController/Delete/5
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
