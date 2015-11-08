using System;
using System.Xml.Linq;

namespace Podcasts.Dom
{
    /// <summary>
    /// http://cyber.law.harvard.edu/rss/rss.html#hrelementsOfLtitemgt
    /// </summary>
    public class PodcastFeedItem : XElementHost
    {
        public class ITunesNS : XElementHost
        {
            internal ITunesNS(XElement element) : base(element, Constants.ITunesNamespace)
            {
            }

            private SetOnce<string> _author;
            public string Author => LazyLoadContent(ref _author, "author");

            private SetOnce<YesEnum?> _block;
            public YesEnum? Block => LazyLoadContent(ref _block, "block");

            private ITunesImageElement _image;
            public ITunesImageElement Image => _image ?? (_image = ITunesImageElement.TryCreate(this));

            private SetOnce<TimeSpan?> _duration;
            public TimeSpan? Duration => LazyLoadContent(ref _duration, "duration");

            private SetOnce<ExplicitEnum?> _explicit;
            public ExplicitEnum? Explicit => LazyLoadContent(ref _explicit, "explicit");

            private SetOnce<YesEnum?> _isClosedCaption;
            public YesEnum? IsClosedCaption => LazyLoadContent(ref _isClosedCaption, "isClosedCaption");

            private SetOnce<uint?> _order;
            public uint? Order => LazyLoadContent(ref _order, "order");

            private SetOnce<string> _subtitle;
            public string Subtitle => LazyLoadContent(ref _subtitle, "subtitle");

            private SetOnce<string> _summary;
            public string Summary => LazyLoadContent(ref _summary, "summary");
        }

        public XElement Item => Element;

        internal PodcastFeedItem(XElement element) : base(element)
        {
        }

        private SetOnce<string> _title;
        public string Title => LazyLoadContent(ref _title, "title");

        private SetOnce<Uri> _link;
        public Uri Link => LazyLoadContent(ref _link, "link");

        private SetOnce<string> _description;
        public string Description => LazyLoadContent(ref _description, "description");

        private SetOnce<string> _author;
        public string Author => LazyLoadContent(ref _author, "author");

        private SetOnce<string> _category;
        public string Category => LazyLoadContent(ref _category, "category");

        private SetOnce<Uri> _comments;
        public Uri Comments => LazyLoadContent(ref _comments, "comments");

        private SetOnce<EnclosureElement> _enclosure;
        public EnclosureElement Enclosure
        {
            get
            {
                if (_enclosure == null)
                {
                    var element = Item.Element("enclosure");

                    _enclosure = SetOnce<EnclosureElement>.Create(element == null ? null : new EnclosureElement(element));
                }

                return _enclosure.Extract();
            }
        }

        private SetOnce<string> _guid;
        public string Guid => LazyLoadContent(ref _guid, "guid");

        private SetOnce<DateTime?> _pubDate;
        public DateTime? PubDate => LazyLoadContent(ref _pubDate, "pubDate");

        private SetOnce<string> _source;
        public string Source => LazyLoadContent(ref _source, "source");

        private ITunesNS _itunes;
        public ITunesNS ITunes => _itunes ?? (_itunes = new ITunesNS(Element));
    }
}