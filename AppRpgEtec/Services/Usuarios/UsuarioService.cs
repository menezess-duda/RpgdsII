using AppRpgEtec.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AppRpgEtec.Services.Usuarios
{
    public class UsuarioService : Request
    {
        private readonly Request _request;

        private const string apiUrlBase =
            "http://luizsilva12.somee.com/RpgApi/Usuarios";

        private string _token;

        public UsuarioService()
        {
            _request = new Request();
        }

        public UsuarioService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<Usuario> PostRegistrarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Registrar";

            u.Id = await _request.PostReturnIntAsync(
                apiUrlBase + urlComplementar,
                u,
                string.Empty);

            return u;
        }

        public async Task<Usuario> PostAutenticarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Autenticar";

            u = await _request.PostAsync(
                apiUrlBase + urlComplementar,
                u,
                string.Empty);

            return u;
        }

        public async Task<int> PutAtualizarLocalizacaoAsync(Usuario u)
        {
            string urlComplementar = "/AtualizarLocalizacao";

            var result = await _request.PutAsync(
                apiUrlBase + urlComplementar,
                u,
                _token);

            return result;
        }

        public async Task<ObservableCollection<Usuario>> GetUsuariosAsync()
        {
            string urlComplementar = "/GetAll";

            ObservableCollection<Usuario> listaUsuarios =
                await _request.GetAsync<ObservableCollection<Usuario>>(
                    apiUrlBase + urlComplementar,
                    _token);

            return listaUsuarios;
        }
    }
}