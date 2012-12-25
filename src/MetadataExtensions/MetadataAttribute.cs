using System;
using System.Web.Mvc;
using Castle.Windsor;

namespace MetadataExtensions
{
    public abstract class MetadataAttribute : Attribute
    {
        public abstract void Process(ModelMetadata modelMetaData, IWindsorContainer container);
    }
}
