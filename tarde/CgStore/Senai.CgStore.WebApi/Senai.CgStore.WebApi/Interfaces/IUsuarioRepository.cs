using Senai.CgStore.WebApi.Domains;
using Senai.CgStore.WebApi.ViewModels;

namespace Senai.CgStore.WebApi.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuarios BuscarPorEmailESenha(LoginViewModel login);
    }
}
