using Assessment.Application.Conta.Infra;
using Assessment.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Tests.Controller
{
    public class UsuarioControllerTests
    {
        [Fact]
        public void PostUsuarioComSucesso()
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

            var logger = LoggerFactory.Create(logger => logger.AddConsole())
                                      .CreateLogger<UsuarioController>();

            var controller = new UsuarioController(logger);

            var response = controller.CriarConta(dto);

            Assert.True(response is CreatedResult);

            //var responseContent = (response as CreatedResult).Value;
            //Assert.True(responseContent is DadosUsuario);
            //Assert.True((responseContent as DadosUsuario).Id != Guid.Empty);
        }
    }
}
