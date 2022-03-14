using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public interface IUserService
    {
        public string InheritorPluginName { get; }
        public User[] GetUsers();
        public void SaveChangesAsync();
        public void Add(User user);
        public User GetById(int id);
        public void Remove(User user);
    }
}
