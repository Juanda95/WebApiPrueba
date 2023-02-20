using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Persitence.Contexts;

namespace Persitence.Repository
{
    public class OperacionBancariaRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public OperacionBancariaRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
