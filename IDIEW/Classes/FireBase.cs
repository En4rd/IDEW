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
    class FireBase
    {
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

            public async Task<Dictionary<string, Form1.Perfil>> ObtenerPerfilesAsync()
            {
                FirebaseResponse response = await _client.GetAsync("Perfiles");
                return response.ResultAs<Dictionary<string, Form1.Perfil>>();
            }
        }
    }
}
