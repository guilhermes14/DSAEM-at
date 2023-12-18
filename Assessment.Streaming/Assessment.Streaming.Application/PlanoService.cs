using Assessment.Streaming.Application.Infra;
using Assessment.Streaming.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Streaming.Application
{
    public class PlanoService
    {
        private PlanoRepository PlanoRepository { get; set; }
        public PlanoService()
        {
            this.PlanoRepository = new PlanoRepository();
        }

        public DadosPlano ObterPlano(Guid id)
        {
            var plano = this.PlanoRepository.ObterPlanoPorId(id);

            if (plano == null)
                return null;

            return new DadosPlano()
            {
                Descricao = plano.Descricao,
                Id = plano.Id,
                Nome = plano.Nome,
                Valor = plano.Valor,
            };

        }
    }
}
