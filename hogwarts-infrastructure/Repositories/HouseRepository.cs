using hogwarts_core.Entities;
using hogwarts_core.Interfaces;
using hogwarts_infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hogwarts_infrastructure.Repositories
{
    public class HouseRepository : IHouseRepository
    {
        private readonly HogwartsContext hogwartsContext;

        public HouseRepository(HogwartsContext hogwartsContext)
        {
            this.hogwartsContext = hogwartsContext;
        }

        public async Task<House> GetHouse(string house)
        {
            return await hogwartsContext.Houses.FirstOrDefaultAsync(x => x.Name == house);
        }

        public async Task<IEnumerable<House>> GetHouses()
        {
            return await hogwartsContext.Houses.ToListAsync();
        }
    }
}
