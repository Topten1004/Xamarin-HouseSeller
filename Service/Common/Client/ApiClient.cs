using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Service.Common.Url;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Immowert4You.Service.Common.Client
{
    public class ApiClient : IApiClient
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly RestClient _restClient;

        public ApiClient(ITokenRepository tokenRepository, IApiUrlProvider urlProvider)
        {
            _tokenRepository = tokenRepository;

            _restClient = new RestClient(urlProvider.Url)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            _restClient.UseNewtonsoftJson();
        }

        private class RepeatPleaseException : Exception
        { }

        public async Task<T> SendRequestWithResponseAsync<T>(string endpoint, object data = default, Method method = Method.GET, CancellationToken cancelationToken = default)
        {
            try
            {
                var request = GetRequest(endpoint, data);

                var response = await _restClient.ExecuteAsync<T>(request, method, cancelationToken);

                if (!response.IsSuccessful)
                    HandleResponseError(response.ErrorException, response.Content);

                return response.Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SendRequestAsync(string endpoint, object data = default, Method method = Method.POST, CancellationToken cancelationToken = default)
        {
            try
            {
                var request = GetRequest(endpoint, data);

                var response = await _restClient.ExecuteAsync(request, method, cancelationToken);

                if (!response.IsSuccessful)
                    HandleResponseError(response.ErrorException, response.Content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SendFile(string endpoint, byte[] bytes, string fileName)
        {
            var request = GetRequest(endpoint, default);

            string extension = Path.GetExtension(fileName).Replace(".", "");
            request.AddFile("photo", bytes, fileName, $"image/{extension}");
            request.AddHeader("Content-Type", "multipart/form-data");

            var response = await _restClient.ExecuteAsync(request, Method.POST, default);

            if (!response.IsSuccessful)
                HandleResponseError(response.ErrorException, response.Content);
        }

        private IRestRequest GetRequest(string endpoint, object data)
        {
            var request = new RestRequest(endpoint);

            if (data != null)
                request.AddJsonBody(data);

            var token = _tokenRepository.GetToken();

            if (!string.IsNullOrEmpty(token))
            { 
                request.AddHeader("Authorization", "Bearer " + token);
            }

            return request;
        }
        private void HandleResponseError(Exception exception, string content)
        {
            if (string.IsNullOrEmpty(content))
                throw exception;

            // TODO return 401 on invalid token
            if (content.Contains("Microsoft.IdentityModel.Tokens"))
            {
                _tokenRepository.SetToken(String.Empty);

                throw new Exception("Anmeldung von mehreren Geräten erkannt. Bitte melden Sie sich erneut an");
            }
                

            throw new Exception(content);
        }
    }
}
