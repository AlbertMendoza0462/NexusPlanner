using Microsoft.EntityFrameworkCore;
using NexusPlanner.DAL;
using NexusPlanner.Models;

namespace NexusPlanner.Interfaces.BLL
{
    public interface IEscrituraBLL<T>
    {
        public Contexto _contexto { get; }
        public Task<bool> Existe(int id);
        public Task<bool> Insertar(T instancia);
        public Task<bool> Modificar(T instancia);
        public Task<bool> Guardar(T instancia);
        public Task<bool> Eliminar(int id);
    }
}
