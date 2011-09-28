using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using NAnt.Additional.Tasks.Logging;
using NAnt.Additional.Tasks.Properties;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace NAnt.Additional.Tasks
{
	[TaskName("xmlAddNode")]
	public class XmlAddNodeTask : XmlTask
	{
		#region Constructors

		public XmlAddNodeTask()
		{
			Logger = new NAntLogger(this);
		}

		#endregion Constructors

		#region Properties

		[TaskAttribute("file", Required = true)]
		[StringValidator(AllowEmpty = false)]
		public FileInfo File { get; set; }

		[TaskAttribute("xpath", Required = true)]
		[StringValidator(AllowEmpty = false)]
		public string XPath { get; set; }

		[TaskAttribute("nodeXml", Required = true)]
		[StringValidator(AllowEmpty = false)]
		public string NodeXml { get; set; }

		public ILogger Logger { get; set; }

		#endregion Properties

		#region Task overrides

		protected override void ExecuteTask()
		{
			Logger.Log(Level.Info, string.Format(Resources.XmlAddNodeTaskLogMessage, NodeXml, XPath, File.FullName));

			var document = XDocument.Load(File.FullName);
			var parentNode = document.XPathSelectElement(XPath);
			var children = XElement.Parse(NodeXml);
			parentNode.Add(children);
			Save(document, File.FullName);
		}

		#endregion Task overrides
	}
}