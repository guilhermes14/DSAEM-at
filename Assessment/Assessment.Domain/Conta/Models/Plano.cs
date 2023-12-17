using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Domain.Conta.Models
{
    public class Plano
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string DescricaoPlano { get; set; }
        public Decimal ValorPlano { get; set; }
    }
}
