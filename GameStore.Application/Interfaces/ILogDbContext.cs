using GameStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Interfaces;

public interface ILogDbContext
{
    DbSet<Log> Logs { get; set; }
}