using System;

namespace CoreLibrary
{
	public abstract class AbstractDataEntityManager
	{
		private DataEntityBase mDataEntity;
		public DataEntityBase DataEntity
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
				case DataEntityState.Added:
				DataBase.Insert (this.mDataEntity);
				break;
				case DataEntityState.Modified:
				DataBase.Update (this.mDataEntity);
				break;
				case DataEntityState.Deleted:
				DataBase.Delete (this.mDataEntity);
				break;
				default:
				break;
			}
		}

		public abstract DataEntityBase Read ();

		public virtual void Read<T> (object primaryKey)
			where T : DataEntityBase, new()
		{
			this.DataEntity = this.DataBase.Get<T> (primaryKey);
			this.DataEntity.State = DataEntityState.Modified;
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

		public virtual List<T> GetEntityList<T> () where T : DataEntityBase, new()
		{
			CreateTableIfNotExist<T> ();
			return (from i in  this.DataBase.Table<T> () select i).ToList();
		}
	}
}

