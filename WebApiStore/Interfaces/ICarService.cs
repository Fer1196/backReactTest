using WebApiStore.Models;

namespace WebApiStore.Interfaces
{
    public interface ICarService
    {
        List<Category> GetAllCatgoryItems();
        List<Category> GetCategories();

        Product[] GetProductCategory(int category);
        bool DeleteCategory(int caprId);

        bool DeleteProduct(int caprCodigo, int cproCodigo);

        bool UpdateProductByCategory(int caprCodigo, Product updatedProduct);

        bool InsertProductCategory(int caprCodigo, Product newProduct);
        bool SavePurchase(ProductCataLog product);


    }
}
