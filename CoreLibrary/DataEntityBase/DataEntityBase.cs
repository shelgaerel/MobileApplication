using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace CoreLibrary
{
	public class DataEntityBase
	{
		[IgnoreAttribute]
		public DataEntityState State {get;set;}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public DataEntityBase()
		{

		}

		public virtual bool OnIsValid ()
		{
			throw new NotSupportedException ();
		}

		public virtual List<MemberInfo> GetPropertyColumnName()
		{
			Type myType = this.GetType();
			List<MemberInfo> props = myType.GetMembers().Where(prop => Attribute.IsDefined(prop, typeof(EntityColumnName))).ToList<MemberInfo>();
			return props;
		}

		public virtual T GetFieldValue<T>(string columnName)
		{
			PropertyInfo field = this.GetType().GetProperty (columnName);
			object val = field.GetValue (this, null);

			return (T)val;
		}
	}
}

