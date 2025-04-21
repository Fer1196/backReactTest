using WebApiStore.Models;

namespace WebApiStore.Interfaces
{
    public interface ICarRepository
    {
        List<Category> GetAll();
        List<Category> GetCategoryName();
        Product[] GetProductByCategory(int category ); 
        bool DeleteByCategory(int caprId);
        bool DeleteByProduct(int caprCodigo, int cproCodigo);
        bool UpdateProduct(int caprCodigo, Product updatedProduct);

        bool InsertProductInCategory(int caprCodigo, Product newProduct);

        bool SaveLogPurchase(ProductCataLog product);
    }
}
