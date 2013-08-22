using System.ComponentModel.DataAnnotations;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Centros.Model;
using Tandil.MetadataBuilder;
using Tandil.MetadataBuilder.PatternAppliers;

namespace WebGeneric.Config
{
	public class MetadataBuilderConfig
	{
		public static void Start(IWindsorContainer container)
		{
			var reg = ConfigurationHolder.
				GetRootRegistrar().
				WireToRuntime();
			
			ConfigMetadata(reg, container);
			ConfigValidators(reg);
			ConfigModelAccessors(container);
		}

		private static void ConfigMetadata(IModelRegistrar reg, IWindsorContainer container)
		{
			reg.ModelNamespacePattern("Centros.Model.{0}, Centros.Model");
			reg.RegisterPatterApplier(new IdPatternApplier { IdPropertyName = "Id" });
			reg.RegisterPatterApplier(new ReferencePatternApplier { ExcludeReferenceTypes = new[] { typeof(Horario) } });
			reg.DomainAccessorAccessor(modelType =>
			                           	{
			                           		var providerType = typeof (IDomainAccessor<>).MakeGenericType(modelType);
			                           		return (IDomainAccessor<object>) container.Resolve(providerType);
			                           	}).
				DomainMutatorAccessor(modelType =>
				                      	{
				                      		var providerType = typeof (IDomainMutator<>).MakeGenericType(modelType);
				                      		return (IDomainMutator<object>) container.Resolve(providerType);
				                      	});

			reg.ForType<Centro>();
			reg.ForType<Educador>();
			reg.ForType<Horario>();
			reg.ForType<Institucion>(md => md.Description("Institución"));
			reg.ForType<Jurisdiccion>(md => md.Description("Jurisdicción"));
			reg.ForType<Organizacion>(md => md.Description("Organización"));
			reg.ForType<Participacion>();
			reg.ForType<Participante>();
			reg.ForType<Registro>();
			reg.ForType<Taller>();
		}

		private static void ConfigValidators(IModelRegistrar reg)
		{
			reg.ForType<Centro>().
				ForProperty(m => m.Nombre, md =>
				                           	{
				                           		md.Required();
				                           		md.StringLength(50, 1);
				                           	}).
				ForProperty(m => m.Horario, md => md.Required()).
				ForProperty(m => m.EducadorACargo, md => md.Required()).
				ForProperty(m => m.Institucion, md => md.Required());
			reg.ForType<Educador>().
				ForProperty(m => m.Nombre, md =>
				                           	{
				                           		md.Required();
				                           		md.StringLength(50, 1);
				                           	}).
				ForProperty(m => m.Apellido, md =>
				                           	{
				                           		md.Required();
				                           		md.StringLength(50, 1);
				                           	}).
				ForProperty(m => m.Telefono, md => md.StringLength(50)).
				ForProperty(m => m.Direccion, md => md.StringLength(200)).
				ForProperty(m => m.EMail, md => md.DataType(DataType.EmailAddress)).
				ForProperty(m => m.NombreCompleto, md => md.ShowForEdit(false));
			reg.ForType<Horario>().
				ForProperty(m => m.Hora, md =>
				                         	{
				                         		md.Required();
				                         		md.StringLength(50, 1);
				                         	});
			reg.ForType<Institucion>().
				ForProperty(m => m.Nombre, md =>
				                           	{
												md.Required();
				                           		md.StringLength(50, 1);
				                           	}).
				ForProperty(m => m.Telefono, md => md.StringLength(50)).
				ForProperty(m => m.Direccion, md => md.StringLength(200)).
				ForProperty(m => m.EMail, md => md.DataType(DataType.EmailAddress)).
				ForProperty(m => m.Jurisdiccion, md => md.Required());
			reg.ForType<Jurisdiccion>().
				ForProperty(m => m.Nombre, md =>
				                           	{
				                           		md.Required();
												md.StringLength(50, 1);
				                           	});
			reg.ForType<Organizacion>().
				ForProperty(m => m.Nombre, md =>
				                           	{
												md.Required();
				                           		md.StringLength(50, 1);
				                           	}).
				ForProperty(m => m.Telefono, md => md.StringLength(50)).
				ForProperty(m => m.Direccion, md => md.StringLength(200)).
				ForProperty(m => m.EMail, md => md.DataType(DataType.EmailAddress));
			reg.ForType<Participacion>();
			reg.ForType<Participante>();
			reg.ForType<Registro>();
			reg.ForType<Taller>().
				ForProperty(m => m.Nombre, md =>
				                           	{
				                           		md.Required();
				                           		md.StringLength(50, 1);
				                           	}).
				ForProperty(m => m.Inicio, md =>
				                           	{
				                           		md.Required();
				                           		md.DisplayFormat(null, "{0:d}", false, true, true);
				                           	}).
				ForProperty(m => m.Fin, md =>
				                           	{
				                           		md.Required();
				                           		md.DisplayFormat(null, "{0:d}", false, true, true);
				                           	}).
				ForProperty(m => m.Centro, md => md.Required());
		}

		private static void ConfigModelAccessors(IWindsorContainer container)
		{
			container.Register(
				Component.
					For(typeof (IDomainAccessor<>)).
					ImplementedBy(typeof (DomainAccessor<>)).
					LifeStyle.Transient);

			container.Register(
				Component.
					For(typeof(IDomainMutator<>)).
					ImplementedBy(typeof(DomainMutator<>)).
					LifeStyle.Transient);
		}
	}
}