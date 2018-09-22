using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GameOfThrones.Data;

namespace GameOfThrones.Services
{
    public interface IHousesService
    {
        Task<List<House>> FetchHousesAsync(int page);
    }

    public class HousesService : IHousesService
    {
        public async Task<List<House>> FetchHousesAsync(int page)
        {
            using (var httpClient = new HttpClient())
            {
                var jsonResponse = await httpClient.GetStringAsync(new Uri($"https://www.anapioficeandfire.com/api/houses?page={page}&pageSize=50"));

                return House.FromJson(jsonResponse);
            }
        }
    }
}
