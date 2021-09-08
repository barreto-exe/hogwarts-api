using hogwarts_core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hogwarts_core.Interfaces
{
    public interface IApplicationRepository
    {
        public Task<IEnumerable<Application>> GetApplications();
        public Task<Application> GetApplication(int id);
        public Task InsertApplication(Application application);
        public Task<bool> UpdateApplication(Application application);
        public Task<bool> DeleteApplication(int id);
    }
}
