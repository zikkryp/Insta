using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.Data.Json;
using Instagram.DataStructure;

namespace Instagram.DataModel
{
    public class LikesDataSource : DataParser
    {
        private static LikesDataSource _likesDataSource;

        private ObservableCollection<User> _liked = new ObservableCollection<User>();
        
        public ObservableCollection<User> Liked
        {
            get { return this._liked; }
        }

        public static async Task<IEnumerable<User>> GetLikesAsync()
        {
            _likesDataSource = new LikesDataSource();
            await _likesDataSource.GetDataAsync();
            return _likesDataSource.Liked;
        }

        private async Task GetDataAsync()
        {
            Uri dataUri = new Uri("ms-appx:///DataModel/Likes.json");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["data"].GetArray();

            foreach (JsonValue itemValue in jsonArray)
            {
                var itemObject = itemValue.GetObject();
                
                Liked.Add(GetUser(itemObject));
            }
        }
    }
}
