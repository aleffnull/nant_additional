using System.Xml;
using System.Xml.Linq;
using NAnt.Core;

namespace NAnt.Additional.Tasks
{
	public abstract class XmlTask : Task
	{
		#region Methods

		protected void Save(XDocument document, string filePath)
		{
			var settings = new XmlWriterSettings { Indent = true, IndentChars = "\t" };
			using (var writer = XmlWriter.Create(filePath, settings))
			{
				document.Save(writer);
			}
		}

		#endregion Methods
	}
}