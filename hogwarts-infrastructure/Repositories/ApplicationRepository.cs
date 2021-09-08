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

        public async Task<bool> InsertApplication(Application application)
        {
            hogwartsContext.Applications.Add(application);
            var result = await hogwartsContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateApplication(Application application)
        {
            var currentApplication = await GetApplication(application.ApplicationId);
            if (currentApplication == null) return false;

            currentApplication.PersonId = application.PersonId;
            currentApplication.AspiredHouse = application.AspiredHouse;

            int rows = await hogwartsContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteApplication(int id)
        {
            var currentApplication = await GetApplication(id);
            if (currentApplication == null) return false;

            hogwartsContext.Applications.Remove(currentApplication);

            int rows = await hogwartsContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
