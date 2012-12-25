using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using Castle.Windsor;

namespace MetadataExtensions
{
    public class CustomModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        readonly IWindsorContainer _container;

        public CustomModelMetadataProvider(IWindsorContainer container)
        {
            _container = container;
        }

        protected override ModelMetadata CreateMetadata(
                                 IEnumerable<Attribute> attributes,
                                 Type containerType,
                                 Func<object> modelAccessor,
                                 Type modelType,
                                 string propertyName)
        {
            var metaData = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            attributes.OfType<MetadataAttribute>().ToList().ForEach(x => x.Process(metaData, _container));

            return metaData;
        }
    }

}