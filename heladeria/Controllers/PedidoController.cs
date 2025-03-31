using heladeria.Models;
using heladeria.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace heladeria.Controllers
{
    public class PedidoController : Controller
    {
        private PedidoRepository PedidoRepository { get; set; }
        private ProductoRepository ProductoRepository { get; set; }

        public PedidoController(PedidoRepository pedidoRepository, ProductoRepository productoRepository)
        {
            PedidoRepository = pedidoRepository;
            ProductoRepository = productoRepository;
        }

        // GET: PedidoController
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home", new { message = "No sos un usuario" });
            }

            IEnumerable<PedidoCompleto> lista = null;
            if (User.Claims.First(c => c.Type == "UNLZRole").Value == "Administrador")
            {
                lista = PedidoRepository.ObtenerTodos();
            }
            else
            {
                int idUsuario = int.Parse(User.Claims.First(c => c.Type == "usuario").Value);
                lista = PedidoRepository.ObtenerPropios(idUsuario);

            }

            //agregar descripcion a los productos
            foreach (var pedido in lista)
            {
                Producto prod = ProductoRepository.ObtenerPorId(pedido.IdHelado);
                pedido.Descripcion = prod.Descripcion;
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


            PedidoVM pedidoVM = new PedidoVM();
            pedidoVM.pedido = null;
            /*pedidoVM.listaSabores = new List<SelectListItem>();
            var sabores = ProductoRepository.ObtenerTodos();
            foreach (var sabor in sabores)
            {
                pedidoVM.listaSabores.Add(new SelectListItem { Value = sabor.IdHelado.ToString(), Text = sabor.Descripcion });
            }*/
            pedidoVM.listaSabores = new SelectList(ProductoRepository.ObtenerTodos(), "IdHelado", "Descripcion");
            return View(pedidoVM);
        }


        // POST: PedidoController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {

                int idUsuario = int.Parse(User.Claims.First(c => c.Type == "usuario").Value);
                Pedido pedido = new Pedido()
                {
                    Kilos = int.Parse(collection["pedido.Kilos"]),
                    IdHelado = int.Parse(collection["pedido.IdHelado"]),
                    IdUsuarioAlta = idUsuario
                };

                var prod = ProductoRepository.ObtenerPorId(int.Parse(collection["pedido.IdHelado"]));
                prod.Kilos -= int.Parse(collection["pedido.Kilos"]);
                if(prod.Kilos<0)
                {
                    return RedirectToAction("Error", "Home", new { message = "No hay stock para su pedido" });
                }

                PedidoRepository.Agregar(pedido);

                //resta los kilos pedidos al producto
                ProductoRepository.Actualizar(prod);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home", new { message = "Error al crear el pedido" });
            }
        }


        //muestra los kilos disponibles, se llama desde JS
        public ActionResult ObtenerKilos(int id)
        {
            try
            {
                if (id == 0) throw new Exception();

                int kilos = ProductoRepository.ObtenerPorId(id).Kilos;
                return Ok(kilos);
            }
            catch
            {
                return Ok(0);
            }
        }


        // GET: PedidoController/Edit/5
        public ActionResult Edit(int id)
        {
            var p = PedidoRepository.ObtenerPorId(id);
            return View(p);
        }




        // POST: PedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
               
                Pedido pedido = new Pedido()
                {
                    IdPedido = id, 
                    Kilos = int.Parse(collection["Kilos"]),  
                    IdHelado = int.Parse(collection["IdHelado"]), 
                    IdUsuarioAlta = 0,  
                   
                };

                
                PedidoRepository.Actualizar(pedido);

              
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                return RedirectToAction("Error", "Home", new { message = "Error al editar el pedido" });
            }
        }

        // GET: PedidoController/Delete/5

        // GET: PedidoController/Delete/5
        public ActionResult Delete(int id)
        {
           

            
            var pedido = PedidoRepository.ObtenerPorId(id);
            if (pedido == null)
            {
                return RedirectToAction("Error", "Home", new { message = "El pedido no existe." });
            }

            return View(pedido);  
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                
                PedidoRepository.Eliminar(id);
                return RedirectToAction(nameof(Index));  
            }
            catch
            {
                return RedirectToAction("Error", "Home", new { message = "Error al eliminar el pedido" });
            }
        }






            
        
    }
}