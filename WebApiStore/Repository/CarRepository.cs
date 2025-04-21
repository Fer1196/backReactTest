using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Text.Json;
using WebApiStore.Interfaces;
using WebApiStore.Models;

namespace WebApiStore.Repository
{
    public class CarRepository : ICarRepository
    {

        private List<Category> _carItems;
        private List<ProductCataLog> _productsLog;
        

        private readonly string _jsonDataBase = Path.Combine(Directory.GetCurrentDirectory(), "DataBase", "data.json");
        private readonly string _jsonLogPurchase = Path.Combine(Directory.GetCurrentDirectory(), "DataBase", "purchase.json");


        private void createFilePurchase()
        {
            if (File.Exists(_jsonLogPurchase))
            {
                var dataJson = File.ReadAllText(_jsonLogPurchase);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                if (!string.IsNullOrWhiteSpace(dataJson))
                {
                    try
                    {
                        _productsLog = JsonSerializer.Deserialize<List<ProductCataLog>>(dataJson, options) ?? [];
                    }
                    catch (JsonException)
                    {
                        _productsLog = new List<ProductCataLog>();
                    }
                }
                else
                {
                    _productsLog = new List<ProductCataLog>();
                }
            }
            else
            {
                var directory = Path.GetDirectoryName(_jsonLogPurchase);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(_jsonLogPurchase, string.Empty);
                _productsLog = new List<ProductCataLog>();
            }
        }

        public CarRepository( ) {
            
            if (File.Exists(_jsonDataBase))
            {
                var dataJson = File.ReadAllText(_jsonDataBase);

               
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                _carItems = JsonSerializer.Deserialize<List<Category>>(dataJson, options) ?? [];
            }
            else
            {
                _carItems = new List<Category>();
            }



        }

        private void SaveToJson<T>(string filePath, List<T> data)
        {
            
    
            var options = new JsonSerializerOptions
            {
                WriteIndented = true 
            };


            try
            {
                var json = JsonSerializer.Serialize(data, options);
                File.WriteAllText(filePath, json);
                Console.WriteLine("Archivo JSON actualizado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el archivo JSON: {ex.Message}");
            }
        }

        public List<Category> GetAll() {
            return _carItems;   
        }

        public List<Category> GetCategoryName()
        {
            return _carItems
        .Where(c => !string.IsNullOrEmpty(c.CaprNombreRuta))
        .Select(c => new Category
        {
            CaprId = c.CaprId,
            CaprCodigo = c.CaprCodigo,
            CaprNombre = c.CaprNombre??"",
            CaprNombreRuta = c.CaprNombreRuta??"", 
            CaprPadre = c.CaprPadre ?? "",
            CaprStatus = c.CaprStatus ?? "",             
        }) 
        .ToList();


        }


        public Product[] GetProductByCategory(int category)
        {


            if (category==0) {
                var car = _carItems
                    .Where(category => category.CatalogoProd != null)
                    .SelectMany(category => category.CatalogoProd);
                return car.ToArray() ;
            }
            

            var listCategory = _carItems.Find(pro => pro.CaprId ==category);

            return listCategory?.CatalogoProd ?? new Product[] { };


        }

        public bool DeleteByCategory(int caprId) {
            var category = _carItems.FirstOrDefault(p=>p.CaprId.Equals(caprId));

            if (category != null)
            {
                _carItems.Remove(category);
                SaveToJson(_jsonDataBase, _carItems);
                return true;
            }

            return false;

        }

        public bool DeleteByProduct(int caprCodigo, int cproCodigo)
        {
            var category = _carItems.FirstOrDefault(p => p.CaprCodigo.Equals(caprCodigo));

            if (category != null)
            {
                var product = category.CatalogoProd.FirstOrDefault(p => p.CproCodigo == cproCodigo);
                if (product != null)
                {

                    category.CatalogoProd = category.CatalogoProd
                .Where(p => p.CproCodigo != cproCodigo) 
                .ToArray();
                    SaveToJson(_jsonDataBase, _carItems);



                    Console.WriteLine($"Producto con CproCodigo {cproCodigo} eliminado de la categoría {caprCodigo}.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Producto con CproCodigo {cproCodigo} no encontrado en la categoría {caprCodigo}.");
                    return false;
                }
            }

            return false;

        }


        public bool UpdateProduct(int caprCodigo, Product updatedProduct) {
            var category = _carItems.FirstOrDefault(p => p.CaprId.Equals(caprCodigo));

            if (category != null)
            {
                var product = category.CatalogoProd.FirstOrDefault(p => p.CproCodigo == updatedProduct.CproCodigo);
                if (product != null) {
                    product.CproNombre = updatedProduct.CproNombre;
                    product.CproDescripcion = updatedProduct.CproDescripcion;
                    product.CproMarca = updatedProduct.CproMarca;
                    product.CproUbicacion = updatedProduct.CproUbicacion;
                    product.CproTipoPrecio = updatedProduct.CproTipoPrecio;
                    SaveToJson(_jsonDataBase, _carItems);
                    return true;
                }
                return false;

            }
            else return false;
        }


        public bool InsertProductInCategory(int caprCodigo, Product newProduct) {

            var category = _carItems.FirstOrDefault(c => c.CaprCodigo == caprCodigo);

            if (category != null && newProduct.CaprId == category.CaprId) {
                if (category.CatalogoProd == null)
                {
                    category.CatalogoProd = new[] { newProduct };
                }
                else {
                    var productList = category.CatalogoProd.ToList();

                    bool exists = productList.Any(p =>
                    p.CproCodigo == newProduct.CproCodigo  || (
                     p.CproId == newProduct.CproId)
                    );

                    if (exists) { return false; }
                    productList.Add(newProduct);
                    category.CatalogoProd = productList.ToArray();
                }
                SaveToJson(_jsonDataBase, _carItems);
                return true;

            }
            return false;
        }

        public bool SaveLogPurchase(ProductCataLog product)
        {

            try
            {
                createFilePurchase();
                _productsLog.Add(product);
                SaveToJson(_jsonLogPurchase, _productsLog);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
