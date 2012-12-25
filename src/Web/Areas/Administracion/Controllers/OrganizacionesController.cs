using System;
using System.Web.Mvc;

using Centros.Model;
using Centros.Model.Queries;
using Centros.Model.Repositories;

namespace Centros.Web.Areas.Administracion.Controllers
{
    public class OrganizacionesController : Controller
    {
        readonly IQueryOrganizacionesLista _query;
        readonly IRepository<Organizacion> _repository;

        public OrganizacionesController(IQueryOrganizacionesLista query, IRepository<Organizacion> repository)
        {
            _query = query;
            _repository = repository;
        }

        public ActionResult Index()
        {
            var list = _query.GetList();

            return View(list);
        }

        public ActionResult Details(Guid id)
        {
            var instance = _query.Get(id);

            return View(instance);
        }

        public ActionResult Create()
        {
            var instance = new Organizacion();

            return View(instance);
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var instance = new Organizacion();
            TryUpdateModel(instance);

            if (!ModelState.IsValid)
                return View(instance);

            _repository.Add(instance);

            return RedirectToAction("Index");
        }
        
        public ActionResult Edit(Guid id)
        {
            var instance = _query.Get(id);

            return View(instance);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            var instance = _query.Get(id);
            TryUpdateModel(instance);

            if (!ModelState.IsValid)
                return View(instance);

            // TODO: abortar la transaccion de NH
            _repository.Add(instance);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            var instance = _query.Get(id);

            return View(instance);
        }

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            var instance = _query.Get(id);

            try
            {
                _repository.Delete(instance);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(instance);
            }
        }
    }
}