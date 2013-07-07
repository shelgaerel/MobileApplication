using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreLibrary
{
	public abstract class AbstractDataManager
	{
		private Data mDataEntity;
		public Data DataEntity
		{
			get
			{
				return this.mDataEntity;
			}
			set
			{
				this.mDataEntity = value;
			}
		}

		private SQLiteConnection mDataBase;
		protected SQLiteConnection DataBase
		{
			get
			{
				if (mDataBase == null) 
				{
					this.mDataBase = new SQLiteConnection (MobileOptions.GetDataBaseFilePath());
				}
				return mDataBase;

			}
			set
			{
				mDataBase = value;
			}
		}

		public virtual void UpdateEntity()
		{
			//Inserimento della logica di scrittura
			switch (this.mDataEntity.State) {
				case DataState.Added:
				DataBase.Insert (this.mDataEntity);
				break;
				case DataState.Modified:
				DataBase.Update (this.mDataEntity);
				break;
				case DataState.Deleted:
				DataBase.Delete (this.mDataEntity);
				break;
				default:
				break;
			}
		}

		public abstract Data Read ();

		public virtual void Read<T> (object primaryKey)
			where T : Data, new()
		{
			this.DataEntity = this.DataBase.Get<T> (primaryKey);
			this.DataEntity.State = DataState.Modified;
		}

		public virtual bool CheckEntity()
		{
			//return this.mEntity.OnIsValid();
			return true;
		}

		protected virtual void CreateTableIfNotExist<T>()
		{
			using (var conn= new SQLiteConnection(MobileOptions.GetDataBaseFilePath()))
			{
				conn.CreateTable<T>();
			}
		}

		public virtual List<T> GetEntityList<T> () where T : Data, new()
		{
			CreateTableIfNotExist<T> ();
			return (from i in  this.DataBase.Table<T> () select i).ToList();
		}
	}
}

