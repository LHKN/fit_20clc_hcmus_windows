﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public sealed class Bill
    {
        private int _id;
        private int _customerId;
<<<<<<< HEAD
        private int _totalPrice;
        private DateOnly _transactionDate;

        public int Id { get => _id; set => _id = value; }
        public int CustomerId { get => _customerId; set => _customerId = value; }
        public int TotalPrice { get => _totalPrice; set => _totalPrice = value; }
        public DateOnly TransactionDate { get => _transactionDate; set => _transactionDate = value; }
=======
        private decimal _totalPrice;
        private DateTime _transactionDate;

        public int Id { get => _id; set => _id = value; }
        public int CustomerId { get => _customerId; set => _customerId = value; }
        public decimal TotalPrice { get => _totalPrice; set => _totalPrice = value; }
        public DateTime TransactionDate { get => _transactionDate; set => _transactionDate = value; }


>>>>>>> bcb6f031140599b2b5291bbae7cbd5cebb4164e1
    }
}
