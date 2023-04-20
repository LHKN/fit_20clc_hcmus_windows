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
        Task<bool> Add(Book book);
        Task<bool> Edit(Book book);
        Task<bool> Remove(int id);
        Task<Book> GetById(int id);
        Task<List<Book>> GetAll();

        List<Book> Filter(List<Book> booksList, int startPrice = 0, int endPrice = Int32.MaxValue, string keyword = "", int genre = 0);

        Task<List<Genre>> GetGenres();
    }
}
