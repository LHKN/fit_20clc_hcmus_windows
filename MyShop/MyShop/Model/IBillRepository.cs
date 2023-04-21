using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public interface IBillRepository
    {
        Task Add(Bill bill);
        Task Edit(Bill bill);
        Task Remove(int id);
        Task<Bill> GetById(int id);

        Task<List<Bill>> GetAll(DateOnly? dateFrom, DateOnly? dateTo);

        Task AddBillDetail(BillDetail billDetail);
        Task EditBillDetail(int billId, int bookId, BillDetail billDetail);
        Task RemoveBillDetail(int billId, int bookId);
    }
}
