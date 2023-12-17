using Assessment.Domain.Conta.Exception;
using Assessment.Domain.Conta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Tests.Domain
{
    public class UsuarioTest
    {
        [Fact]
        public void CriaUsuarioComSucesso()
        {
            Plano plano = new Plano()
            {
                DescricaoPlano = "Plano para teste",
                Id = Guid.NewGuid(),
                Nome = "Plano teste",
                ValorPlano = 10.50M
            };

            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 5000M,
                Numero = "5237864195",
            };

            string cpf = "29732374080";
            string nome = "usuario teste";

            Usuario usuario = new Usuario();
            usuario.CriaUsuario(nome, cpf, plano, cartao);

            Assert.NotNull(usuario.Cpf);
            Assert.NotNull(usuario.Nome);
            Assert.True(usuario.Cpf.NumeroCpf == cpf);
            Assert.True(usuario.Nome == nome);

            Assert.True(usuario.Assinaturas.Count > 0);
            Assert.Same(usuario.Assinaturas[0].Plano, plano);

            Assert.True(usuario.Cartoes.Count > 0);
            Assert.Same(usuario.Cartoes[0], cartao);

            Assert.True(usuario.Playlists.Count > 0);
            Assert.True(usuario.Playlists[0].Nome == "Favoritas");
            Assert.False(usuario.Playlists[0].Publica);
        }

        [Fact()]
        public void TesteDeCpfInvalido()
        {
            Plano plano = new Plano()
            {
                DescricaoPlano = "Plano para teste",
                Id = Guid.NewGuid(),
                Nome = "Plano teste",
                ValorPlano = 10.50M
            };

            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 5000M,
                Numero = "5237864195",
            };

            string cpf = "29732374";
            string nome = "usuario teste";
            Usuario usuario = new Usuario();

            Assert.Throws<CpfException>
                (() => usuario.CriaUsuario(nome, cpf, plano, cartao));

        }


        [Fact()]
        public void TesteDeCartaoInvalido()
        {
            Plano plano = new Plano()
            {
                DescricaoPlano = "Plano para teste",
                Id = Guid.NewGuid(),
                Nome = "Plano teste",
                ValorPlano = 10.50M
            };

            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = false,
                Limite = 5000M,
                Numero = "5237864195",
            };

            string cpf = "29732374080";
            string nome = "usuario teste";
            Usuario usuario = new Usuario();

            Assert.Throws<CartaoException>
                (() => usuario.CriaUsuario(nome, cpf, plano, cartao));

        }
    }
}
