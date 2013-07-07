using System;
using CoreLibrary;

namespace CoreDatabase
{
	public static class DataBaseFactory
	{
		static AbstractDataBase mDataBaseConnection;

		public static AbstractDataBase GetDataBaseConnection()
		{
			if (mDataBaseConnection == null)
				mDataBaseConnection = new SQLiteConnection (MobileOptions.GetDataBaseFilePath());
			else
				return mDataBaseConnection;
			return mDataBaseConnection;
		}
	}
}

