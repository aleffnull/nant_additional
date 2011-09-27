using System.IO;
using System.Linq;
using System.Xml.Linq;
using NAnt.Additional.Tests.Tasks.Executors;
using NUnit.Framework;

namespace NAnt.Additional.Tests.Tasks
{
	[TestFixture]
	public class XmlAddNodeTaskFixture
	{
		#region Tests

		[Test]
		public void CanAddNode()
		{
			var document = new XDocument(new XElement("document", new XElement("element1"), new XElement("element2")));
			var file = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			document.Save(file);
			try
			{
				var task = new XmlAddNodeTaskExecutor {File = new FileInfo(file), XPath = "/document", NodeXml = "<element3 />"};
				task.CallExecuteTask();

				document = XDocument.Load(file);
				Assert.That(document.Root, Is.Not.Null);
				// ReSharper disable PossibleNullReferenceException
				Assert.That(document.Root.Descendants().Count(), Is.EqualTo(3));
				Assert.That(document.Element("document").Element("element1"), Is.Not.Null);
				Assert.That(document.Element("document").Element("element2"), Is.Not.Null);
				Assert.That(document.Element("document").Element("element3"), Is.Not.Null);
				// ReSharper restore PossibleNullReferenceException
			}
			finally
			{
				File.Delete(file);
			}
		}

		[Test]
		public void CanAddDuplicateNode()
		{
			var document = new XDocument(new XElement("document", new XElement("element1"), new XElement("element2")));
			var file = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			document.Save(file);
			try
			{
				var task = new XmlAddNodeTaskExecutor { File = new FileInfo(file), XPath = "/document", NodeXml = "<element2 />" };
				task.CallExecuteTask();

				document = XDocument.Load(file);
				Assert.That(document.Root, Is.Not.Null);
				// ReSharper disable PossibleNullReferenceException
				Assert.That(document.Root.Descendants().Count(), Is.EqualTo(3));
				Assert.That(document.Element("document").Element("element1"), Is.Not.Null);
				Assert.That(document.Element("document").Elements("element2").Count(), Is.EqualTo(2));
				// ReSharper restore PossibleNullReferenceException
			}
			finally
			{
				File.Delete(file);
			}
		}

		#endregion Tests
	}
}