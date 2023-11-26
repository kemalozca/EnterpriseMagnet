using EnterpriseMagnet.DataAccess.Abstract.UnitOfWorks;
using EnterpriseMagnet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseMagnet.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWorks
    {
        private readonly EnterpriseMagnetDbContext _dbContext;

        public UnitOfWork(EnterpriseMagnetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
