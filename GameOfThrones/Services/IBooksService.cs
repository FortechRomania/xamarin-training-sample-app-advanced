using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GameOfThrones.Data;

namespace GameOfThrones.Services
{
    public interface IBooksService
    {
        Task<List<Book>> FetchBooksAsync();
    }

    public class BooksService : IBooksService
    {
        public async Task<List<Book>> FetchBooksAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var jsonResponse = await httpClient.GetStringAsync(new Uri("https://www.anapioficeandfire.com/api/books?pageSize=50"));

                return Book.FromJson(jsonResponse);
            }
        }
    }
}
