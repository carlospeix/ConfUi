using System;

namespace Centros.Model.Queries
{
    public interface IQueryCentrosPorDia : IQuery<Centro>
    {
        IQueryCentrosPorDia ConDia(DayOfWeek dayOfWeek);
    }
}