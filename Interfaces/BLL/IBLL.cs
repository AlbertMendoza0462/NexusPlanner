using Microsoft.EntityFrameworkCore;
using NexusPlanner.DAL;
using NexusPlanner.Models;

namespace NexusPlanner.Interfaces.BLL
{
    public interface IBLL<T> : ILecturaBLL<T>, IEscrituraBLL<T>
    {

    }
}
