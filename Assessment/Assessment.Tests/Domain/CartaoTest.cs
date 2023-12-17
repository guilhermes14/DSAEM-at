using Assessment.Domain.Conta.Exception;
using Assessment.Domain.Conta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Tests.Domain
{
    public class CartaoTest
    {
        [Fact]
        public void CriaTransacaoComSucesso()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 5000M,
                Numero = "5237864195",
            };

            cartao.CriarTransacao("teste", 10M, "teste descricao");
            Assert.True(cartao.Transacoes.Count > 0);
        }

        [Fact]
        public void TesteCartaoInativo()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = false,
                Limite = 1000M,
                Numero = "6465465466",
            };

            Assert.Throws<CartaoException>(
                () => cartao.CriarTransacao("teste", 19M, "teste transacao"));
        }

        [Fact]
        public void CartaoComLimiteBaixo()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 10M,
                Numero = "6465465466",
            };

            Assert.Throws<CartaoException>(
                () => cartao.CriarTransacao("teste", 19M, "teste transacao"));
        }

        [Fact]
        public void CartaoComValorDuplicado()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 1000M,
                Numero = "6465465466",
            };

            cartao.Transacoes.Add(new Assessment.Domain.Transacao.Models.Transacao()
            {
                TransacaoDT = DateTime.Now,
                Id = Guid.NewGuid(),
                Merchant = new Assessment.Domain.Transacao.Objects.Merchant()
                {
                    Nome = "teste"
                },
                Valor = 19M,
                Descricao = "testest"
            });

            Assert.Throws<CartaoException>(
                () => cartao.CriarTransacao("teste", 19M, "teste transacao"));
        }

        [Fact]
        public void TesteComCartaoSendoReutilizado()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 1000M,
                Numero = "6465465466",
            };

            cartao.Transacoes.Add(new Assessment.Domain.Transacao.Models.Transacao()
            {
                TransacaoDT = DateTime.Now.AddMinutes(-1),
                Id = Guid.NewGuid(),
                Merchant = new Assessment.Domain.Transacao.Objects.Merchant()
                {
                    Nome = "teste"
                },
                Valor = 19M,
                Descricao = "testest"
            });

            cartao.Transacoes.Add(new Assessment.Domain.Transacao.Models.Transacao()
            {
                TransacaoDT = DateTime.Now.AddMinutes(-0.5),
                Id = Guid.NewGuid(),
                Merchant = new Assessment.Domain.Transacao.Objects.Merchant()
                {
                    Nome = "teste"
                },
                Valor = 19M,
                Descricao = "testest"
            });

            cartao.Transacoes.Add(new Assessment.Domain.Transacao.Models.Transacao()
            {
                TransacaoDT = DateTime.Now,
                Id = Guid.NewGuid(),
                Merchant = new Assessment.Domain.Transacao.Objects.Merchant()
                {
                    Nome = "teste"
                },
                Valor = 19M,
                Descricao = "testest"
            });


            Assert.Throws<CartaoException>(
                () => cartao.CriarTransacao("teste", 19M, "teste transacao"));
        }
    }
}
