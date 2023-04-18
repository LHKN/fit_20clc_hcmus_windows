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
        Task<ObservableCollection<Book>> GetAll();

        Task<List<Genre>> GetGenres();
    }
}
