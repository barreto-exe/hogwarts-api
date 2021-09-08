using hogwarts_core.Entities;
using hogwarts_core.Interfaces;
using hogwarts_infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hogwarts_infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly HogwartsContext hogwartsContext;

        public ApplicationRepository(HogwartsContext hogwartsContext)
        {
            this.hogwartsContext = hogwartsContext;
        }

        public async Task<Application> GetApplication(int id)
        {
            return await hogwartsContext.Applications.FirstOrDefaultAsync(x => x.ApplicationId == id);
        }

        public async Task<IEnumerable<Application>> GetApplications()
        {
            return await hogwartsContext.Applications.ToListAsync();
        }

        public async Task InsertApplication(Application application)
        {
            hogwartsContext.Applications.Add(application);
            await hogwartsContext.SaveChangesAsync();
        }
    }
}
