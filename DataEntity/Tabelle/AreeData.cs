using System;
using CoreLibrary;

namespace DataEntity
{
	public class AreeData : Data
	{
		public AreeData ()
		{
			State = DataState.UnChanged;
		}

		public AreeData(DataState state)
		{
			State = state;
		}

		#region implemented abstract members of AbstractEntity

		public override bool OnIsValid ()
		{
			return true;
		}

		#endregion

		#region Interfaccia a Proprietà

		[EntityColumnName("CodArea")]
		[FieldName("CodArea")]
		public string CodArea { get; set; }

		[EntityColumnName("Descrizione")]
		[FieldName("CodArea")]
		public string Descrizione { get; set; }

		#endregion
	}
}

