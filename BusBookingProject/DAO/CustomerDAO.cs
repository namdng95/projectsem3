using BusBookingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusBookingProject.DAO
{
    public class CustomerDAO
    {
        BusTicketManagementEntities db = null;
        public CustomerDAO()
        {
            db = new BusTicketManagementEntities();
        }
        public long Insert(Khach_Hang customer)
        {
            db.Khach_Hang.Add(customer);
            db.SaveChanges();
            return customer.MaKH;
        }
        public bool CheckEmail(string email)
        {
            return db.Khach_Hang.Count(x => x.Email == email) > 0;
        }
        public bool IsEmailExist(string email)
        {
            var customer = db.Khach_Hang.Where(x => x.Email == email).FirstOrDefault();
            return customer == null ? false : true;
        }
        public bool CheckPhoneNumber(string phoneNumber)
        {
            return db.Khach_Hang.Count(x => x.DienThoai == phoneNumber) > 0;

        }
    }
}