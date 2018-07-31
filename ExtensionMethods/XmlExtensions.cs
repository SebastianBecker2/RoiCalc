public static class XmlExtensions
{
	public static void AppendChild(this System.Xml.XmlNode node, string name, string value)
	{
		System.Xml.XmlElement child = node.OwnerDocument.CreateElement(name);
		child.AppendChild(node.OwnerDocument.CreateTextNode(value));
		node.AppendChild(child);
	}

	public static void AppendChild(this System.Xml.XmlNode node, string name, int value)
	{
		node.AppendChild(name, value.ToString());
	}

	public static void AppendChild(this System.Xml.XmlNode node, string name, uint value)
	{
		node.AppendChild(name, value.ToString());
	}

	public static void AppendChild(this System.Xml.XmlNode node, string name, bool value)
	{
		node.AppendChild(name, value.ToString());
	}

	public static void ToInt(this System.Xml.XmlNode root, string name, ref int value)
	{
		if (root[name] == null) return;
		value = root[name].InnerText.SoftParse(value);
	}

	public static void ToString(this System.Xml.XmlNode root, string name, ref string value)
	{
		if (root[name] == null) return;
		value = root[name].InnerText;
	}

	public static void ToBool(this System.Xml.XmlNode root, string name, ref bool value)
	{
		if (root[name] == null) return;
		value = root[name].InnerText.SoftParse(value);
	}
}