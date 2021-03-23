using System.ComponentModel.DataAnnotations;

namespace TestApplication.Models
{
    public class Products
    {
        [Key]
        public int product_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }

    }
}
