using Microsoft.EntityFrameworkCore;
using NexusPlanner.DAL;
using NexusPlanner.Models;

namespace NexusPlanner.Interfaces.BLL
{
    public interface ILecturaBLL<T>
    {
        public Task<T?> Buscar(int id);
        public Task<List<T>> Listar();
    }
}
