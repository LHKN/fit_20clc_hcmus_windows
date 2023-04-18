using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public sealed class Book: INotifyPropertyChanged, ICloneable
    {
        private int _id;
        private string _title;
        private string _author;
        private string _description;
        private DateOnly _publishedDate;
        private string _image;
        private int _gerneId;
        private int _price;
        private int _quantity;

        public int Id { get => _id; set => _id = value; }
        public string Title { get => _title; set => _title = value; }
        public string Author { get => _author; set => _author = value; }
        public string Description { get => _description; set => _description = value; }
        public DateOnly PublishedDate { get => _publishedDate; set => _publishedDate = value; }
        public int GerneId { get => _gerneId; set => _gerneId = value; }
        public int Price { get => _price; set => _price = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public string Image { get => _image; set => _image = value; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            return new Book
            {
                Id = Id,
                Title = Title,
                Author = Author,
                Description = Description,
                PublishedDate = PublishedDate,
                GerneId = GerneId,
                Price = Price,
                Quantity = Quantity,
                Image = Image
            };
        }
    }
}
