using Assessment.Application.Conta.Infra;
using Assessment.Core.Exception;
using Assessment.Domain.Conta.Models;
using Assessment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Application.Conta
{
    public class UsuarioService
    {
        private UsuarioRepository usuarioRepository = new UsuarioRepository();
        private PlanoRepository planoRepository = new PlanoRepository();
        private BandaRepository bandaRepository = new BandaRepository();

        public async Task<DadosUsuario> CriarConta(DadosUsuario conta)
        {
            Plano plano = await this.planoRepository.GetPlano(conta.PlanoId);

            if (plano == null)
            {
                new ModelsException(new ModelsValidation()
                {
                    ErrorMessage = "Plano não encontrado",
                    ErrorName = nameof(CriarConta)
                }).ValidateAndThrow();
            }


            Cartao cartao = new Cartao();
            cartao.Ativo = conta.Cartao.Ativo;
            cartao.Numero = conta.Cartao.Numero;
            cartao.Limite = conta.Cartao.Limite;
            Usuario usuario = new Usuario();
            usuario.CriaUsuario(conta.Nome, conta.Cpf, plano, cartao);
            this.usuarioRepository.SaveUsuario(usuario);
            conta.Id = usuario.Id;

            return conta;
        }

        public DadosUsuario ObterUsuario(Guid id)
        {
            var usuario = this.usuarioRepository.GetUsuario(id);

            if (usuario == null)
                return null;

            DadosUsuario result = new DadosUsuario()
            {
                Id = usuario.Id,
                Cartao = new DadosCartao()
                {
                    Ativo = usuario.Cartoes.FirstOrDefault().Ativo,
                    Limite = usuario.Cartoes.FirstOrDefault().Limite,
                    Numero = "xxxx-xxxx-xxxx-xx"
                },
                Cpf = usuario.Cpf.NumeroFormatado(),
                Nome = usuario.Nome,
                Playlists = new List<DadosPlaylist>()
            };

            foreach (var item in usuario.Playlists)
            {
                var playList = new DadosPlaylist()
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Publica = item.Publica,
                    Musicas = new List<Conta.Infra.DadosMusica>()
                };

                foreach (var musicas in item.Musicas)
                {
                    playList.Musicas.Add(new Conta.Infra.DadosMusica()
                    {
                        Duracao = musicas.Duracao,
                        Id = musicas.Id,
                        Nome = musicas.Nome
                    });
                }
                result.Playlists.Add(playList);
            }
            return result;
        }

        public async Task FavoritaMusica(Guid id, Guid idMusica)
        {
            var usuario = this.usuarioRepository.GetUsuario(id);

            if (usuario == null)
            {
                throw new ModelsException(new ModelsValidation()
                {
                    ErrorMessage = "Usuario nao foi encontrado",
                    ErrorName = nameof(FavoritaMusica)
                });
            }

            var musica = await this.bandaRepository.GetMusica(idMusica);

            if (musica == null)
            {
                throw new ModelsException(new ModelsValidation()
                {
                    ErrorMessage = "Musica nao foi encontrada",
                    ErrorName = nameof(FavoritaMusica)
                });
            }

            usuario.Favoritar(musica);
            this.usuarioRepository.Update(usuario);
        }
    }
}
