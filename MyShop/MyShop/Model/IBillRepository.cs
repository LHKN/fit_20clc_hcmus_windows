using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public interface IBillRepository
    {
        void Add(Bill bill);
        void Edit(Bill bill);
        void Remove(int id);
        Task<Bill> GetById(int id);

        // get by Date
        Task<List<Bill>> GetAll(String date);

        void AddBillDetail(int billId, BillDetail billDetail);
        void EditBillDetail(int billId, BillDetail billDetail);
        void RemoveBillDetail(int billId, int billDetailId);
    }
}
