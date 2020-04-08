using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;

namespace DotNetFrameworkXxeTests.Tests
{
    class LinqXmlTest
    {
        [Test]
        public void XDocumentLoad_IsSafeByDefault()
        {
            string xml = Util.XmlWithPayload;
            XDocument target = XDocument.Load(new MemoryStream(Encoding.ASCII.GetBytes(xml)));
            Assert.That(target.ToString(), Does.Not.Contain(Util.Payload));
        }

        [Test]
        public void XDocumentParse_IsSafeByDefault()
        {
            string xml = Util.XmlWithPayload;
            XDocument target = XDocument.Parse(xml);
            Assert.That(target.ToString(), Does.Not.Contain(Util.Payload));
        }

        [Test]
        public void XDocumentLoad_IsSafeWithDtdProcessing()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            string xml = Util.XmlWithPayload;
            XmlReader reader = XmlReader.Create(new MemoryStream(Encoding.ASCII.GetBytes(xml)), settings, Util.ResourceFolderPath);
            XDocument target = XDocument.Load(reader);
            Assert.That(target.ToString(), Does.Not.Contain(Util.Payload));
        }

        [Test]
        public void XDocumentLoad_IsSafeWithDtdProcessingAndUrlResolver()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.XmlResolver = new XmlUrlResolver();
            string xml = Util.XmlWithPayload;
            XmlReader reader = XmlReader.Create(new MemoryStream(Encoding.ASCII.GetBytes(xml)), settings, Util.ResourceFolderPath);
            XDocument target = XDocument.Load(reader);
            Assert.That(target.ToString(), Does.Not.Contain(Util.Payload));
        }

        [Test]
        public void XElementLoad_IsSafeByDefault()
        {
            string xml = Util.XmlWithPayload;
            XElement target = XElement.Load(new MemoryStream(Encoding.ASCII.GetBytes(xml)));
            Assert.That(target.ToString(), Does.Not.Contain(Util.Payload));
        }

        [Test]
        public void XElementParse_IsSafeByDefault()
        {
            string xml = Util.XmlWithPayload;
            XElement target = XElement.Parse(xml);
            Assert.That(target.ToString(), Does.Not.Contain(Util.Payload));
        }

        [Test]
        public void XElementLoad_IsSafeWithDtdProcessing()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            string xml = Util.XmlWithPayload;
            XmlReader reader = XmlReader.Create(new MemoryStream(Encoding.ASCII.GetBytes(xml)), settings, Util.ResourceFolderPath);
            XElement target = XElement.Load(reader);
            Assert.That(target.ToString(), Does.Not.Contain(Util.Payload));
        }

        [Test]
        public void XElementLoad_IsSafeWithDtdProcessingAndUrlResolver()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.XmlResolver = new XmlUrlResolver();
            string xml = Util.XmlWithPayload;
            XmlReader reader = XmlReader.Create(new MemoryStream(Encoding.ASCII.GetBytes(xml)), settings, Util.ResourceFolderPath);
            XElement target = XElement.Load(reader);
            Assert.That(target.ToString(), Does.Not.Contain(Util.Payload));
        }
    }
}
