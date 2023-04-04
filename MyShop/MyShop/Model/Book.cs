using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public sealed class Book
    {
        private int _id;
        private string _title;
        private string _author;
        private string _description;
        private DateTime _publishedDate;
        private int _gerneId;
        private int _price;
        private int _quantity;

        public int Id { get => _id; set => _id = value; }
        public string Title { get => _title; set => _title = value; }
        public string Author { get => _author; set => _author = value; }
        public string Description { get => _description; set => _description = value; }
        public DateTime PublishedDate { get => _publishedDate; set => _publishedDate = value; }
        public int GerneId { get => _gerneId; set => _gerneId = value; }
        public int Price { get => _price; set => _price = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
    }
}
