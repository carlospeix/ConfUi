using System;
using System.Collections.Generic;

namespace Centros.Model.Queries
{
    public interface IQuery<T> where T : class
    {
        T Get(Guid id);
        T GetUnique();
        IEnumerable<T> GetList();
        //IPagedQueryResult<T> GetPagedList(int pageSize, int pageNumber);
    }

    public interface IQueryCentrosLista : IQuery<Centro>
    {
    }

    public interface IQueryEducadoresLista : IQuery<Educador>
    {
    }

    public interface IQueryInstitucionesLista : IQuery<Institucion>
    {
    }

    public interface IQueryOrganizacionesLista : IQuery<Organizacion>
    {
    }

    public interface IQueryJurisdiccionesLista : IQuery<Jurisdiccion>
    {
    }
}
