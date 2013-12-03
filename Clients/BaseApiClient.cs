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

        protected ODataResult<TApiDto> GetPageByApi<TApiDto>(string odataParameters = null)
        {
            string requestUri = String.IsNullOrEmpty(odataParameters) ?
                _entitiesName :
                String.Format("{0}?{1}", _entitiesName, odataParameters);

            return GetByApi<ODataResult<TApiDto>>(requestUri);
        }

        protected TApiDto GetByApi<TApiDto>(long id)
        {
            string requestUri = String.Format("{0}/{1}", _entitiesName, id);
            return GetByApi<TApiDto>(requestUri);
        }

        protected TApiDto GetByApi<TApiDto>(string requestUri)
        {
            using (HttpResponseMessage response = GetByApiRaw(requestUri))
            {
                EnsureSuccess(response);

                TApiDto result = response.Content.ReadAsAsync<TApiDto>().Result;
                return result;
            }
        }

        protected long PostByApi<TApiDto>(TApiDto apiDto)
        {
            using (HttpResponseMessage response = PostByApiRaw(apiDto))
            {
                EnsureSuccess(response);
                long createdItemId = GetItemIdFromPostUri(response.Headers.Location);
                
                return createdItemId;
            }
        }

        protected void PutByApi<TApiDto>(long id, TApiDto apiDto)
        {
            using (HttpResponseMessage response = PutByApiRaw(id, apiDto))
            {
                EnsureSuccess(response);
            }
        }

        protected void DeleteByApi(long id, byte[] timestamp)
        {
            using (HttpResponseMessage response = DeleteByApiRaw(id, timestamp))
            {
                EnsureSuccess(response);
            }
        }

        protected BatchItemProcessResult[] BatchByApi<TApiDto>(BatchItem<TApiDto>[] batchItems)
        {
            using (HttpResponseMessage response = BatchByApiRaw(batchItems))
            {
                EnsureSuccess(response);
                
                BatchItemProcessResult[] result = response.Content.ReadAsAsync<BatchItemProcessResult[]>().Result;
                return result;
            }
        }

        #endregion Object CRUD methods


        #region Raw CRUD methods
        
        protected HttpResponseMessage GetByApiRaw(string requestUri)
        {
            HttpRequestMessage request = GetRequestMessage(HttpMethod.Get, requestUri);
            HttpResponseMessage response = HttpClient.SendAsync(request).Result;
            return response;
        }

        protected HttpResponseMessage PostByApiRaw<TApiDto>(TApiDto apiDto)
        {
            HttpRequestMessage request = GetRequestMessage(HttpMethod.Post, _entitiesName, apiDto);
            HttpResponseMessage response = HttpClient.SendAsync(request).Result;
            return response;
        }

        protected HttpResponseMessage PutByApiRaw<TApiDto>(long id, TApiDto apiDto)
        {
            string requestUri = String.Format("{0}/{1}", _entitiesName, id);
            HttpRequestMessage request = GetRequestMessage(HttpMethod.Put, requestUri, apiDto);
            HttpResponseMessage response = HttpClient.SendAsync(request).Result;
            return response;
        }

        protected HttpResponseMessage DeleteByApiRaw(long id, byte[] timestamp)
        {
            string timestampAsString = Utils.ConvertTimestampToBase64UrlString(timestamp);
            string requestUri = String.Format("{0}/{1}?timestamp={2}", _entitiesName, id, timestampAsString);
            HttpRequestMessage request = GetRequestMessage(HttpMethod.Delete, requestUri);
            HttpResponseMessage response = HttpClient.SendAsync(request).Result;
            return response;
        }

        protected HttpResponseMessage BatchByApiRaw<TApiDto>(BatchItem<TApiDto>[] batchItems)
        {
            string requestUri = String.Format("{0}/batch", _entitiesName);
            HttpRequestMessage request = GetRequestMessage(HttpMethod.Put, requestUri, batchItems);
            HttpResponseMessage response = HttpClient.SendAsync(request).Result;
            return response;
        }

        #endregion Raw CRUD methods


        #region Protected helper methods

        protected static HttpClient CreateHttpClient(string apiServerUrl)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(apiServerUrl) };
            return client;
        }

        protected static void EnsureSuccess(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                HttpStatusCode statusCode = response.StatusCode;
                string reasonPhrase = response.ReasonPhrase;
                string content = response.Content.ReadAsStringAsync().Result;
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

        private HttpRequestMessage GetRequestMessage(HttpMethod httpMethod, string requestUri)
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
