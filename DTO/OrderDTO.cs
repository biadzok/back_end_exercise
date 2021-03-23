using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication.DTO
{
    public class OrderDTO
    {
        public int customer_id { get; set; }
        public List<int> product_id { get; set; }
    }
}
