using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Instagram.Net
{
    class Answer
    {
        public Answer(string Content, ResponseStatus status)
        {
            this.Status = status;
            this.Content = Content;
        }

        public ResponseStatus Status { get; private set; }
        public string Content { get; private set; }
    }
}
