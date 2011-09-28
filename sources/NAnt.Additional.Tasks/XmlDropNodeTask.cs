using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using NAnt.Additional.Tasks.Logging;
using NAnt.Additional.Tasks.Properties;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace NAnt.Additional.Tasks
{
	[TaskName("xmlDropNode")]
	public class XmlDropNodeTask : Task
	{
		#region Constructors

		public XmlDropNodeTask()
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

		public ILogger Logger { get; set; }

		#endregion Properties

		#region Task overrides

		protected override void ExecuteTask()
		{
			Logger.Log(Level.Info, string.Format(Resources.XmlDropNodeTaskLogMessage, XPath, File.FullName));

			var document = XDocument.Load(File.FullName);
			var nodes = document.XPathSelectElements(XPath).ToList();
			nodes.ForEach(node => node.Remove());
			document.Save(File.FullName);
		}

		#endregion Task overrides
	}
}