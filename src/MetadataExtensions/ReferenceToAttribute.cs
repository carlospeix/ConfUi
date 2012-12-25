using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Castle.Windsor;
using System.Collections;

namespace MetadataExtensions
{
    public class ReferenceToAttribute : MetadataAttribute
    {
        private Type _referenceType;
        private Type _providerType;

        public ReferenceToAttribute(Type referenceType)
        {
            _referenceType = referenceType;
            _providerType = typeof(IDomainReferenceProvider<>).MakeGenericType(referenceType);
        }

        public override void Process(ModelMetadata modelMetaData, IWindsorContainer container)
        {
            modelMetaData.AdditionalValues.Add("selectList", BuildSelectList(modelMetaData, container));
        }

        private SelectList BuildSelectList(ModelMetadata modelMetaData, IWindsorContainer container)
        {
            var provider = container.Resolve(_providerType);

            // TODO: Esto es horrible
            IEnumerable list = _providerType.GetMethod("GetList").Invoke(provider, new object[] { }) as IEnumerable;

            IList<SelectListItem> selectList = new List<SelectListItem>();

            foreach (object o in list)
            {
                Guid id = (Guid)_referenceType.GetProperty("Id").GetValue(o, new object[] { });
                string text = (string)_referenceType.GetMethod("ToString").Invoke(o, new object[] { });
                selectList.Add(new SelectListItem{ Value = id.ToString(), Text = text });
            }

            Guid selectedId = Guid.Empty;
            if (modelMetaData.Model != null)
                selectedId = (Guid)_referenceType.GetProperty("Id").GetValue(modelMetaData.Model, new object[] { });

            return new SelectList(selectList, "Value", "Text", selectedId);
        }
    }
}
