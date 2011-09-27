using NAnt.Core;
using NAnt.Core.Attributes;

namespace NAnt.Additional.Tasks
{
	public class XmlAddNodeTask : Task
	{
		#region Properties

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
			//
		}

		#endregion Task overrides
	}
}