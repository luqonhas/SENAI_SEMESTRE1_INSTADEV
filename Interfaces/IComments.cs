using System.Collections.Generic;
using Project_Instadev.Models;

namespace Project_Instadev.Interfaces
{
    public interface IComments
    {
        // CRUD
        void Create(Comments newComment);
        List<Comments> ReadAllItens();
        void Update();
        void Delete(int id);
    }
}