using BusBookingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using BusBookingProject.Common;
using System.Data.Entity.Validation;

namespace BusBookingProject.DAO
{
    public class UserDAO
    {
        BusTicketManagementEntities db = null;
        public UserDAO()
        {
            db = new BusTicketManagementEntities();
        }
        public long Insert(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user.Id;
        }

        public IEnumerable<User> ListAllPaging(int page, int pageSize)
        {
            return db.Users.OrderBy(x => x.Id).ToPagedList(page, pageSize);
        }
        public bool CheckUsername(string username)
        {
            return db.Users.Count(x => x.Username == username) > 0;
        }

        public User GetUserById(int id)
        {
            return db.Users.Where(x => x.Id == id).FirstOrDefault();
        }
        public User GetUserByName(string username)
        {
            return db.Users.Where(x => x.Username == username).FirstOrDefault();
        }
        public bool Login(string username, string password)
        {
            string md5Password = MD5Hash.GetMd5Hash(password);
            var result = db.Users.Count(x => x.Username == username && x.Password == md5Password);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.Id);
                user.Username = entity.Username;
                if (!String.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.CreatedDate = DateTime.Now;
                user.Role = entity.Role;
                user.ActiveCode = Guid.NewGuid();
                user.Status = entity.Status;

                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}