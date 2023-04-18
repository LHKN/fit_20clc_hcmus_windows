using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public sealed class BillDetail
    {
        private int _bill_id;
        private int _book_id;
        private decimal _price;
        private int _number;

        public int Bill_id { get => _bill_id; set => _bill_id = value; }
        public int Book_id { get => _book_id; set => _book_id = value; }
        public decimal Price { get => _price; set => _price = value; }
        public int Number { get => _number; set => _number = value; }

        public int TotalPrice()
        {
            return (int)(Math.Round(_price, 0) * _number);
        }
    }
}
