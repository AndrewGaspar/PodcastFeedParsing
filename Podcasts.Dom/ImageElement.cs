using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Podcasts.Dom
{
    public class ImageElement : XElementHost
    {
        public XElement Image => Element;

        public ImageElement(XElement element) : base(element)
        {
        }

        private SetOnce<Uri> _url;
        public Uri Url => LazyLoadContent(ref _url, "url");

        private SetOnce<string> _title;
        public string Title => LazyLoadContent(ref _title, "title");

        private SetOnce<Uri> _link;
        public Uri Link => LazyLoadContent(ref _link, "link");

        private SetOnce<uint?> _width;
        public uint? Width => LazyLoadContent(ref _width, "width");

        private SetOnce<uint?> _height;
        public uint? Height => LazyLoadContent(ref _height, "height");

        private SetOnce<string> _description;
        public string Description => LazyLoadContent(ref _description, "description");
    }
}
