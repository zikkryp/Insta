using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using System.Net.Http;
using System.Net;

namespace Instagram.Net
{
    class Client
    {
        public async Task<Answer> GetAnswerAsync(string uri)
        {
            return await GetDataAsync(new Uri(uri));
        }

        public async Task<Answer> GetAnswerAsync(Uri uri)
        {
            return await GetDataAsync(uri);
        }

        public async Task<Answer> PostAsync(string api, string content)
        {
            Windows.Web.Http.HttpClient client = new Windows.Web.Http.HttpClient();

            try
            {
                var postContent = new Windows.Web.Http.HttpStringContent(content);
                Uri resourceAddress = new Uri(api);

                Windows.Web.Http.HttpResponseMessage response = await client.PostAsync(resourceAddress, postContent);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == Windows.Web.Http.HttpStatusCode.Ok)
                {                    
                    return new Answer(responseContent, ResponseStatus.Success);
                }
                else if (response.StatusCode == Windows.Web.Http.HttpStatusCode.BadRequest)
                {
                    return new Answer(responseContent, ResponseStatus.InvalidToken);
                }
                else if ((int)response.StatusCode == 429)
                {
                    return new Answer(responseContent, ResponseStatus.InvalidToken);
                }
            }
            catch (HttpRequestException)
            {
                client.Dispose();
            }
            catch (Exception)
            {
                client.Dispose();
            }

            Uri dataUri = new Uri("ms-appx:///DataModel/ConnectionError.json");
            var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await Windows.Storage.FileIO.ReadTextAsync(file);

            return new Answer(jsonText, ResponseStatus.ConnectionError);
        }

        public async Task<Answer> DeleteAsync(string api)
        {
            Windows.Web.Http.HttpClient client = new Windows.Web.Http.HttpClient();

            try
            {
                Uri resourceAddress = new Uri(api);

                var response = await client.DeleteAsync(resourceAddress);

                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == Windows.Web.Http.HttpStatusCode.Ok)
                {
                    return new Answer(responseContent, ResponseStatus.Success);
                }
                else if (response.StatusCode == Windows.Web.Http.HttpStatusCode.BadRequest)
                {
                    return new Answer(responseContent, ResponseStatus.InvalidToken);
                }
                else if ((int)response.StatusCode == 429)
                {
                    return new Answer(responseContent, ResponseStatus.InvalidToken);
                }
            }
            catch (HttpRequestException)
            {
                client.Dispose();
            }
            catch (Exception)
            {
                client.Dispose();
            }

            Uri dataUri = new Uri("ms-appx:///DataModel/ConnectionError.json");
            var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await Windows.Storage.FileIO.ReadTextAsync(file);

            return new Answer(jsonText, ResponseStatus.ConnectionError);
        }

        private async Task<Answer> GetDataAsync(Uri uri)
        {
            HttpClient client = new HttpClient();
            Uri dataUri = null;

            string responseContent = string.Empty;

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                responseContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    return new Answer(responseContent, ResponseStatus.Success);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return new Answer(responseContent, ResponseStatus.InvalidToken);
                }
                else if ((int)response.StatusCode == 429)
                {
                    return new Answer(responseContent, ResponseStatus.InvalidToken);
                }
            }
            catch (ArgumentNullException)
            {
                client.Dispose();
            }
            catch (HttpRequestException)
            {
                client.Dispose();
            }
            catch (NullReferenceException)
            {
                client.Dispose();
            }
            catch (Exception)
            {
                client.Dispose();
            }

            dataUri = new Uri("ms-appx:///DataModel/ConnectionError.json");

            var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await Windows.Storage.FileIO.ReadTextAsync(file);

            return new Answer(jsonText, ResponseStatus.ConnectionError);
        }
    }
}
