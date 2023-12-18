using Assessment.Domain.Conta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Repository
{
    public class UsuarioRepository
    {
        private static List<Usuario> usuarios = new List<Usuario>();

        public Usuario GetUsuario(Guid id)
        {
            return UsuarioRepository
                        .usuarios
                        .FirstOrDefault(x => x.Id == id);
        }

        public void SaveUsuario(Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();
            UsuarioRepository.usuarios.Add(usuario);
        }

        public void Update(Usuario usuario)
        {
            Usuario usuarioOld = this.GetUsuario(usuario.Id);
            UsuarioRepository.usuarios.Remove(usuarioOld);
            UsuarioRepository.usuarios.Add(usuario);
        }
    }
}
