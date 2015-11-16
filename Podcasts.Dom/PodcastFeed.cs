using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Podcasts.Dom
{
    using System.IO;
    using System.Net.Http;
    using System.Xml;
    using Utils;

    /// <summary>
    /// http://cyber.law.harvard.edu/rss/rss.html
    /// </summary>
    public class PodcastFeed : XElementHost
    {
        private XDocument Document;

        public class ITunesNS : XElementHost
        {
            public class CategoryNode : XElementHost
            {
                public XElement Category => Element;

                public CategoryNode(XElement element) : base(element, Constants.ITunesNamespace)
                {
                }

                internal static CategoryNode TryCreate(XElementHost host)
                {
                    var categoryNode = host.SelectSingleElement("category");

                    return categoryNode == null ? null : new CategoryNode(categoryNode);
                }

                private CategoryNode TryCreate()
                {
                    return TryCreate(this);
                }

                private SetOnce<CategoryNode> _category;
                public CategoryNode SubCategories
                {
                    get
                    {
                        if(_category == null)
                        {
                            var element = SelectSingleElement("category");

                            _category = SetOnce<CategoryNode>.Create(element == null ? null : new CategoryNode(element));
                        }

                        return _category.Extract();
                    }
                }

                private SetOnce<string> _text;
                public string Text => LazyLoadAttribute(ref _text, "text");
            }

            public class OwnerElement : XElementHost
            {
                public OwnerElement(XElement element) : base(element, Constants.ITunesNamespace)
                {
                }

                private SetOnce<string> _email;
                public string Email => LazyLoadContent(ref _email, "email");

                private SetOnce<string> _name;
                public string Name => LazyLoadContent(ref _name, "name");
            }

            public ITunesNS(XElement element) : base(element, Constants.ITunesNamespace)
            {
            }

            private SetOnce<string> _author;
            public string Author => LazyLoadContent(ref _author, "author");

            private SetOnce<YesEnum?> _block;
            public YesEnum? Block => LazyLoadContent(ref _block, "block");

            private IEnumerable<CategoryNode> _categories;
            public IEnumerable<CategoryNode> Categories =>
                _categories ?? (_categories = SelectElements("category").Select(node => new CategoryNode(node)).CacheResults());

            private SetOnce<ITunesImageElement> _image;
            public ITunesImageElement Image
            {
                get
                {
                    if(_image == null)
                    {
                        _image = SetOnce<ITunesImageElement>.Create(ITunesImageElement.TryCreate(this));
                    }

                    return _image.Extract();
                }
            }

            private SetOnce<ExplicitEnum?> _explicit;
            public ExplicitEnum? Explicit => LazyLoadContent(ref _explicit, "explicit");

            private SetOnce<YesEnum?> _complete;
            public YesEnum? Complete => LazyLoadContent(ref _complete, "complete");

            private SetOnce<Uri> _newFeedUrl;
            public Uri NewFeedUrl => LazyLoadContent(ref _newFeedUrl, "new-feed-url");

            private SetOnce<OwnerElement> _owner;
            public OwnerElement Owner
            {
                get
                {
                    if (_owner == null)
                    {
                        var element = SelectSingleElement("owner");

                        _owner = SetOnce<OwnerElement>.Create(element == null ? null : new OwnerElement(element));
                    }

                    return _owner.Extract();
                }
            }

            private SetOnce<string> _subtitle;
            public string Subtitle => LazyLoadContent(ref _subtitle, "subtitle");

            private SetOnce<string> _summary;
            public string Summary => LazyLoadContent(ref _summary, "summary");
        }

        public XElement Channel => Element;

        public PodcastFeed(XDocument dom) : base(dom.Element("rss").Element("channel"))
        {
            Document = dom;
        }

        private SetOnce<string> _title;
        public string Title => LazyLoadContent(ref _title, "title");

        private SetOnce<string> _description;
        public string Description => LazyLoadContent(ref _description, "description");

        private SetOnce<Uri> _link;
        public Uri Link => LazyLoadContent(ref _link, "link");

        private SetOnce<string> _language;
        public string Language => LazyLoadContent(ref _language, "language");

        private SetOnce<string> _copyright;
        public string Copyright => LazyLoadContent(ref _copyright, "copyright");

        private SetOnce<string> _managingEditor;
        public string ManagingEditor => LazyLoadContent(ref _managingEditor, "managingEditor");

        private SetOnce<string> _webMaster;
        public string WebMaster => LazyLoadContent(ref _webMaster, "webMaster");

        private SetOnce<DateTime?> _pubDate;
        public DateTime? PubDate => LazyLoadContent(ref _pubDate, "pubDate");

        private SetOnce<DateTime?> _lastBuildDate;
        public DateTime? LastBuildDate => LazyLoadContent(ref _lastBuildDate, "lastBuildDate");

        private SetOnce<string> _category;
        public string Category => LazyLoadContent(ref _category, "category");

        private SetOnce<string> _generator;
        public string Generator => LazyLoadContent(ref _generator, "generator");

        private SetOnce<Uri> _docs;
        public Uri Docs => LazyLoadContent(ref _docs, "docs");

        private SetOnce<uint?> _ttl;
        public uint? TimeToLive => LazyLoadContent(ref _ttl, "ttl");

        private IReadOnlyList<PodcastFeedItem> _items;
        public IReadOnlyList<PodcastFeedItem> Items =>
            _items ?? (_items = Channel.Elements("item").Select(node => new PodcastFeedItem(node)).CacheResults());

        private ImageElement TryGetImageElement()
        {
            var element = Channel.Element("image");
            if (element == null)
            {
                return null;
            }

            return new ImageElement(element);
        }

        private SetOnce<ImageElement> _image;
        public ImageElement Image
        {
            get
            {
                if (_image == null)
                {
                    _image = SetOnce<ImageElement>.Create(TryGetImageElement());
                }

                return _image.Extract();
            }
        }

        private ITunesNS _itunes;
        public ITunesNS ITunes => _itunes ?? (_itunes = new ITunesNS(Channel));
        
        public static async Task<PodcastFeed> LoadFeedAsync(Uri location)
        {
            using (var stream = await PodcastDomConfiguration.Current.UriResolution.GetStreamAsync(location).ConfigureAwait(false))
            {
                return await LoadFeedAsync(stream).ConfigureAwait(false);
            }
        }

        public static async Task<PodcastFeed> LoadFeedAsync(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return await LoadFeedAsync(reader).ConfigureAwait(false);
            }
        }

        public static async Task<PodcastFeed> LoadFeedAsync(TextReader reader)
        {
            var content = await reader.ReadToEndAsync().ConfigureAwait(false);
            
            return new PodcastFeed(XDocument.Parse(content));
        }
    }
}