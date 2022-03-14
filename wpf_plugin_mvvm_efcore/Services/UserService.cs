using Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_plugin_mvvm_efcore.Services
{
    //This class is our default service to work with all user data, without sql server
    public class UserService : IUserService
    {
        public string InheritorPluginName => "DefaultService";

        public ObservableCollection<User> Users = new();    

        public UserService()
        {

        }
        public void Add(User user)
        {
            Users.Add(user);
        }

        public User GetById(int id)
        {
            foreach (User user in Users)
                if (user.Id == id)
                    return user;

            return null;
        }

        public void Remove(User user)
        {
            Users.Remove(user);
        }

        public User[] GetUsers()
        {
            throw new NotImplementedException();
        }

        public void SaveChangesAsync()
        {
           /// throw new NotImplementedException();
        }
    }
}
