using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instagram.Net;
using Instagram.DataStructure;
using Instagram.Storage;

namespace Instagram.DataModel
{
    class Liker
    {
        public Liker()
        {
            client = new Client();
        }

        Client client;

        public async Task<string> PostLike(string id)
        {
            string token = (await DataStorage.GetAuthData()).Token;

            string api = "https://api.instagram.com/v1/media/" + id + "/likes";

            var answer = await client.PostAsync(api, "access_token=" + token);

            if (answer.Status == ResponseStatus.Success)
            {
                return answer.Content;
            }
            else if (answer.Status == ResponseStatus.InvalidToken)
            {
                return "Update token";
            }
            else if (answer.Status == ResponseStatus.ConnectionError)
            {
                return "Connection problem";
            }

            return string.Format("Unknown error");
        }

        public async Task<string> DeleteLike(string id)
        {
            string token = (await DataStorage.GetAuthData()).Token;
            string api = "https://api.instagram.com/v1/media/" + id + "/likes?access_token=" + token;

            var answer = await client.DeleteAsync(api);

            if (answer.Status == ResponseStatus.Success)
            {
                return answer.Content;
            }
            else if (answer.Status == ResponseStatus.InvalidToken)
            {
                return "Update token";
            }
            else if (answer.Status == ResponseStatus.ConnectionError)
            {
                return "Connection problem";
            }

            return string.Format("Unknown error");
        }
    }
}
