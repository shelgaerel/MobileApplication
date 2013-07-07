using System;
using CoreLibrary;
using DataEntity;

namespace DataEntityManager
{
	public class AreeDataManager : AbstractDataManager
	{
		public AreeDataManager ()
		{

		}

		#region implemented abstract members of EngineBase

		public override Data Read ()
		{
			//Inserimento della logica di lettura per un nuovo inserimento
			this.DataEntity = new AreeData(DataState.Added);
			return this.DataEntity;
		}

		#endregion
	}
}

