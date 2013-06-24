using System;

namespace CoreLibrary
{
	public enum CardAction
	{
		None = 0,
		Read = 1,
		ReadPk = 2,
		Delete = 3,
	}

	public static class Utility
	{
		public static object DataContractDeserialize(string buffer, bool compress)
		{
			if (!string.IsNullOrEmpty(buffer))
			{
				int idx = buffer.IndexOf('@');
				string assemblyQualifiedName = buffer.Substring(0, idx);
				string objBuffer = buffer.Substring(idx + 1);
				Type objType = Type.GetType(assemblyQualifiedName, true);

				System.Runtime.Serialization.DataContractSerializer aa = new System.Runtime.Serialization.DataContractSerializer(objType);
				XmlReader reader = XmlReader.Create(new StringReader(objBuffer));
				return aa.ReadObject(reader);
			}
			else
				return null;
		}

		public static string DataContractSerialize(Object obj, bool compress)
		{
			if (obj != null)
			{
				Type objType = obj.GetType();
				System.Runtime.Serialization.DataContractSerializer aa = new System.Runtime.Serialization.DataContractSerializer(objType);
				StringBuilder sb = new StringBuilder();

				// Inserisce l'assembly qualified name nello stream del buffer come primo elemento separato dalla @
				sb.Append(objType.AssemblyQualifiedName);
				sb.Append('@');

				XmlWriter writer = XmlWriter.Create(sb);
				aa.WriteObject(writer, obj);
				writer.Close();

				return sb.ToString();
			}
			else
				return null;
		}
	}
}

