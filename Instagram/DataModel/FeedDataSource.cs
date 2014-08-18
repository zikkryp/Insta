using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Data.Json;
using Instagram.DataStructure;
using System.Collections.ObjectModel;
using Instagram.Net;
using Instagram.Storage;

namespace Instagram.DataModel
{
    public class FeedDataSource : DataParser
    {
        private static FeedDataSource _feedDataSource = new FeedDataSource();

        private ObservableCollection<FeedItem> _feed = new ObservableCollection<FeedItem>();
        public ObservableCollection<FeedItem> Feed { get { return _feed; } }

        public static async Task<IEnumerable<FeedItem>> GetFeedAsync()
        {
            _feedDataSource = new FeedDataSource();
            await _feedDataSource.GetDataAsync(string.Empty, false);

            return _feedDataSource.Feed;
        }

        public static async Task<IEnumerable<FeedItem>> LoadMoreAsync()
        {
            if (FeedDataSource.next_url != string.Empty)
            {
                await _feedDataSource.GetDataAsync(userid, true);
            }

            return _feedDataSource.Feed;
        }

        public static async Task<IEnumerable<FeedItem>> GetFeedAsync(string id)
        {
            _feedDataSource = new FeedDataSource();

            if (userid != id)
            {
                userid = id;
                FeedDataSource.next_url = string.Empty;
            }

            await _feedDataSource.GetDataAsync(id, false);

            return _feedDataSource.Feed;
        }

        private static string next_url = string.Empty;
        private static string userid = string.Empty;

        private async Task GetDataAsync(string userid, bool loadMore)
        {
            Answer answer = null;

            if (loadMore)
            {
                if (FeedDataSource.next_url != string.Empty)
                {
                    answer = await new Client().GetAnswerAsync(FeedDataSource.next_url);
                }
            }
            else
            {
                AuthData authData = await DataStorage.GetAuthData();
                if (userid.Equals(string.Empty))
                {
                    //answer = await new Client().GetAnswerAsync("https://api.instagram.com/v1/users/5946413/media/recent/?access_token=5946413.f59def8.7601270936b741c5a90161431a5df01f");
                    answer = await new Client().GetAnswerAsync("https://api.instagram.com/v1/users/self/feed?access_token=" + authData.Token);
                }
                else
                {
                    answer = await new Client().GetAnswerAsync("https://api.instagram.com/v1/users/" + userid + "/media/recent/?access_token=" + authData.Token);
                }
            }

            if (answer.Status == ResponseStatus.Success)
            {
                JsonObject jsonObject = JsonObject.Parse(answer.Content);
                JsonArray jsonArray = jsonObject["data"].GetArray();

                if (jsonObject.ContainsKey("pagination"))
                {
                    FeedDataSource.next_url = GetNextUrl(jsonObject["pagination"].GetObject());
                }

                foreach (JsonValue feedItem in jsonArray)
                {
                    var itemObject = feedItem.GetObject();

                    var tags = GetTags(itemObject["tags"].GetArray());
                    var Type = GetType(itemObject["type"].GetString());
                    var location = GetLocation(itemObject["location"]);
                    var comments = GetComments(itemObject["comments"].GetObject());
                    var filter = itemObject["filter"].GetString();
                    var createdTime = itemObject["created_time"].GetString();
                    var link = itemObject["link"].GetString();
                    var likes = GetLikes(itemObject["likes"].GetObject());
                    var images = GetImages(itemObject["images"].GetObject());
                    var usersInPhoto = GetUsersInPhoto(itemObject["users_in_photo"].GetArray());
                    var caption = GetCaption(itemObject["caption"]);
                    var userhasLiked = itemObject["user_has_liked"].GetBoolean();
                    var id = itemObject["id"].GetString();
                    var user = GetUser(itemObject["user"].GetObject());

                    FeedItem item;

                    if (Type == MediaType.Video)
                    {
                        var videos = GetVideos(itemObject["videos"].GetObject());

                        item = new FeedItem(null, tags, Type, location, comments, filter, createdTime, link, likes, images, usersInPhoto, caption, userhasLiked, id, user, videos);
                    }
                    else
                    {
                        item = new FeedItem(null, tags, Type, location, comments, filter, createdTime, link, likes, images, usersInPhoto, caption, userhasLiked, id, user);
                    }

                    this.Feed.Add(item);
                }
            }
            else if (answer.Status == ResponseStatus.ConnectionError)
            {
                await new Windows.UI.Popups.MessageDialog("Check your internet connection", "Connection problem").ShowAsync();
            }
            else if (answer.Status == ResponseStatus.InvalidToken)
            {
                await new Windows.UI.Popups.MessageDialog("Pass authorization once again to update token","Invalid Token").ShowAsync();
            }
        }
    }
}
