using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ComponentBuisinessLogic;

namespace ComponentAccessToDB
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly transfersystemContext db;
        public UserRepository(transfersystemContext curDb)
        {
            db = curDb;
        }
        public void Add(User element)
        {
            UserDB t = UserConv.BltoDB(element);

            try
            {
                db.Users.Add(t);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new UserAddException("UserAdd", ex);
            }
        }
        public List<User> GetAll()
        {
            IQueryable<UserDB> Users = db.Users;
            List<UserDB> conv = Users.ToList();
            List<User> final = new List<User>();
            foreach (var m in conv)
            {
                final.Add(UserConv.DBtoBL(m));
            }
            return final;
        }
        public void Update(User element)
        {
            UserDB o = db.Users.Find(element.Login);
            o.Password_ = element.Password_;
            o.Name_ = element.Name_;
            o.Surname = element.Surname;
            try
            {
                db.Users.Update(o);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new UserUpdateException("UserUpdate", ex);
            }
        }
        public void Delete(User element)
        {
            UserDB o = db.Users.Find(element.Login);
            if (o == null)
                return;

            try
            {
                db.Users.Remove(o);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new UserDeleteException("UserDelete", ex);
            }
        }
        public User GetUserByLogin(string login)
        {
            UserDB e = db.Users.Find(login);
            return e != null ? UserConv.DBtoBL(e) : null;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
