using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Podcasts.Dom.Test
{
    [TestClass]
    public class ParsingTests
    {
        [TestMethod]
        public void ParseUint()
        {
            Assert.AreEqual(2u, Parsing.TryParse<uint?>("2"));
        }

        [TestMethod]
        public void ParseUlong()
        {
            Assert.AreEqual(3ul, Parsing.TryParse<ulong?>("3"));
        }

        [TestMethod]
        public void ParseUri()
        {
            Assert.IsNotNull(Parsing.TryParse<Uri>(@"http://www.google.com"));
        }

        [TestMethod]
        public void ParseDate()
        {
            Assert.AreEqual(
                new DateTime(year: 2015, month: 6, day: 7, hour: 7, minute: 1, second: 0, kind: DateTimeKind.Utc),
                Parsing.TryParse<DateTime?>("Sun, 07 Jun 2015 00:01:00 PDT"));
        }

        [TestMethod]
        public void ParseSeconds()
        {
            Assert.AreEqual(
                new TimeSpan(hours: 0, minutes: 0, seconds: 4321),
                Parsing.TryParse<TimeSpan?>("4321"));
        }

        [TestMethod]
        public void ParseMinutesAndSeconds()
        {
            Assert.AreEqual(
                new TimeSpan(hours: 0, minutes: 43, seconds: 21),
                Parsing.TryParse<TimeSpan?>("43:21"));
        }

        [TestMethod]
        public void ParseHoursMinutesAndSeconds()
        {
            Assert.AreEqual(
                new TimeSpan(hours: 8, minutes: 43, seconds: 21),
                Parsing.TryParse<TimeSpan?>("8:43:21"));
        }

        [TestMethod]
        public void ParsingFail()
        {
            Assert.IsNull(Parsing.TryParse<uint?>("blue"));
        }
    }
}
