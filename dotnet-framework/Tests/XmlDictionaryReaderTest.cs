using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;

namespace DotNetFrameworkXxeTests.Tests
{
    class XmlDictionaryReaderTest
    {
        [Test]
        public void IsSafeByDefault()
        {
            try
            {
                XmlDictionaryReader target = XmlDictionaryReader.CreateTextReader(Encoding.ASCII.GetBytes(Util.XmlWithPayload), XmlDictionaryReaderQuotas.Max);
                string stringifiedTarget = ToString(target);
                Assert.That(stringifiedTarget, Does.Not.Contain(Util.Payload));
            }
            catch (XmlException)
            {
                // Got Exception, so OK.
            }
        }

        [Test]
        public void IsSafeWithDtdProcessing()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            XmlReader reader = XmlReader.Create(new MemoryStream(Encoding.ASCII.GetBytes(Util.XmlWithPayload)), settings, Util.ResourceFolderPath);
            XmlDictionaryReader target = XmlDictionaryReader.CreateDictionaryReader(reader);
            string stringifiedTarget = ToString(target);
            Assert.That(stringifiedTarget, Does.Not.Contain(Util.Payload));
        }

        [Test]
        public void IsSafeWithDtdProcessingAndUrlResolver()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.XmlResolver = new XmlUrlResolver();
            XmlReader reader = XmlReader.Create(new MemoryStream(Encoding.ASCII.GetBytes(Util.XmlWithPayload)), settings, Util.ResourceFolderPath);
            XmlDictionaryReader target = XmlDictionaryReader.CreateDictionaryReader(reader);
            string stringifiedTarget = ToString(target);
            Assert.That(stringifiedTarget, Does.Not.Contain(Util.Payload));
        }

        private string ToString(XmlDictionaryReader target)
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
