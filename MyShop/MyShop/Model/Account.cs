using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public enum Role
    {
        Customer, Admin
    }
    public class Account
    {
        private int _id;
        private string _name;
        private string _phoneNumber;
        private string _email;
        private string _password;
        private Role _role;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
        public Role Role { get => _role; set => _role = value; }
    }
}
