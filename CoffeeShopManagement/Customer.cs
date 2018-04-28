using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeCustomerForm
{
    class Customer
    {  
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        public int Phone { get; set; }
    }
}
