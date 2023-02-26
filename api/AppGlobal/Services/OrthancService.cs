using System.Threading.Tasks;
using System.Security.Cryptography;
using AppGlobal.Commons;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Collections.Generic;

namespace AppGlobal.Services
{
    public class OrthancService : RestApiClient
    {
        private readonly string ApiBaseUrl;

        // DI environment config and check if prod/dev + pass http client to parent
        public OrthancService(IHttpClientFactory factory, IConfiguration configuration) : base(factory.CreateClient())
            => ApiBaseUrl = configuration["OrthancUrl"];

        public List<string> GetAllStudyIDs()
            => GetRequest<List<string>>($"{ApiBaseUrl}/studies");
    }
}
