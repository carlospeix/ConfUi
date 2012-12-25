using System.Web.Mvc;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using Castle.Windsor;

namespace WebGeneric.ValueProviders
{
    public class JurisdiccionValueProvider : IValueProvider
    {
        private Dictionary<string, ValueProviderResult> values;

        public JurisdiccionValueProvider()
        {
            values = new Dictionary<string, ValueProviderResult>();
            foreach (string key in ConfigurationManager.AppSettings.AllKeys)
            {
                string appSetting = ConfigurationManager.AppSettings[key];
                values.Add(key, new ValueProviderResult(appSetting, appSetting, CultureInfo.InvariantCulture));
            }
        }

        public bool ContainsPrefix(string prefix)
        {
            return values.ContainsKey(prefix);
        }

        public ValueProviderResult GetValue(string key)
        {
            ValueProviderResult value;
            values.TryGetValue(key, out value);
            return value;
        }
    }

    public class JurisdiccionValueProviderFactory : ValueProviderFactory
    {
        readonly IWindsorContainer _container;

        public JurisdiccionValueProviderFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            return new JurisdiccionValueProvider();
        }
    }
}