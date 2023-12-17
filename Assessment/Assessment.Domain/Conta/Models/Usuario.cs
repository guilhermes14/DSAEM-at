using Assessment.Domain.Conta.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Domain.Conta.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public String Nome { get; set; }
        public Cpf Cpf { get; set; }
        public List<Cartao> Cartoes { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<Assinatura> Assinaturas { get; set; }

        public Usuario()
        {
            this.Cartoes = new List<Cartao>();
            this.Playlists = new List<Playlist>();
            this.Assinaturas = new List<Assinatura>();
        }

        public void CriaUsuario(string nome, string cpf, Plano plano, Cartao cartao)
        {
            this.Nome = nome;
            this.Cpf = new Cpf(cpf);
            this.AddCartao(cartao);
            this.AssinaPlano(plano, cartao);
            this.CriaPlayList();
        }
        private void AddCartao(Cartao cartao)
        {
            this.Cartoes.Add(cartao);
        }
        public void AssinaPlano(Plano plano, Cartao cartao)
        {
            cartao.CriarTransacao(plano.Nome, plano.ValorPlano, plano.DescricaoPlano);

            if (this.Assinaturas.Count > 0 && this.Assinaturas.Any(x => x.Ativo))
            {
                var planoAtivo = this.Assinaturas.FirstOrDefault(x => x.Ativo);
                planoAtivo.Ativo = false;
            }

            this.Assinaturas.Add(new Assinatura()
            {
                Ativo = true,
                AssinaturaDT = DateTime.Now,
                Plano = plano,
                Id = Guid.NewGuid()
            });
        }

        public void CriaPlayList(string nome = "Favoritas")
        {
            this.Playlists.Add(new Playlist()
            {
                Id = Guid.NewGuid(),
                Nome = nome,
                Publica = false,
                Usuario = this
            });
        }

        public void Favoritar(Musica musica)
        {
            this.Playlists.FirstOrDefault(x => x.Nome == "Favoritas")
                          .Musicas.Add(musica);
        }
    }
}
