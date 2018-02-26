using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool Available { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

    }
    

}
