using System;
using System.Linq;
using System.Web.Mvc;

using Centros.Model;
using Centros.Model.Queries;
using Centros.Model.Repositories;

namespace Centros.Web.Areas.Administracion.Controllers
{
    public class InstitucionesController : Controller
    {
        readonly IQueryInstitucionesLista _query;
        readonly IRepository<Institucion> _repository;
        readonly IQueryJurisdiccionesLista _queryJurisdicciones;

        public InstitucionesController(IQueryInstitucionesLista query, IRepository<Institucion> repository, IQueryJurisdiccionesLista queryJurisdicciones)
        {
            _query = query;
            _repository = repository;
            _queryJurisdicciones = queryJurisdicciones;
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
            var instance = new Institucion();

            AddViewData(instance);

            return View(instance);
        } 

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            var instance = new Institucion();

            if (TryUpdateModel(instance))
            {
                _repository.Add(instance);
                return RedirectToAction("Index");
            }

            AddViewData(instance);
            return View(instance);
        }
        
        public ActionResult Edit(Guid id)
        {
            var instance = _query.Get(id);

            AddViewData(instance);

            return View(instance);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection form)
        {
            var instance = _query.Get(id);

            if (TryUpdateModel(instance))
                return RedirectToAction("Index");

            AddViewData(instance);
            return View(instance);
        }

        public ActionResult Delete(Guid id)
        {
            var instance = _query.Get(id);

            return View(instance);
        }

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection form)
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

        private void AddViewData(Institucion instance)
        {
            var idJurisdiccion = (instance.Jurisdiccion == null) ? Guid.Empty : instance.Jurisdiccion.Id;

            ViewData["Jurisdiccion"] = _queryJurisdicciones.GetList()
                .Select(j => new SelectListItem
                                 {
                                     Text = j.Nombre,
                                     Value = j.Id.ToString(),
                                     Selected = j.Id.Equals(idJurisdiccion)
                                 }).ToList();
        }
    }
}