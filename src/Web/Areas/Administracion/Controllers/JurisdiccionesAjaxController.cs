using System;
using System.Web.Mvc;

using Centros.Model;
using Centros.Model.Queries;
using Centros.Model.Repositories;

namespace Centros.Web.Areas.Administracion.Controllers
{
    public class JurisdiccionesAjaxController : Controller
    {
        readonly IQueryJurisdiccionesLista _query;
        readonly IRepository<Jurisdiccion> _repository;

		public JurisdiccionesAjaxController(IQueryJurisdiccionesLista query, IRepository<Jurisdiccion> repository)
        {
            _query = query;
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult List()
		{
			var list = _query.GetList();

			return PartialView(list);
		}

		public ActionResult Details(Guid id)
        {
            var instance = _query.Get(id);

			return PartialView(instance);
        }

        public ActionResult Create()
        {
            var instance = new Jurisdiccion();

            return PartialView(instance);
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var instance = new Jurisdiccion();
            TryUpdateModel(instance);

            if (!ModelState.IsValid)
				return PartialView(instance);

            _repository.Add(instance);

			return PartialView("Confirm", instance);
        }
        
        public ActionResult Edit(Guid id)
        {
            var instance = _query.Get(id);

			return PartialView(instance);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            var instance = _query.Get(id);
            TryUpdateModel(instance);

            if (!ModelState.IsValid)
                return PartialView(instance);

			return PartialView("Confirm", instance);
		}

        public ActionResult Delete(Guid id)
        {
            var instance = _query.Get(id);

            return PartialView(instance);
        }

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            var instance = _query.Get(id);

            try
            {
                _repository.Delete(instance);

				return PartialView("Confirm", instance);
            }
            catch
            {
                return PartialView(instance);
            }
        }
    }
}