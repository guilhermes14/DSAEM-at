using Assessment.Domain.Conta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assessment.Repository
{
    public class PlanoRepository
    {
        private HttpClient HttpClient { get; set; }

        public PlanoRepository()
        {
            this.HttpClient = new HttpClient();
        }

        public async Task<Plano> GetPlano(Guid id)
        {
            var result = await this.HttpClient.GetAsync($"https://localhost:7221/api/Plano/{id}");
            if (result.IsSuccessStatusCode == false)
                return null;

            var content = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Plano>(content);
        }
    }
}
