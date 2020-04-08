using System.Text;
using System.Xml;
using NUnit.Framework;

namespace DotNetFrameworkXxeTests.Tests
{
    class XmlNodeReaderTest
    {
        [Test]
        public void IsSafeWhenWrappingUnsafeXmlDocument()
        {
            // This is unsafe, as demonstrated by XmlDocumentTest.
            var unsafeXmlDocument = new XmlDocument();
            unsafeXmlDocument.XmlResolver = new XmlUrlResolver();
            unsafeXmlDocument.LoadXml(Util.XmlWithPayload);

            // But despite that, this XmlNodeReader is safe.
            XmlNodeReader target = new XmlNodeReader(unsafeXmlDocument);
            string stringifiedTarget = ToString(target);
            Assert.That(stringifiedTarget, Does.Not.Contain(Util.Payload));
        }

        [Test]
        public void IsSafeWhenWrappedByUnsafeXmlReader()
        {
            // This is unsafe, as demonstrated by XmlDocumentTest.
            var unsafeXmlDocument = new XmlDocument();
            unsafeXmlDocument.XmlResolver = new XmlUrlResolver();
            unsafeXmlDocument.LoadXml(Util.XmlWithPayload);

            // This is also unsafe, as demonstrated by XmlReaderTest.
            XmlReaderSettings unsafeSettings = new XmlReaderSettings();
            unsafeSettings.DtdProcessing = DtdProcessing.Parse;
            unsafeSettings.XmlResolver = new XmlUrlResolver();

            // But despite that, this XmlNodeReader is safe.
            XmlNodeReader target = new XmlNodeReader(unsafeXmlDocument);
            XmlReader reader = XmlReader.Create(target, unsafeSettings);
            string stringifiedReader = ToString(reader);
            Assert.That(stringifiedReader, Does.Not.Contain(Util.Payload));
        }

        private string ToString(XmlReader target)
        {
            StringBuilder sb = new StringBuilder();
            while (target.Read())
            {
                sb.Append(target.Value);
            }

            return sb.ToString();
        }

        private string ToString(XmlNodeReader target)
        {
            StringBuilder sb = new StringBuilder();
            while (target.Read())
            {
                sb.Append(target.Value);
            }

            return sb.ToString();
        }
    }
}
