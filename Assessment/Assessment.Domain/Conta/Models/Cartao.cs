using Assessment.Domain.Conta.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Domain.Conta.Models
{
    public class Cartao
    {
        public Guid Id { get; set; }
        public Boolean Ativo { get; set; }
        public Decimal Limite { get; set; }
        public String Numero { get; set; }
        public List<Transacao.Models.Transacao> Transacoes { get; set; }

        private const int TRANSACTION_TIME_INTERVAL = -2;
        private const int TRANSACTION_MERCHANT_REPEAT = 1;

        public Cartao()
        {
            this.Transacoes = new List<Transacao.Models.Transacao>();
        }

        private void ValidaCartao(CartaoException validationErrors)
        {
            if (this.Ativo == false)
            {
                validationErrors.AddError(new Core.Exception.ModelsValidation()
                {
                    ErrorMessage = "Este cartão não é ativo",
                    ErrorName = nameof(CartaoException)
                });
            }
        }

        private void VerificaLimite(Transacao.Models.Transacao transacao, CartaoException validationErrors)
        {
            if (transacao.Valor > this.Limite)
            {
                validationErrors.AddError(new Core.Exception.ModelsValidation()
                {
                    ErrorMessage = "Cartão não possui limite para esta transação",
                    ErrorName = nameof(CartaoException)
                });
            }
        }

        private void ValidaTransacao(Transacao.Models.Transacao transacao, CartaoException validationErrors)
        {
            var ultimasTransacoes = this.Transacoes.Where(x => x.TransacaoDT >= DateTime.Now.AddMinutes(TRANSACTION_TIME_INTERVAL));

            if (ultimasTransacoes?.Count() >= 3)
            {
                validationErrors.AddError(new Core.Exception.ModelsValidation()
                {
                    ErrorMessage = "Cartão utilizado muitas vezes em um período curto demais",
                    ErrorName = nameof(CartaoException)
                });
            }

            if (ultimasTransacoes?.Where(x => x.Merchant.Nome.ToUpper() == transacao.Merchant.Nome.ToUpper()
                                         && x.Valor == transacao.Valor).Count() == TRANSACTION_MERCHANT_REPEAT)
            {
                validationErrors.AddError(new Core.Exception.ModelsValidation()
                {
                    ErrorMessage = "Transação duplicada",
                    ErrorName = nameof(CartaoException)
                });
            }
        }

        public void CriarTransacao(string merchant, Decimal valor, string descricao)
        {
            CartaoException validationErrors = new CartaoException();
            this.ValidaCartao(validationErrors);
            Transacao.Models.Transacao transacao = new Transacao.Models.Transacao();

            transacao.Merchant = new Transacao.Objects.Merchant() { Nome = merchant };
            transacao.Valor = valor;
            transacao.Descricao = descricao;
            transacao.TransacaoDT = DateTime.Now;

            this.VerificaLimite(transacao, validationErrors);
            this.ValidaTransacao(transacao, validationErrors);

            validationErrors.ValidateAndThrow();
            transacao.Id = Guid.NewGuid();

            this.Limite = this.Limite - transacao.Valor;
            this.Transacoes.Add(transacao);
        }
    }
}
