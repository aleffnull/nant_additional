using System.IO;
using System.Linq;
using System.Xml.Linq;
using Moq;
using NAnt.Additional.Tasks.Logging;
using NAnt.Additional.Tasks.Properties;
using NAnt.Additional.Tests.Tasks.Executors;
using NAnt.Core;
using NUnit.Framework;

namespace NAnt.Additional.Tests.Tasks
{
	[TestFixture]
	public class XmlDropNodeTaskFixture
	{
		#region Tests

		[Test]
		public void CanDropOneNode()
		{
			var document = new XDocument(new XElement("document", new XElement("element1"), new XElement("element2")));
			var file = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			document.Save(file);
			try
			{
				var mock = new Mock<ILogger>();
				var task = new XmlDropNodeTaskExecutor
				           {
				           	File = new FileInfo(file),
				           	XPath = "/document/element1",
				           	Logger = mock.Object
				           };
				task.CallExecuteTask();

				document = XDocument.Load(file);
				Assert.That(document.Root, Is.Not.Null);
				// ReSharper disable PossibleNullReferenceException
				Assert.That(document.Root.Descendants().Count(), Is.EqualTo(1));
				Assert.That(document.Root.Descendants().First().Name.ToString(), Is.EqualTo("element2"));
				// ReSharper restore PossibleNullReferenceException

				var desiredLogMessage = string.Format(Resources.XmlDropNodeTaskLogMessage, task.XPath, file);
				mock.Verify(logger => logger.Log(It.Is<Level>(l => l == Level.Info), It.Is<string>(s => s.Equals(desiredLogMessage))));
			}
			finally
			{
				File.Delete(file);
			}
		}

		[Test]
		public void CanDropSeveralNodes()
		{
			var document = new XDocument(
				new XElement("document", new XElement("element1"), new XElement("element1"), new XElement("element2")));
			var file = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			document.Save(file);
			try
			{
				var mock = new Mock<ILogger>();
				var task = new XmlDropNodeTaskExecutor
				           {
				           	File = new FileInfo(file),
				           	XPath = "/document/element1",
				           	Logger = mock.Object
				           };
				task.CallExecuteTask();

				document = XDocument.Load(file);
				Assert.That(document.Root, Is.Not.Null);
				// ReSharper disable PossibleNullReferenceException
				Assert.That(document.Root.Descendants().Count(), Is.EqualTo(1));
				Assert.That(document.Root.Descendants().First().Name.ToString(), Is.EqualTo("element2"));
				// ReSharper restore PossibleNullReferenceException

				var desiredLogMessage = string.Format(Resources.XmlDropNodeTaskLogMessage, task.XPath, file);
				mock.Verify(logger => logger.Log(It.Is<Level>(l => l == Level.Info), It.Is<string>(s => s.Equals(desiredLogMessage))));
			}
			finally
			{
				File.Delete(file);
			}
		}

		[Test]
		public void CanRunIfNothingToDrop()
		{
			var document = new XDocument(new XElement("document", new XElement("element1")));
			var file = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			document.Save(file);
			try
			{
				var mock = new Mock<ILogger>();
				var task = new XmlDropNodeTaskExecutor
				           {
				           	File = new FileInfo(file),
				           	XPath = "/document/element2",
				           	Logger = mock.Object
				           };
				task.CallExecuteTask();

				document = XDocument.Load(file);
				Assert.That(document.Root, Is.Not.Null);
				// ReSharper disable PossibleNullReferenceException
				Assert.That(document.Root.Descendants().Count(), Is.EqualTo(1));
				Assert.That(document.Root.Descendants().First().Name.ToString(), Is.EqualTo("element1"));
				// ReSharper restore PossibleNullReferenceException

				var desiredLogMessage = string.Format(Resources.XmlDropNodeTaskLogMessage, task.XPath, file);
				mock.Verify(logger => logger.Log(It.Is<Level>(l => l == Level.Info), It.Is<string>(s => s.Equals(desiredLogMessage))));
			}
			finally
			{
				File.Delete(file);
			}
		}

		#endregion Tests
	}
}