using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;

namespace DotNetFrameworkXxeTests.Tests
{
    class XmlTextReaderTest
    {
        [Test]
        public void IsSafeByDefault()
        {
            XmlTextReader target = new XmlTextReader(Util.ResourceFolderPath, new MemoryStream(Encoding.ASCII.GetBytes(Util.XmlWithPayload)));
            string stringifiedTarget = ToString(target);
            Assert.That(stringifiedTarget, Does.Not.Contains(Util.Payload));
        }

        [Test]
        public void IsSafeWithDtdProcessingProhibited()
        {
            try
            {
                XmlTextReader target = new XmlTextReader(Util.ResourceFolderPath, new MemoryStream(Encoding.ASCII.GetBytes(Util.XmlWithPayload)));
                target.DtdProcessing = DtdProcessing.Prohibit;
                string stringifiedTarget = ToString(target);
                Assert.That(stringifiedTarget, Does.Not.Contains(Util.Payload));
            }
            catch (XmlException)
            {
                // Got Exception, so OK.
            }
        }

        [Test]
        public void IsSafeWithUrlResolver()
        {
            XmlTextReader target = new XmlTextReader(Util.ResourceFolderPath, new MemoryStream(Encoding.ASCII.GetBytes(Util.XmlWithPayload)));
            target.XmlResolver = new XmlUrlResolver();
            string stringifiedTarget = ToString(target);
            Assert.That(stringifiedTarget, Does.Not.Contains(Util.Payload));
        }

        private string ToString(XmlTextReader target)
        {
            StringBuilder sb = new StringBuilder();
            while (target.Read())
            {
                if (target.NodeType == XmlNodeType.Element)
                {
                    sb.Append(target.ReadElementContentAsString());
                }
            }

            return sb.ToString();
        }
    }
}
