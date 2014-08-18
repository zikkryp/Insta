using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Instagram.Net;
using Instagram.DataStructure;

namespace Instagram.DataModel
{
    class UserInfoSource : DataParser
    {
        private static UserInfoSource _userUnfo;

        private User user { get; set; }

        public static async Task<User> GetUser(int id)
        {
            _userUnfo = new UserInfoSource();
            await _userUnfo.GetDataAsync(id.ToString());

            return _userUnfo.user;
        }

        public static async Task<User> GetUser(int id, string token)
        {
            _userUnfo = new UserInfoSource();
            await _userUnfo.GetDataAsync(id.ToString(), token);
            return _userUnfo.user;
        }

        private async Task GetDataAsync(string id)
        {
            var authData = await Instagram.Storage.DataStorage.GetAuthData();
            var answer = await new Client().GetAnswerAsync("https://api.instagram.com/v1/users/" + id + "/?access_token=" + authData.Token);

            if (answer.Status == ResponseStatus.Success)
            {
                var jsonObject = JsonObject.Parse(answer.Content);
                
                var userObject = jsonObject["data"].GetObject();

                var counts = GetCounts(userObject["counts"].GetObject());

                user = new User(
                    userObject["username"].GetString(),
                    userObject["profile_picture"].GetString(),
                    userObject["full_name"].GetString(),
                    userObject["id"].GetString(),
                    userObject["website"].GetString(),
                    userObject["bio"].GetString(),
                    counts);
            }
        }

        private async Task GetDataAsync(string id, string token)
        {
            var answer = await new Client().GetAnswerAsync("https://api.instagram.com/v1/users/" + id + "/?access_token=" + token);

            if (answer.Status == ResponseStatus.Success)
            {
                var jsonObject = JsonObject.Parse(answer.Content);

                var userObject = jsonObject["data"].GetObject();

                var counts = GetCounts(userObject["counts"].GetObject());

                user = new User(
                    userObject["username"].GetString(),
                    userObject["profile_picture"].GetString(),
                    userObject["full_name"].GetString(),
                    userObject["id"].GetString(),
                    userObject["website"].GetString(),
                    userObject["bio"].GetString(),
                    counts);
            }
        }
    }
}
