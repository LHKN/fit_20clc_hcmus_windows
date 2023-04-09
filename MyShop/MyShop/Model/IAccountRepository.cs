using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public interface IAccountRepository
    {
        bool AuthenticateAccount(NetworkCredential credentical);
        void Add(Account account);
        void Edit(Account account);
        void Remove(int id);
        Account GetById(int id);
        Account GetByUsername(int id);
        IEnumerable<Account> GetAll();

    }
}
