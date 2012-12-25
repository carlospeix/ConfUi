using System;
using System.Linq;
using System.Web.Mvc;

using Centros.Model;
using Centros.Model.Queries;
using Centros.Model.Repositories;

namespace Centros.Web.Areas.Administracion.Controllers
{
    public class CentrosController : Controller
    {
        readonly IQueryCentrosLista _query;
        readonly IRepository<Centro> _repository;
        readonly IQueryEducadoresLista _queryEducadores;
        readonly IQueryInstitucionesLista _queryInstituciones;

        public CentrosController(IQueryCentrosLista query, IRepository<Centro> repository, IQueryEducadoresLista queryEducadores, IQueryInstitucionesLista queryInstituciones)
        {
            _query = query;
            _repository = repository;
            _queryEducadores = queryEducadores;
            _queryInstituciones = queryInstituciones;
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
            var instance = new Centro();

            AddViewData(instance);

            return View(instance);
        } 

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            var instance = new Centro();

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

        private void AddViewData(Centro instance)
        {
            var idEducador = (instance.EducadorACargo == null) ? Guid.NewGuid() : instance.EducadorACargo.Id;
            var idInstitucion = (instance.Institucion == null) ? Guid.NewGuid() : instance.Institucion.Id;

            ViewData["EducadorACargo"] = _queryEducadores.GetList()
                .Select(j => new SelectListItem
                                 {
                                     Text = j.NombreCompleto,
                                     Value = j.Id.ToString(),
                                     Selected = j.Id.Equals(idEducador)
                                 }).ToList();

            ViewData["Institucion"] = _queryInstituciones.GetList()
                .Select(j => new SelectListItem
                                 {
                                     Text = j.Nombre,
                                     Value = j.Id.ToString(),
                                     Selected = j.Id.Equals(idInstitucion)
                                 }).ToList();
        }
    }
}