using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DataStructure
{
    public class Comments
    {
        public Comments(int count, IEnumerable<Comment> comments)
        {
            this.Count = count;
            this.ItemComments = comments;
        }

        public int Count { get; private set; }
        public IEnumerable<Comment> ItemComments { get; private set; }

        public override string ToString()
        {
            return this.Count.ToString();
        }
    }
}
