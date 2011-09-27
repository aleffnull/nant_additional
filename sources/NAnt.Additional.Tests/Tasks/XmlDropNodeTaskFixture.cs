using System.IO;
using System.Linq;
using System.Xml.Linq;
using NAnt.Additional.Tests.Tasks.Executors;
using NUnit.Framework;

namespace NAnt.Additional.Tests.Tasks
{
	[TestFixture]
	public class XmlDropNodeTaskFixture
	{
		#region Tests

		[Test]
		public void CanDropOneNodeByNodePath()
		{
			var document = new XDocument(new XElement("document", new XElement("element1"), new XElement("element2")));
			var file = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			document.Save(file);
			try
			{
				var task = new XmlDropNodeTaskExecutor { File = new FileInfo(file), XPath = "/document/element1" };
				task.CallExecuteTask();

				document = XDocument.Load(file);
				Assert.That(document.Root, Is.Not.Null);
				// ReSharper disable PossibleNullReferenceException
				Assert.That(document.Root.Descendants().Count(), Is.EqualTo(1));
				Assert.That(document.Root.Descendants().First().Name, Is.EqualTo("element2"));
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