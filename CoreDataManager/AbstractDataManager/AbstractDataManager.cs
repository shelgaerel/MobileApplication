using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreDataManager
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

		public virtual void UpdateEntity()
		{
			//Inserimento della logica di scrittura
			switch (this.mDataEntity.State) {
				case DataState.Added:
				DataBaseFactory.GetDataBaseConnection().Insert (this.mDataEntity);
				break;
				case DataState.Modified:
				DataBaseFactory.GetDataBaseConnection().Update (this.mDataEntity);
				break;
				case DataState.Deleted:
				DataBaseFactory.GetDataBaseConnection().Delete (this.mDataEntity);
				break;
				default:
				break;
			}
		}

		public abstract Data Read ();

		public virtual void Read<T> (object primaryKey)
			where T : Data, new()
		{
			this.DataEntity = DataBaseFactory.GetDataBaseConnection().Get<T> (primaryKey);
			this.DataEntity.State = DataState.Modified;
		}

		public virtual bool CheckEntity()
		{
			//return this.mEntity.OnIsValid();
			return true;
		}

		protected virtual void CreateTableIfNotExist<T>()
		{
			using (var conn= DataBaseFactory.GetDataBaseConnection())
			{
				conn.CreateTable<T>();
			}
		}

		public virtual List<T> GetEntityList<T> () where T : Data, new()
		{
			CreateTableIfNotExist<T> ();
			return (from i in  DataBaseFactory.GetDataBaseConnection().Table<T> () select i).ToList();
		}
	}
}

