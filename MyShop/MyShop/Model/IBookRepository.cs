using System;
using System.Collections.Generic;
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
        
        Task<List<Book>> GetAll();
    }
}
