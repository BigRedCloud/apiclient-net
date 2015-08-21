using BigRedCloud.Api.Components;
using BigRedCloud.Api.Configuration;
using BigRedCloud.Api.Exceptions;
using BigRedCloud.Api.Model.Batch;
using BigRedCloud.Api.Model.Querying;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BigRedCloud.Api.Clients
{
    public abstract class BaseApiClient
    {
        private static readonly HttpClient HttpClientInstance;

        private readonly string _apiKeyBase64;
        private readonly string _entitiesName;
        
        static BaseApiClient()
        {
            BigRedCloudApiSection apiConfigSection = (BigRedCloudApiSection)ConfigurationManager.GetSection("bigRedCloudApiSection");
            HttpClientInstance = CreateHttpClient(apiConfigSection.ApiServerUrl);
        }

        protected BaseApiClient(string apiKey, string entitiesName)
        {
            _apiKeyBase64 = Utils.EncodeToBase64(apiKey);
            _entitiesName = entitiesName;
        }

        protected HttpClient HttpClient
        {
            get { return HttpClientInstance; }
        }


        #region Object CRUD methods

        protected async Task<ODataResult<TApiDto>> GetPageByApiAsync<TApiDto>(string odataParameters = null)
        {
            string requestUri = String.IsNullOrEmpty(odataParameters) ?
                _entitiesName :
                String.Format("{0}?{1}", _entitiesName, odataParameters);

            return await GetByApiAsync<ODataResult<TApiDto>>(requestUri).ConfigureAwait(false);
        }

        protected async Task<TApiDto> GetByApiAsync<TApiDto>(long id)
        {
            string requestUri = String.Format("{0}/{1}", _entitiesName, id);
            return await GetByApiAsync<TApiDto>(requestUri).ConfigureAwait(false);
        }

        protected async Task<TApiDto> GetByApiAsync<TApiDto>(string requestUri)
        {
            using (HttpResponseMessage response = await GetByApiRawAsync(requestUri).ConfigureAwait(false))
            {
                await EnsureSuccessAsync(response).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TApiDto>().ConfigureAwait(false);
            }
        }

        protected async Task<long> PostByApiAsync<TApiDto>(TApiDto apiDto)
        {
            using (HttpResponseMessage response = await PostByApiRawAsync(apiDto).ConfigureAwait(false))
            {
                await EnsureSuccessAsync(response).ConfigureAwait(false);
                long createdItemId = GetItemIdFromPostUri(response.Headers.Location);
                return createdItemId;
            }
        }

        protected async Task PutByApiAsync<TApiDto>(long id, TApiDto apiDto)
        {
            using (HttpResponseMessage response = await PutByApiRawAsync(id, apiDto).ConfigureAwait(false))
            {
                await EnsureSuccessAsync(response).ConfigureAwait(false);
            }
        }

        protected async Task DeleteByApiAsync(long id, byte[] timestamp)
        {
            using (HttpResponseMessage response = await DeleteByApiRawAsync(id, timestamp).ConfigureAwait(false))
            {
                await EnsureSuccessAsync(response).ConfigureAwait(false);
            }
        }

        protected async Task<BatchItemProcessResult<TApiDto>[]> BatchByApiAsync<TApiDto>(BatchItem<TApiDto>[] batchItems)
        {
            using (HttpResponseMessage response = await BatchByApiRawAsync(batchItems).ConfigureAwait(false))
            {
                await EnsureSuccessAsync(response).ConfigureAwait(false);
                BatchItemProcessResult<TApiDto>[] result = await response.Content.ReadAsAsync<BatchItemProcessResult<TApiDto>[]>().ConfigureAwait(false);
                return result;
            }
        }

        #endregion Object CRUD methods


        #region Raw CRUD methods
        
        protected async Task<HttpResponseMessage> GetByApiRawAsync(string requestUri)
        {
            return await SendToApiRawAsync(HttpMethod.Get, requestUri).ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> PostByApiRawAsync<TApiDto>(TApiDto apiDto)
        {
            return await SendToApiRawAsync(HttpMethod.Post, _entitiesName, apiDto).ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> PutByApiRawAsync<TApiDto>(long id, TApiDto apiDto)
        {
            string requestUri = String.Format("{0}/{1}", _entitiesName, id);
            return await SendToApiRawAsync(HttpMethod.Put, requestUri, apiDto).ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> DeleteByApiRawAsync(long id, byte[] timestamp)
        {
            string timestampAsString = Utils.ConvertTimestampToBase64UrlString(timestamp);
            string requestUri = String.Format("{0}/{1}?timestamp={2}", _entitiesName, id, timestampAsString);
            return await SendToApiRawAsync(HttpMethod.Delete, requestUri).ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> BatchByApiRawAsync<TApiDto>(BatchItem<TApiDto>[] batchItems)
        {
            string requestUri = String.Format("{0}/batch", _entitiesName);
            return await SendToApiRawAsync(HttpMethod.Put, requestUri, batchItems).ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> SendToApiRawAsync(HttpMethod httpMethod, string requestUri)
        {
            HttpRequestMessage request = GetRequestMessage(httpMethod, requestUri);
            HttpResponseMessage response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            return response;
        }

        protected async Task<HttpResponseMessage> SendToApiRawAsync<TApiDto>(HttpMethod httpMethod, string requestUri, TApiDto content)
        {
            HttpRequestMessage request = GetRequestMessage(httpMethod, requestUri, content);
            HttpResponseMessage response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            return response;
        }

        #endregion Raw CRUD methods


        #region Protected helper methods

        protected static HttpClient CreateHttpClient(string apiServerUrl)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(apiServerUrl) };
            return client;
        }

        protected static async Task EnsureSuccessAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                HttpStatusCode statusCode = response.StatusCode;
                string reasonPhrase = response.ReasonPhrase;
                string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                response.Dispose();
                throw new ApiRequestException(statusCode, reasonPhrase, content);
            }
        }

        protected static long GetItemIdFromPostUri(Uri itemUri)
        {
            string itemIdAsString = itemUri.Segments[itemUri.Segments.Length - 1];
            return Int64.Parse(itemIdAsString);
        }
        
        #endregion Protected helper methods


        #region Private methods

        protected HttpRequestMessage GetRequestMessage(HttpMethod httpMethod, string requestUri)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(httpMethod, requestUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", _apiKeyBase64);
            return requestMessage;
        }

        private HttpRequestMessage GetRequestMessage<TApiDto>(HttpMethod httpMethod, string requestUri, TApiDto contentValue)
        {
            HttpRequestMessage requestMessage = GetRequestMessage(httpMethod, requestUri);
            JsonMediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }
            };
            requestMessage.Content = new ObjectContent<TApiDto>(contentValue, jsonFormatter);
            return requestMessage;
        }

        #endregion Private methods
    }
}
