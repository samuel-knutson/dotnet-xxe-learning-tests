using System.Xml;
using NUnit.Framework;

namespace DotNetFrameworkXxeTests.Tests
{
    class XmlDocumentTest
    {
        [Test]
        public void IsSafeByDefault()
        {
            var target = new XmlDocument();
            target.LoadXml(Util.XmlWithPayload);
            Assert.That(target.InnerText, Does.Not.Contain(Util.Payload));
        }

        [Test]
        public void IsSafeWithNullXmlResolver()
        {
            XmlDocument target = new XmlDocument();
            target.XmlResolver = null;
            target.LoadXml(Util.XmlWithPayload);
            Assert.That(target.InnerText, Does.Not.Contain(Util.Payload));
        }

        [Test]
        public void IsSafeWithUrlResolver()
        {
            XmlDocument target = new XmlDocument();
            target.XmlResolver = new XmlUrlResolver();
            target.LoadXml(Util.XmlWithPayload);
            Assert.That(target.InnerText, Does.Not.Contain(Util.Payload));
        }
    }
}
