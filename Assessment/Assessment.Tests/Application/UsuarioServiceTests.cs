using Assessment.Application.Conta;
using Assessment.Application.Conta.Infra;
using Assessment.Core.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Tests.Application
{
    public class UsuarioServiceTests
    {
        [Fact]
        public void CriaContaComSucesso()
        {
            DadosUsuario dto = new DadosUsuario()
            {
                Nome = "Lorem Ipsum do teste",
                Cpf = "26952278095",
                Cartao = new DadosCartao()
                {
                    Ativo = true,
                    Limite = 100,
                    Numero = "5248581002684983"
                },
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C")
            };

            UsuarioService service = new UsuarioService();
            service.CriarConta(dto);

            Assert.True(dto.Id != Guid.Empty);
        }

        [Fact]
        public void TesteDePlanoInvalido()
        {
            DadosUsuario dto = new DadosUsuario()
            {
                Nome = "Lorem Ipsum do teste",
                Cpf = "26952278095",
                Cartao = new DadosCartao()
                {
                    Ativo = true,
                    Limite = 100,
                    Numero = "5248581002684983"
                },
                PlanoId = Guid.NewGuid()
            };

            UsuarioService service = new UsuarioService();

            //Assert.Throws<ModelsException>(() => service.CriarConta(dto));
        }
    }
}
