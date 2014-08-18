//using System;
//using System.Collections;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using Windows.UI.Xaml.Data;
//using System.Collections.Specialized;
//using System.Runtime.InteropServices;
//using System.Threading;
//using Windows.Foundation;
//using System.Runtime.InteropServices.WindowsRuntime;
//using Instagram.DataStructure;


//namespace Instagram.DataModel
//{
//    public class IncrementalLoadingBase : ObservableCollection<FeedItem> , ISupportIncrementalLoading, INotifyCollectionChanged
//    {
//        #region ISupportIncrementalLoading

//        public bool HasMoreItems
//        {
//            get { return HasMoreItemsOverride(); }
//        }

//        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
//        {
//            if (_busy)
//            {
//                throw new InvalidOperationException("Only one operation in flight at a time");
//            }

//            _busy = true;

//            return AsyncInfo.Run((c) => LoadMoreItemsAsync(c, count));
//        }

//        #endregion

//        #region INotifyCollectionChanged

//        public event NotifyCollectionChangedEventHandler CollectionChanged;

//        #endregion

//        #region Private methods

//        async Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken c, uint count)
//        {
//            try
//            {
//                //await new Windows.UI.Popups.MessageDialog(count.ToString()).ShowAsync();
//                //var items = await LoadMoreItemsOverrideAsync(c, count);

//                for (int i = 0; i < 50; i++)
//                {
//                    //this.Add(new FeedItem());
//                }

//                var items = this;//await FeedDataSource.GetFeedAsync();

//                var baseIndex = _storage.Count;

//                _storage.AddRange(items);

//                // Now notify of the new items
//                //NotifyOfInsertedItems(baseIndex, items.Count());

//                return new LoadMoreItemsResult { Count = (uint)items.Count() };
//            }
//            finally
//            {
//                _busy = false;
//            }
//        }

//        void NotifyOfInsertedItems(int baseIndex, int count)
//        {
//            if (CollectionChanged == null)
//            {
//                return;
//            }

//            for (int i = 0; i < count; i++)
//            {
//                //this.Add(new FeedItem());
//                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _storage[i + baseIndex], i + baseIndex);
//                CollectionChanged(this, args);
//            }
//        }

//        #endregion

//        #region Overridable methods

//        protected async Task<IEnumerable<FeedItem>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
//        {
//            //return await FeedDataSource.GetFeedAsync();
//            return null;
//        }

//        protected bool HasMoreItemsOverride() { return true; }

//        #endregion

//        #region State

//        List<FeedItem> _storage = new List<FeedItem>();
//        bool _busy = false;

//        #endregion
//    }
//}
