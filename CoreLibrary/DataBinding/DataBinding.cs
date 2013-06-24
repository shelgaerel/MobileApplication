using System;

namespace CoreLibrary
{
	public class DataBinding
	{
		public DataBinding ()
		{
		}

		public DataBinding (string fieldName, string propName)
		{
			FieldName = fieldName;
			PropName = propName;
		}

		public DataBinding (string fieldName, string propName, int iOSId)
		{
			PropName = propName;
			FieldName = fieldName;
			IOSId = iOSId;
		}

		public string PropName { get; set; }
		public string FieldName { get; set; }
		public int IOSId { get; set; } 
	}
}

