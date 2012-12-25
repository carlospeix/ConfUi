using System;

namespace Tandil.MetadataBuilder
{
	public interface IPatternApplier
	{
		void ModelRegistered(Type modelType);
	}
}