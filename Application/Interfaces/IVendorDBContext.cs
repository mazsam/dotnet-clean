using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using VendorBoilerplate.Domain.Entities;
using StoredProcedureEFCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace VendorBoilerplate.Application.Interfaces
{
    public interface IUserDBContext : IDisposable
    {
        DbSet<User> Users { set; get; }

        IStoredProcBuilder loadStoredProcedureBuilder(string val);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}