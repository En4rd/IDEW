using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace IDIEW.Classes
{
    public static class FireBase
    {
        public static PerfilService PerfilServiceInstance { get; private set; }

            static FireBase()
            {
                PerfilServiceInstance = new PerfilService(
                    "https://caducidad-de-programas-default-rtdb.firebaseio.com/",
                    "ulr7uQFvG2wKmBIxpvwMclkUSBS8ok9bmf9nUL2B"
                );
            }


            public class PerfilService
            {
            private readonly IFirebaseClient _client;

            public PerfilService(string basePath, string authSecret)
            {
                var config = new FirebaseConfig
                {
                    BasePath = basePath,
                    AuthSecret = authSecret
                };

                _client = new FireSharp.FirebaseClient(config);
            }

            public async Task<Dictionary<string, Perfil>> ObtenerPerfilesAsync()
            {
                FirebaseResponse response = await _client.GetAsync("Perfiles");
                return response.ResultAs<Dictionary<string, Perfil>>();
            }

            public async Task<bool> EliminarPerfilAsync(string perfilId)
            {
                FirebaseResponse response = await _client.DeleteAsync($"Perfiles/{perfilId}");
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }

            public async Task<bool> GuardarPerfilAsync(string nombre, Perfil perfil)
            {
                FirebaseResponse response = await _client.SetAsync($"Perfiles/{nombre}", perfil);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
        }
    }
}
