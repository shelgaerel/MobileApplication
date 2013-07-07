using System;

namespace CoreLibrary
{
	public class EntityBindingProperty : Attribute
	{
		public string PropertyName { get; set;}
		public EntityBindingProperty(string propertyName)
		{
			this.PropertyName = propertyName;
		}
	}

	#region Attributi Entity Fabrizio-Android

	[AttributeUsage(AttributeTargets.Property)]
	public class FieldName : Attribute
	{
		public string Name;
		public FieldName(string name)
		{
			this.Name = name;
		}
	}

	#endregion

	#region Attributi Entity

	public class EntityColumnName: Attribute
	{
		public string ColumnName { get; set;}
		public EntityColumnName(string columnName)
		{
			this.ColumnName = columnName;
		}
	}

	#endregion

	#region Attributi SQLite

	public class PrimaryKeyAttribute : Attribute
	{
	}

	public class AutoIncrementAttribute : Attribute
	{
	}

	public class IndexedAttribute : Attribute
	{
		public string Name { get; set; }
		public int Order { get; set; }
		public virtual bool Unique { get; set; }

		public IndexedAttribute()
		{
		}

		public IndexedAttribute(string name, int order)
		{
			Name = name;
			Order = order;
		}
	}

	public class IgnoreAttribute : Attribute
	{
	}

	public class UniqueAttribute : IndexedAttribute
	{
		public override bool Unique {
			get { return true; }
			set { /* throw?  */ }
		}
	}

	public class MaxLengthAttribute : Attribute
	{
		public int Value { get; private set; }

		public MaxLengthAttribute (int length)
		{
			Value = length;
		}
	}

	public class CollationAttribute: Attribute
	{
		public string Value { get; private set; }

		public CollationAttribute (string collation)
		{
			Value = collation;
		}
	}

	#endregion

}

