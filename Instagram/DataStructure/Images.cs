using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DataStructure
{
    public class Images
    {
        public Images(StandardResolution standard, LowResolution low, Thumbnail thumb)
        {
            this.Standard = standard;
            this.Low = low;
            this.Thumb = thumb;
        }

        public StandardResolution Standard { get; private set; }
        public LowResolution Low { get; private set; }
        public Thumbnail Thumb { get; private set; }

        public class StandardResolution
        {
            public StandardResolution(string url, double width, double height)
            {
                this.Url = url;
                this.Width = width;
                this.Height = height;
            }

            public string Url { get; private set; }
            public double Width { get; private set; }
            public double Height { get; private set; }
        }

        public class LowResolution
        {
            public LowResolution(string url, double width, double height)
            {
                this.Url = url;
                this.Width = width;
                this.Height = height;
            }

            public string Url { get; private set; }
            public double Width { get; private set; }
            public double Height { get; private set; }
        }

        public class Thumbnail
        {
            public Thumbnail(string url, double width, double height)
            {
                this.Url = url;
                this.Width = width;
                this.Height = height;
            }

            public string Url { get; private set; }
            public double Width { get; private set; }
            public double Height { get; private set; }
        }
    }
}
