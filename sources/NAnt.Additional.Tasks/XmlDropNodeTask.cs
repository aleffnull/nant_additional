using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace NAnt.Additional.Tasks
{
	public class XmlDropNodeTask : Task
	{
		#region Properties

		[TaskAttribute("file", Required = true)]
		[StringValidator(AllowEmpty = false)]
		public FileInfo File { get; set; }

		[TaskAttribute("xpath", Required = true)]
		[StringValidator(AllowEmpty = false)]
		public string XPath { get; set; }

		#endregion Properties

		#region Task overrides

		protected override void ExecuteTask()
		{
			var document = XDocument.Load(File.FullName);
			var nodes = document.XPathSelectElements(XPath).ToList();
			nodes.ForEach(node => node.Remove());
			document.Save(File.FullName);
		}

		#endregion Task overrides
	}
}