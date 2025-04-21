using WebApiStore.Interfaces;
using WebApiStore.Models;
using WebApiStore.Repository;

namespace WebApiStore.Service
{
    public class CarService: ICarService
    {
        private readonly CarRepository _carService;

        public CarService (CarRepository carService)
        {
            _carService = carService;
        }

        public List<Category> GetAllCatgoryItems()
        {
            return _carService.GetAll();
        }

        public List<Category> GetCategories()
        {
            return _carService.GetCategoryName();
        }

        public Product[] GetProductCategory(int category)
        {
            return _carService.GetProductByCategory(category);
        }

        public bool DeleteCategory(int caprId)
        {
           return _carService.DeleteByCategory(caprId);
        }

        public bool DeleteProduct(int caprCodigo, int cproCodigo)
        {
            return _carService.DeleteByProduct(caprCodigo, cproCodigo);
        }

        public bool UpdateProductByCategory(int caprCodigo, Product updatedProduct) {
            return _carService.UpdateProduct(caprCodigo, updatedProduct);
        }

        public bool InsertProductCategory(int caprCodigo, Product newProduct)
        {
            return _carService.InsertProductInCategory(
                caprCodigo,newProduct);
        }

        public bool SavePurchase(ProductCataLog product)
        {
            return _carService.SaveLogPurchase(product);
        }
    }
}
