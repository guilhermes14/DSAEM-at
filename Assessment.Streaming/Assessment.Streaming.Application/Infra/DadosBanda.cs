using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Streaming.Application.Infra
{
    public class DadosBanda
    {
        public Guid Id { get; set; }

        [Required]
        public String Nome { get; set; }

        [Required]
        public String Descricao { get; set; }

        public List<DadosAlbum> Albums { get; set; }


    }

    public class DadosAlbum
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public List<DadosMusica> Musicas { get; set; }
    }

    public class DadosMusica
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Duracao { get; set; }

    }
}

