using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace NAnt.Additional.Tasks
{
	public class XmlAddNodeTask : Task
	{
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

		#endregion Properties

		#region Task overrides

		protected override void ExecuteTask()
		{
			var document = XDocument.Load(File.FullName);
			var parentNode = document.XPathSelectElement(XPath);
			var children = XElement.Parse(NodeXml);
			parentNode.Add(children);
			document.Save(File.FullName);
		}

		#endregion Task overrides
	}
}