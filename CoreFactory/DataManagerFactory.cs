using System;
using CoreLibrary;
using System.Reflection;
using System.Collections.Generic;
using System.IO;

namespace CoreFactory
{
	public static class DataEntityManagerFactory
	{
		public static AbstractDataManager CreateEngine(string dataEntityManagerQualifiedName, 
		                                                     string dataEntityQualifiedName)
		{
			AbstractDataManager dataEntityManager = (AbstractDataManager)CreateObject(dataEntityManagerQualifiedName);
			Data dataEntity = (Data)CreateObject(dataEntityQualifiedName);

			dataEntity.DataBindingList = GetDataBindingList (dataEntityQualifiedName);
			dataEntityManager.DataEntity = dataEntity;

			return (AbstractDataManager)dataEntityManager;
		}

		public static object CreateObject(string assemblyQualifiedName)
		{
			Type providerType = Type.GetType(assemblyQualifiedName, true);

			if (providerType == null)
				throw new Exception(string.Format("Creazione {0} fallita", assemblyQualifiedName));  

			return Activator.CreateInstance(providerType);
		}

		private static List<DataBinding> GetDataBindingList(string entityName)
		{
			Type entityType = Type.GetType(entityName, true);

			List<DataBinding> fieldList = new List<DataBinding>();
			PropertyInfo[] entityProperty = entityType.GetProperties(BindingFlags.Public 
			                                                         | BindingFlags.Instance 
			                                                         | BindingFlags.SetProperty);

			foreach (PropertyInfo property in entityProperty) 
			{
				DataBinding bindingItem = new DataBinding ();
				bindingItem.PropName = property.Name;

				IEnumerable<FieldName> fieldNames = (IEnumerable<FieldName>)property.GetCustomAttributes(typeof(FieldName), true);
				foreach (var field in fieldNames) 
					bindingItem.FieldName = field.Name;

				fieldList.Add(bindingItem);
			}

			return fieldList;
		}

		private static Assembly LoadAssembly(string engineQualifiedName)
		{
			Type engineType = Type.GetType(engineQualifiedName, true);

			string fileName = GetPluginAssemblyNameFrom(engineType);
			Assembly assembly = Assembly.LoadFrom(fileName);
			return assembly;
		}

		private static string GetPluginAssemblyNameFrom(Type toLoad)
		{
			string fileName = string.Format("{0}{1}", toLoad.Namespace, ".dll");
			var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			return Path.Combine(location, fileName);
		}
	}
}

