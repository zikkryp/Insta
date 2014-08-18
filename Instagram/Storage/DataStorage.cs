using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Instagram.DataStructure;
using Instagram.Net;
using SQLite;

namespace Instagram.Storage
{
    class DataStorage
    {
        private DataStorage()
        {
            authData = null;
        }

        private const string storageName = "Storage.db";

        private static DataStorage _dataStorage;

        private AuthData authData;

        public static async Task<AuthData> GetAuthData()
        {
            _dataStorage = new DataStorage();

            await _dataStorage.GetAuthenticatedUser();

            if (_dataStorage.authData == null)
            {
            M:
                await Authorization.Authorize();
                await _dataStorage.GetAuthenticatedUser();

                if (_dataStorage.authData == null)
                {
                    goto M;   
                }
            }

            return _dataStorage.authData;
        }

        public static async Task Create(AuthData authData)
        {
            _dataStorage = new DataStorage();
            await _dataStorage.CreateStorage(authData);
        }

        private async Task CreateStorage(AuthData authData)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(storageName, CreationCollisionOption.OpenIfExists);

            using (SQLiteConnection connection = new SQLiteConnection(file.Path))
            {
                connection.CreateTable<AuthData>();
                connection.InsertOrReplace(authData);
            }
        }

        private async Task GetAuthenticatedUser()
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(storageName, CreationCollisionOption.OpenIfExists);

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(file.Path))
                {
                    authData = connection.Query<AuthData>("select * from AuthData where IsActive = 1").FirstOrDefault();
                }
            }
            catch(Exception)
            {
                authData = null;
            }
        }
    }
}
