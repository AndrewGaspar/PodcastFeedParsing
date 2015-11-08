using System;
using System.Xml.Linq;

namespace Podcasts.Dom
{
    public class ITunesImageElement : XElementHost
    {
        public ITunesImageElement(XElement element) : base(element, Constants.ITunesNamespace)
        {
        }

        public static ITunesImageElement TryCreate(XElementHost host)
        {
            var node = host.SelectSingleElement("image");
            return node == null ? null : new ITunesImageElement(node);
        }

        private SetOnce<Uri> _href;
        public Uri Href => LazyLoadAttribute(ref _href, "href");
    }
}