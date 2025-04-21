using Microsoft.AspNetCore.Mvc;
using WebApiStore.Interfaces;
using WebApiStore.Models;
using WebApiStore.Service;

namespace WebApiStore.Controllers
{
    [ApiController]
    [Route("api/v1/car")]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }


        [HttpGet]
        public IActionResult GetItems()
        {
            var items = _carService.GetAllCatgoryItems();
            return Ok(items);
        }
        [HttpGet("categories")]
        public IActionResult GetCategoryProducts()
        {
            var items = _carService.GetCategories();
            return Ok(items);
        }

        [HttpGet("category/{category}")]
        public IActionResult GetCategoryProducts(int category)
        {
            var items = _carService.GetProductCategory(category);

            return Ok(items);
        }

        [HttpDelete("{caprId}")]
        public IActionResult DeleteCategory(int caprId)
        {
            try
            {
                bool deleted = _carService.DeleteCategory(caprId);
                if (deleted)
                {
                    return Ok($"Categoría con ID {caprId} eliminada.");
                }
                return NotFound($"Categoría con ID {caprId} no encontrada.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("deleteProduct/{caprCodigo}/{cproCodigo}")]
        public IActionResult DeleteProduct(int caprCodigo, int cproCodigo)
        {
            try
            {
                bool deleted = _carService.DeleteProduct(caprCodigo, cproCodigo);
                if (deleted)
                {
                    return Ok($"Producto con ID {cproCodigo} eliminada.");
                }
                return NotFound($"Prodcuto con ID {cproCodigo} no encontrada.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("updateProduct/{caprCodigo}")]
        public IActionResult UpdateProduct(int caprCodigo, [FromBody] Product updatedProduct)
        {
            try
            {
                bool updated = _carService.UpdateProductByCategory(caprCodigo, updatedProduct);

                if (updated)
                {
                    return Ok($"Producto con CproCodigo {updatedProduct.CproCodigo} actualizado correctamente.");
                }
                return NotFound($"Producto con CproCodigo {updatedProduct.CproCodigo} no encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("addProduct/{caprCodigo}")]
        public IActionResult AddProduct(int caprCodigo, [FromBody] Product newProduct)
        {
            try
            {
                bool productAdded = _carService.InsertProductCategory(caprCodigo, newProduct);

                if (productAdded)
                {
                    return Ok($"Producto con CproCodigo {newProduct.CproCodigo} agregado correctamente a la categoría {caprCodigo}.");
                }
                return Conflict($"Ya existe un producto con CproCodigo {newProduct.CproCodigo} en la categoría {caprCodigo}.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("purchase/")]
        public IActionResult AddLogPurchase( [FromBody] ProductCataLog product)
        {
            try
            {
                bool productAdded = _carService.SavePurchase(product);

                if (productAdded)
                {
                    return Ok($"Log creado correctamente");
                }
                return Conflict($"No se puede guardar Log");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




    }
}
