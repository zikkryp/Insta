using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DataStructure
{
    public class Videos
    {
        public Videos(LowBandwidth lowBandwidth, LowResolution lowResolution, StandardResolution standardResolution)
        {
            this.VideoLowBandwidth = lowBandwidth;
            this.VideoLowResolution = lowResolution;
            this.VideoStandardResolution = standardResolution;
        }

        public StandardResolution VideoStandardResolution { get; private set; }
        public LowResolution VideoLowResolution { get; private set; }
        public LowBandwidth VideoLowBandwidth { get; private set; }

        public class LowBandwidth
        {
            public LowBandwidth(string url, double width, double height)
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

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
