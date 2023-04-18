using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public sealed class BillDetail
    {
        private int _billId;
        private int _bookId;
        private int _price;
        private int _number;

        public int BillId { get => _billId; set => _billId = value; }
        public int BookId { get => _bookId; set => _bookId = value; }
        public int Price { get => _price; set => _price = value; }
        public int Number { get => _number; set => _number = value; }

        public int TotalPrice()
        {
            return Price*Number;
        }
    }
}
