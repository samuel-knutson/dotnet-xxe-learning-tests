using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;

namespace DotNetFrameworkXxeTests.Tests
{
    class XmlReaderTest
    {
        [Test]
        public void IsSafeByDefault()
        {
            try
            {
                XmlReader target = XmlReader.Create(new MemoryStream(Encoding.ASCII.GetBytes(Util.XmlWithPayload)), new XmlReaderSettings(), Util.ResourceFolderPath);
                string stringifiedTarget = ToString(target);
                Assert.That(stringifiedTarget, Does.Not.Contain(Util.Payload));
            }
            catch (XmlException)
            {
                // Got Exception, so OK.
            }
        }

        [Test]
        public void IsSafeWithDtdProcessingAndUrlResolver()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.XmlResolver = new XmlUrlResolver();

            XmlReader target = XmlReader.Create(new MemoryStream(Encoding.ASCII.GetBytes(Util.XmlWithPayload)), settings, Util.ResourceFolderPath);
            string stringifiedTarget = ToString(target);
            Assert.That(stringifiedTarget, Does.Not.Contain(Util.Payload));
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
    }
}
