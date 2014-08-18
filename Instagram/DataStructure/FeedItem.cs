using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DataStructure
{
    public class FeedItem : DataFormatter
    {
        public FeedItem(object attribution, Tags tags, MediaType type, Location location, Comments comments, string filter, string createdTime, string link, Likes likes, Images images, UsersInPhoto usersInPhoto, Caption caption, bool userHasLiked, string id, User user)
        {
            this.Attribution = attribution;
            this.Tags = tags;
            this.Type = type;
            this.Location = location;
            this.Comments = comments;
            this.Filter = filter;
            this.CreatedTime = createdTime;
            this.Link = link;
            this.Likes = likes;
            this.Images = images;
            this.UsersInPhoto = usersInPhoto;
            this.Caption = caption;
            this.UserHasLiked = userHasLiked;
            this.Id = id;
            this.User = user;
        }

        public FeedItem(object attribution, Tags tags, MediaType type, Location location, Comments comments, string filter, string createdTime, string link, Likes likes, Images images, UsersInPhoto usersInPhoto, Caption caption, bool userHasLiked, string id, User user, Videos videos)
        {
            this.Attribution = attribution;
            this.Tags = tags;
            this.Type = type;
            this.Location = location;
            this.Comments = comments;
            this.Filter = filter;
            this.CreatedTime = createdTime;
            this.Link = link;
            this.Likes = likes;
            this.Images = images;
            this.UsersInPhoto = usersInPhoto;
            this.Caption = caption;
            this.UserHasLiked = userHasLiked;
            this.Id = id;
            this.User = user;
            this.Videos = videos;
        }

        public object Attribution { get; private set; }
        public Tags Tags { get; private set; }
        public MediaType Type { get; private set; }
        public Location Location { get; private set; }
        public Comments Comments { get; private set; }
        public string Filter { get; set; }
        public string Link { get; private set; }
        public Likes Likes { get; private set; }
        public Images Images { get; private set; }
        public UsersInPhoto UsersInPhoto { get; private set; }
        public Caption Caption { get; private set; }
        public bool UserHasLiked { get; private set; }
        public string Id { get; private set; }
        public User User { get; private set; }
        public Videos Videos { get; private set; }
        private string _createdTime;
        public string CreatedTime
        {
            get { return GetDate(_createdTime); }
            private set { _createdTime = value; }
        }

        private async void Message(object s)
        {
            await new Windows.UI.Popups.MessageDialog(this.Id).ShowAsync();
        }

        public override string ToString()
        {
            return this.Id;
        }
    }
}
