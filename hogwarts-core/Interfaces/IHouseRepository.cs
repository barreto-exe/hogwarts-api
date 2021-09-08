﻿using hogwarts_core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hogwarts_core.Interfaces
{
    public interface IHouseRepository
    {
        public Task<IEnumerable<House>> GetHouses();
        public Task<House> GetHouse(string house);
        public Task<bool> InsertHouse(House house);
        public Task<bool> DeleteHouse(string name);
    }
}
