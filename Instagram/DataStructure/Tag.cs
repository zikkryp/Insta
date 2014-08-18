using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DataStructure
{
    public class Tag
    {
        public Tag(string tag)
        {
            this.ItemTag = tag;
        }

        private string tag;
        public string ItemTag
        {
            get { return "#" + tag; }
            private set { tag = value; }
        }

        public override string ToString()
        {
            return this.ItemTag;
        }
    }
}
