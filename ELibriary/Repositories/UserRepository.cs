using System;
using ELibriary.Tables;

namespace ELibriary.Repositories
{
    class UserRepository
    {
        public User SelectUserById(AppContext db, int id)
        {
            var user = db.Users.Where(user => user.Id == id).FirstOrDefault();
            if (!(user is null))
            {
                return user;
            }
            else
            {
                Console.WriteLine("Пользователь не найден");
                return null;
            }
        }

        public List<User> SelectAllUsers(AppContext db)
        {
            var users = db.Users.ToList();
            return users;
        }

        public void AddUser(AppContext db, User user)
        {

            try
            {
                db.Users.Add(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddUsers(AppContext db, User[] users)
        {
            try
            {
                db.Users.AddRange(users);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteUser(AppContext db, User user)
        {
            try
            {
                db.Users.Remove(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteUsers(AppContext db, User[] users)
        {
            try
            {
                db.Users.RemoveRange(users);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}