using Assessment.Streaming.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Streaming.Repository
{
    public class PlanoRepository
    {
        private static List<Plano> plano;
        public PlanoRepository()
        {
            if (PlanoRepository.plano == null)
            {
                PlanoRepository.plano = new List<Plano>();
                PlanoRepository.plano.Add(new Plano()
                {
                    Descricao = "Plano para teste",
                    Nome = "Plano Teste",
                    Valor = 10M,
                    Id = new Guid("5DE20D34-726C-4041-A6D6-74F6C57AFD17")
                });
            }
        }

        public Plano ObterPlanoPorId(Guid idPlano)
        {
            return PlanoRepository.plano.FirstOrDefault(x => x.Id == idPlano);
        }
    }
}
