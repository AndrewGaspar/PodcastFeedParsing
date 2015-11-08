using System;
using System.Xml.Linq;

namespace Podcasts.Dom
{
    public class EnclosureElement : XElementHost
    {
        public XElement Enclosure => Element;

        internal EnclosureElement(XElement element) : base(element)
        {
        }

        private SetOnce<string> _type;
        public string Type => LazyLoadAttribute(ref _type, "type");

        private SetOnce<Uri> _url;
        public Uri Url => LazyLoadAttribute(ref _url, "url");

        private SetOnce<ulong?> _length;
        public ulong? Length => LazyLoadAttribute(ref _length, "length");
    }
}