using Assessment.Streaming.Application.Infra;
using Assessment.Streaming.Domain.Models;
using Assessment.Streaming.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Streaming.Application
{
    public class BandaService
    {
        private BandaRepository Repository = new BandaRepository();
        public BandaService() { }

        public DadosBanda Criar(DadosBanda dto)
        {
            Banda banda = new Banda()
            {
                Descricao = dto.Descricao,
                Nome = dto.Nome,
            };

            if (dto.Albums != null)
            {
                foreach (var item in dto.Albums)
                {
                    Album album = new Album()
                    {
                        Id = Guid.NewGuid(),
                        Nome = item.Nome,
                    };

                    if (item.Musicas != null)
                    {
                        foreach (var musica in item.Musicas)
                        {
                            album.AdicionarMusicas(new Musica()
                            {
                                Duracao = new Domain.Objects.Duracao(musica.Duracao),
                                Nome = musica.Nome,
                                Album = album,
                                Id = Guid.NewGuid()
                            });
                        }
                    }
                    banda.AdicionarAlbum(album);
                }
            }
            this.Repository.Criar(banda);
            dto.Id = banda.Id;

            return dto;
        }

        public DadosBanda ObterBanda(Guid id)
        {
            var banda = this.Repository.ObterBanda(id);
            if (banda == null)
                return null;

            DadosBanda dto = new DadosBanda()
            {
                Id = banda.Id,
                Descricao = banda.Descricao,
                Nome = banda.Nome,
            };

            if (banda.Albums != null)
            {
                dto.Albums = new List<DadosAlbum>();

                foreach (var album in banda.Albums)
                {
                    DadosAlbum albumDto = new DadosAlbum()
                    {
                        Id = album.Id,
                        Nome = album.Nome,
                        Musicas = new List<DadosMusica>()
                    };

                    album.Musicas?.ForEach(m =>
                    {
                        albumDto.Musicas.Add(new DadosMusica()
                        {
                            Id = m.Id,
                            Duracao = m.Duracao.Valor,
                            Nome = m.Nome
                        });
                    });

                    dto.Albums.Add(albumDto);
                }
            }
            return dto;
        }

        public DadosMusica ObterMusica(Guid idMusica)
        {
            var musica = this.Repository.ObterMusica(idMusica);
            if (musica == null)
                return null;

            return new DadosMusica()
            {
                Duracao = musica.Duracao.Valor,
                Id = musica.Id,
                Nome = musica.Nome
            };
        }
    }
}
