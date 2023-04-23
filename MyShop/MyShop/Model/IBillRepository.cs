using System;
using System.Collections.Generic;
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
        Task<List<int>> GetEmptyBillId();
        Task<List<BillDetail>> GetBillDetailById(int billId);
        Task<List<int>> GetBookIdsById(int billId);

        Task AddBillDetail(BillDetail billDetail);
        Task EditBillDetail(BillDetail billDetail);
        Task RemoveBillDetail(int billId, int bookId);
    }
}
