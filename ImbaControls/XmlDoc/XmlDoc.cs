using System.IO;
using System.Xml;

namespace ImbaControls
{
	public class XmlDoc : System.Xml.XmlDocument
	{
		/// <summary>
		/// Uses FileStream to make sure the file gets closed afterwards
		/// </summary>
		/// <param name="filename">URL for the file containing the XML document to load. The URL can be either a local file or an HTTP URL (a Web address).</param>
		public override void Load(string filename)
		{
			using (FileStream file_stream = new FileStream(filename, FileMode.Open))
			{
				base.Load(file_stream);
				file_stream.Close();
			}
		}

		/// <summary>
		/// Automatically indents the xml data and uses FileStream to make sure the file gets closed afterwards
		/// </summary>
		/// <param name="filename">The location of the file where you want to save the document.</param>
		public override void Save(string filename)
		{
			using (FileStream file_stream = new FileStream(filename, FileMode.Create))
			{
				var settings = new XmlWriterSettings() { Indent = true };
				var writer = XmlWriter.Create(file_stream, settings);
				base.Save(writer);
				file_stream.Close();
			}
		}
	}
}