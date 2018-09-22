using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GameOfThrones.Data;

namespace GameOfThrones.Services
{
    public interface ICharactersService
    {
        Task<List<Character>> FetchCharactersAsync(int page);
    }

    public class CharactersService : ICharactersService
    {
        public async Task<List<Character>> FetchCharactersAsync(int page)
        {
            using (var httpClient = new HttpClient())
            {
                var jsonResponse = await httpClient.GetStringAsync(new Uri($"https://www.anapioficeandfire.com/api/characters?page={page}&pageSize=50"));

                return Character.FromJson(jsonResponse);
            }
        }
    }
}
