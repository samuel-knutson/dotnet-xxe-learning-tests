using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using NUnit.Framework;

namespace DotNetFrameworkXxeTests.Tests
{
    class XslCompiledTransformTest
    {
        private const string TemporaryFileName = "xml_with_payload.xml";

        [SetUp]
        public void CreateTemporaryXmlFile()
        {
            Util.SaveResourceFile(TemporaryFileName, Util.XmlWithPayload);
        }

        [TearDown]
        public void DeleteTemporaryXmlFile()
        {
            Util.DeleteResourceFile(TemporaryFileName);
        }

        [Test]
        public void IsSafeByDefault()
        {
            try
            {
                // Safe by default because XslCompiledTransform uses XmlReader by default.
                // Source: http://www.dotnetframework.org/default.aspx/4@0/4@0/DEVDIV_TFS/Dev10/Releases/RTMRel/ndp/fx/src/Xml/System/Xml/Xslt/XslCompiledTransform@cs/1305376/XslCompiledTransform@cs
                // The OWASP tests make this observation, but I do not see a test that actually shows this scenario.
                XslCompiledTransform target = new XslCompiledTransform();
                target.Load(Util.XslFilepath);
                StringWriter output = new StringWriter();
                target.Transform(Util.GetResourceFilePath(TemporaryFileName), new XsltArgumentList(), output);
                Assert.That(output.ToString(), Does.Not.Contain(Util.Payload));
            }
            catch (XmlException)
            {
                // Got Exception, so OK.
            }
        }

        [Test]
        public void IsSafeWithUnsafeReader()
        {
            // This XmlTextReader is unsafe, as demonstrated by XmlTextReaderTest.
            XmlTextReader unsafeReader = new XmlTextReader(Util.ResourceFolderPath, new MemoryStream(Encoding.ASCII.GetBytes(Util.XmlWithPayload)));
            unsafeReader.XmlResolver = new XmlUrlResolver();

            // As a result, the XslCompiledTransform is also unsafe.
            XslCompiledTransform target = new XslCompiledTransform();
            target.Load(Util.XslFilepath);
            StringWriter output = new StringWriter();
            target.Transform(unsafeReader, new XsltArgumentList(), output);
            Assert.That(output.ToString(), Does.Not.Contain(Util.Payload));
        }

        [Test]
        public void IsSafeWithSafeReader()
        {
            try
            {
                XmlReader safeReader = XmlReader.Create(new MemoryStream(Encoding.ASCII.GetBytes(Util.XmlWithPayload)), new XmlReaderSettings(), Util.ResourceFolderPath);
                XslCompiledTransform target = new XslCompiledTransform();
                target.Load(Util.XslFilepath);
                StringWriter output = new StringWriter();
                target.Transform(safeReader, new XsltArgumentList(), output);
                Assert.That(output.ToString(), Does.Not.Contain(Util.Payload));
            }
            catch (XmlException)
            {
                // Got Exception, so OK.
            }
        }
    }
}
