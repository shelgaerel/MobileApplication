using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace CoreDataManager
{
	public class Data
	{
		[IgnoreAttribute]
		public DataState State {get;set;}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public Data()
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

		#region Fabrizio-Android

		private List<DataBinding> mDataBindingList;
		public List<DataBinding> DataBindingList 
		{
			get { return mDataBindingList; }
			set { mDataBindingList = value; }
		}

		#endregion
	}
}

