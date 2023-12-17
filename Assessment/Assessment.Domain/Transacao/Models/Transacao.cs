using Assessment.Domain.Transacao.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Domain.Transacao.Models
{
    public class Transacao
    {
        public Guid Id { get; set; }
        public DateTime TransacaoDT { get; set; }
        public Decimal Valor { get; set; }
        public string Descricao { get; set; }
        public Merchant Merchant { get; set; }
    }
}
