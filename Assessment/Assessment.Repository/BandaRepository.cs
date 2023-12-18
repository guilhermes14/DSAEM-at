using Assessment.Domain.Conta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assessment.Repository
{
    public class BandaRepository
    {
        private HttpClient HttpClient { get; set; }

        public BandaRepository()
        {
            this.HttpClient = new HttpClient();
        }

        public async Task<Musica> GetMusica(Guid id)
        {
            var result = await this.HttpClient.GetAsync($"https://localhost:7192/api/banda/musica/{id}");
            if (result.IsSuccessStatusCode == false)
                return null;

            var content = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Musica>(content);
        }
    }
}
