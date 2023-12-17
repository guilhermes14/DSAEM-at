using Assessment.Domain.Conta.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Domain.Conta.Objects
{
    public class Cpf
    {
        private CpfException _validationError = new CpfException();

        public Cpf() { }

        public String NumeroCpf { get; set; }

        public Cpf(string numeroCpf)
        {
            this.NumeroCpf = numeroCpf;

            if (this.CpfValido() == false)
            {
                this._validationError.AddError(new Core.Exception.ModelsValidation
                {
                    ErrorMessage = "CPF Inválido",
                    ErrorName = nameof(CpfException)
                });

                this._validationError.ValidateAndThrow();
            }
        }

        public String NumeroFormatado()
        {
            return Convert.ToInt64(this.NumeroCpf).ToString("###.###.###-##");
        }

        public bool CpfValido()
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;
            string digito;
            int soma;
            int resto;

            var cpf = this.NumeroCpf.Trim().Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
