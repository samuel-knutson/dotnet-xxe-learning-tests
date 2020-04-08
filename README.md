# .NET XXE Learning Tests
Quick tests to evaluate the safety of various .NET XML Parsers with respect to XXE injection

# Overview

The purpose of this application is to test various XML Parsers in .NET to determine which parsers/patterns are vulnerable to [XML External Entity (XXE) injection](https://owasp.org/www-community/vulnerabilities/XML_External_Entity_(XXE)_Processing). The tests are based on deanf1's [dotnet-security-unit-tests](https://github.com/deanf1/dotnet-security-unit-tests) application, which is referenced in the [OWASP XXE Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/XML_External_Entity_Prevention_Cheat_Sheet.html).

The OWASP Cheat Sheet and deanf1's project are a good reference for .NET framework versions, but I was not sure how these findings translate to .NET Core versions. This repository contains tests for both .NET Framework and .NET Core to avoid any uncertainty.

Note that I also translated the tests to NUnit for my own ease of use. I believe the tests are still valid because I was able to reproduce deanf1's results for .NET Framework.

# Running the tests

- Clone the repository
- Open the appropriate solution in Visual Studio (there are separate solutions for .NET Framework vs. .NET Core)
- Change the .NET Framework version or .NET Core version as desired in the project properties (or .csproj file)
- Build the tests in Visual Studio
- Run the NUnit tests with your test runner of choice (I used ReSharper)
- PASS indicates the scenario is safe in the current version, FAIL indicates the scenario is unsafe

# Results

I tested three versions: .NET Framework 4.5.1 and 4.5.2 (to reproduce deanf1's results) and .NET Core 2.1. You can test against different versions by following the instructions above.

| Scenario | Safe in .NET Framework 4.5.1? | Safe in .NET Framework 4.5.2? | Safe in .NET Core 2.1?
| --- | --- | --- | --- |
| LinqXmlTest: XDocumentLoad_IsSafeByDefault | Yes | Yes | Yes |
| LinqXmlTest: XDocumentLoad_IsSafeWithDtdProcessing | No | Yes | Yes |
| LinqXmlTest: XDocumentLoad_IsSafeWithDtdProcessingAndUrlResolver | No | No | No |
| LinqXmlTest: XDocumentParse_IsSafeByDefault | Yes | Yes | Yes |
| LinqXmlTest: XElementLoad_IsSafeByDefault | Yes | Yes | Yes |
| LinqXmlTest: XElementLoad_IsSafeWithDtdProcessing | No | Yes | Yes |
| LinqXmlTest: XElementLoad_IsSafeWithDtdProcessingAndUrlResolver | No | No | No |
| LinqXmlTest: XElementParse_IsSafeByDefault | Yes | Yes | Yes |
| XPathNavigatorTest: IsSafeByDefault | No | Yes | Yes |
| XPathNavigatorTest: IsSafeWithXmlReader | Yes | Yes | Yes |
| XmlDictionaryReaderTest: IsSafeByDefault | Yes | Yes | Yes |
| XmlDictionaryReaderTest: IsSafeWithDtdProcessing | No | No | No |
| XmlDictionaryReaderTest: IsSafeWithDtdProcessingAndUrlResolver | No | Yes | Yes |
| XmlDocumentTest: IsSafeByDefault | No | Yes | Yes |
| XmlDocumentTest: IsSafeWithNullXmlResolver | Yes | Yes | Yes |
| XmlDocumentTest: IsSafeWithUrlResolver | No | No | No |
| XmlNodeReaderTest: IsSafeWhenWrappedByUnsafeXmlReader | Yes | Yes | Yes |
| XmlNodeReaderTest: IsSafeWhenWrappingUnsafeXmlDocument | Yes | Yes | Yes |
| XmlReaderTest: IsSafeByDefault | Yes | Yes | Yes |
| XmlReaderTest: IsSafeWithDtdProcessingAndUrlResolver | No | No | No |
| XmlTextReaderTest: IsSafeByDefault | No | Yes | Yes |
| XmlTextReaderTest: IsSafeWithDtdProcessingProhibited | Yes | Yes | Yes |
| XmlTextReaderTest: IsSafeWithUrlResolver | No | No | No |
| XslCompiledTransformTest: IsSafeByDefault | Yes | Yes | Yes |
| XslCompiledTransformTest: IsSafeWithSafeReader | Yes | Yes | Yes |
| XslCompiledTransformTest: IsSafeWithUnsafeReader | No | No | No |