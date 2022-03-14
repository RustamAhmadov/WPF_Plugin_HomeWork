using Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCorePlugin
{
    public class EFCoreUserService : IUserService
    {
        private readonly DataContext context;
        public string InheritorPluginName => "EFCorePluginName";

        public EFCoreUserService()
        {
            context = new DataContext();
        }
        public void Add(User user)
        {
            context.Users.Add(user);            
        }
        public void SaveChangesAsync()
        {
            context.SaveChangesAsync();            
        }
        public User GetById(int userId)
        {
            return (from u in context.Users
                    where u.Id == userId
                    select u).FirstOrDefault();
        }
        public User[] GetUsers()
        {
            return context.Users.ToArray<User>();
        }

        public void Remove(User user)
        {
            var userForRemoving = (from u in context.Users
                                 where u.Id == user.Id
                                 select u).FirstOrDefault();
            context.Remove(userForRemoving);
        }
    }
}
