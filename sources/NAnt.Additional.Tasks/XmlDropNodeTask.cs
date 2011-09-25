using System.IO;
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
			//
		}

		#endregion Task overrides
	}
}