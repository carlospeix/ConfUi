using System.Web.Mvc;

namespace Tandil.MetadataBuilder.Attributes
{
	public class ReferenceRequiredAdapter : DataAnnotationsModelValidator<ReferenceRequiredAttribute>
	{
		public ReferenceRequiredAdapter(ModelMetadata metadata, ControllerContext context, ReferenceRequiredAttribute attribute) : base(metadata, context, attribute)
		{
		}
	}
}