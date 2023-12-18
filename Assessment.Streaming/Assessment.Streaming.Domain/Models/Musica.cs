using Assessment.Streaming.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Streaming.Domain.Models
{
    public class Musica
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Duracao Duracao { get; set; }

        public Album Album { get; set; }
    }
}
