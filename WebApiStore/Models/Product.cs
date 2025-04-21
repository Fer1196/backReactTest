using System.Numerics;
using System.Text.Json.Serialization;

namespace WebApiStore.Models
{
    public class Category
    {
            public int CaprId { get; set; }

      
            public int CaprCodigo { get; set; }

            public string CaprNombre { get; set; }

      
            public string CaprNombreRuta { get; set; }

           
            public string CaprPadre { get; set; }

        
            public string CaprStatus { get; set; }

          
            public Product[] CatalogoProd { get; set; }
        
    }

    public partial class Product
    {
        
        public long CproId { get; set; }

      
        public long CproCodigo { get; set; }

       
        public long CaprId { get; set; }

      
        public long UnidCodigo { get; set; }

        public string CproCodigoint { get; set; }

    
        public string CproCodigobarras { get; set; }

    
        public string CproNombre { get; set; }

       
        public string CproDescripcion { get; set; }

        public string CproMarca { get; set; }

        public string CproUbicacion { get; set; }


        public long? CproTipoPrecio { get; set; }
    }



    public class  ProductCataLog
    {
       public Product[] List { get; set; }
        public int Total { get; set; }
        public string Date { get; set; }

}



}
