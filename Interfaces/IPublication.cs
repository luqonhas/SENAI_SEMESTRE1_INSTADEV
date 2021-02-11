using System.Collections.Generic;
using Project_Instadev.Models;

namespace Project_Instadev.Interfaces
{
    public interface IPublication
    {
        // CRUD
        void Create(Publication newPub);
        List<Publication> ReadAllItens();
        void Update();
        void Delete(int id);
    }
}