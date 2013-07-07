using System;

namespace CoreLibrary
{
	public interface UIInterface
	{
		void LoadDataEntity(object pkValue);
		AbstractDataManager CreateDataEntityManager();
		void GetDataEntityFromControls();
	}
}

