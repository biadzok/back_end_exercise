using System.ComponentModel.DataAnnotations;

namespace TestApplication.Models
{
    public class Customers
    {
        [Key]
        public int customer_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string adress { get; set; }
        public int money { get; set; }
    }
}
