﻿using hogwarts_core.Entities;
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

        public async Task<House> GetHouse(string name)
        {
            return await hogwartsContext.Houses.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<House>> GetHouses()
        {
            return await hogwartsContext.Houses.ToListAsync();
        }

        public async Task<bool> InsertHouse(House house)
        {
            hogwartsContext.Houses.Add(house);
            var result = await hogwartsContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteHouse(string name)
        {
            var currentHouse = await GetHouse(name);
            if (currentHouse == null) return false;

            hogwartsContext.Houses.Remove(currentHouse);

            int rows = await hogwartsContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
