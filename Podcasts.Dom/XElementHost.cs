using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Podcasts.Dom
{
    public abstract class XElementHost
    {
        protected XElement Element { get; private set; }

        private XNamespace Namespace = XNamespace.None;

        protected XElementHost(XElement element, XNamespace ns)
        {
            Element = element;
            Namespace = ns;
        }

        protected XElementHost(XElement element) : this(element, XNamespace.None)
        {
        }

        private XName ns(string key)
        {
            return Namespace + key;
        }

        internal IEnumerable<XElement> SelectElements(string key)
        {
            return Element.Elements(ns(key));
        }

        internal XElement SelectSingleElement(string key)
        {
            return Element.Element(ns(key));
        }

        internal T LazyLoadContent<T>(ref SetOnce<T> backing, string key)
        {
            if(backing == null)
            {
                backing = SetOnce<T>.Create(Parsing.TryParse<T>((SelectSingleElement(key)?.FirstNode as XText)?.Value));
            }

            return backing.Extract();
        }

        internal T LazyLoadAttribute<T>(ref SetOnce<T> backing, string attr)
        {
            if(backing == null)
            {
                backing = SetOnce<T>.Create(Parsing.TryParse<T>(Element.Attributes(attr).FirstOrDefault()?.Value));
            }

            return backing.Extract();
        }
    }
}