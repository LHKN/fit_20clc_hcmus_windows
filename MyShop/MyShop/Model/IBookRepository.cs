using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public interface IBookRepository
    {
        void Add(Book book);
        void Edit(Book book);
        void Remove(int id);
        Task<Book> GetById(int id);
        
        Task<ObservableCollection<Book>> GetAll();
    }
}
