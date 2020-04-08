using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using NUnit.Framework;

namespace DotNetFrameworkXxeTests.Tests
{
    class XPathNavigatorTest
    {
        [Test]
        public void IsSafeByDefault()
        {
            XPathDocument doc = new XPathDocument(new MemoryStream(Encoding.ASCII.GetBytes(Util.XmlWithPayload)));
            XPathNavigator target = doc.CreateNavigator();
            Assert.That(target.InnerXml, Does.Not.Contain(Util.Payload));
        }

        [Test]
        public void IsSafeWithXmlReader()
        {
            try
            {
                XmlReader reader = XmlReader.Create(new MemoryStream(Encoding.ASCII.GetBytes(Util.XmlWithPayload)), new XmlReaderSettings(), Util.ResourceFolderPath);
                XPathDocument doc = new XPathDocument(reader);
                XPathNavigator target = doc.CreateNavigator();
                Assert.That(target.InnerXml, Does.Not.Contain(Util.Payload));
            }
            catch (XmlException)
            {
                // Got Exception, so OK.
            }
        }
    }
}
