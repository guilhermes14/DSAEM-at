using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Application.Conta.Infra
{
    public class DadosUsuario
    {
        public Guid Id { get; set; }
        [Required]
        public String Nome { get; set; }
        [Required]
        public String Cpf { get; set; }
        [Required]
        public Guid PlanoId { get; set; }
        public DadosCartao Cartao { get; set; }
        public List<DadosPlaylist> Playlists { get; set; }
    }

    public class DadosCartao
    {
        [Required]
        public String Numero { get; set; }
        [Required]
        public Decimal Limite { get; set; }
        [Required]
        public Boolean Ativo { get; set; }
    }

    public class DadosPlaylist
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Boolean Publica { get; set; }
        public List<DadosMusica> Musicas { get; set; }
    }

    public class DadosMusica
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Duracao { get; set; }
    }
}
