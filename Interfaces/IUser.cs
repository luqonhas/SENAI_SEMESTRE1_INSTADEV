using System.Collections.Generic;
using Project_Instadev.Models;

namespace Project_Instadev.Interfaces
{
    public interface IUser
    {
        // CRUD
        void Create(User newUser);
        List<User> ReadAllItems();
        void Update(User userUpdate);
        void Delete(int id);
    }
}