using System;
using System.Web.Mvc;
using Tandil.MetadataBuilder;

namespace WebGeneric.Controllers
{
	public class GenericController<T> : Controller where T : class
	{
		readonly IDomainAccessor<T> _domainAccessor;
		readonly IDomainMutator<T> _domainMutator;
	
		public GenericController(IDomainAccessor<T> domainAccessor, IDomainMutator<T> domainMutator)
		{
			_domainAccessor = domainAccessor;
			_domainMutator = domainMutator;
		}
	
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult List()
		{
			var list = DomainAccessor.GetList();
			return PartialView(list);
		}

		public ActionResult Details(Guid id)
		{
			var instance = DomainAccessor.Get(id);
			return PartialView(instance);
		}

		public ActionResult Create()
		{
			var instance = CreateInstance();
			return PartialView("Edit", instance);
		}

		[HttpPost]
		public ActionResult Create(FormCollection collection)
		{
			var instance = CreateInstance();
			try
			{
				if (!TryUpdateModel(instance))
					return PartialView("Edit", instance);

				DomainMutator.Add(instance);
				
				return PartialView("Confirm", instance);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return PartialView("Edit", instance);
			}
		}

		public ActionResult Edit(Guid id)
		{
			var instance = DomainAccessor.Get(id);
			return PartialView("Edit", instance);
		}

		[HttpPost]
		public ActionResult Edit(Guid id, FormCollection collection)
		{
			var instance = DomainAccessor.Get(id);
			try
			{
				if (!TryUpdateModel(instance))
					return PartialView("Edit", instance);
				
				DomainMutator.Add(instance);
				
				return PartialView("Confirm", instance);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return PartialView("Edit", instance);
			}
		}

		public ActionResult Delete(Guid id)
		{
			var instance = DomainAccessor.Get(id);
			return PartialView(instance);
		}

		[HttpPost]
		public ActionResult Delete(Guid id, FormCollection collection)
		{
			var instance = DomainAccessor.Get(id);
			try
			{
				DomainMutator.Delete(instance);
				return PartialView("Confirm", instance);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return PartialView(instance);
			}
		}

		protected IDomainAccessor<T> DomainAccessor
		{
			get { return _domainAccessor; }
		}

		protected IDomainMutator<T> DomainMutator
		{
			get { return _domainMutator; }
		}

		protected T CreateInstance()
		{
			return (T)Activator.CreateInstance(ModelType, false);
		}

		protected Type ModelType
		{
			get { return typeof(T); }
		}
	}
}