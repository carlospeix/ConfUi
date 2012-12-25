using System;
using System.Collections.Generic;

namespace Centros.Model
{
    public class Horario
    {
        public DayOfWeek? Dia { get; set; }
        public string Hora { get; set; }

        public override string ToString()
        {
            if (!Dia.HasValue)
                return "(sin horario)";

            var spanish = new Dictionary<DayOfWeek, string>
                              {
                                  {DayOfWeek.Sunday, "Domingo"},
                                  {DayOfWeek.Monday, "Lunes"},
                                  {DayOfWeek.Tuesday, "Martes"},
                                  {DayOfWeek.Wednesday, "Miércoles"},
                                  {DayOfWeek.Thursday, "Jueves"},
                                  {DayOfWeek.Friday, "Viernes"},
                                  {DayOfWeek.Saturday, "Sábado"}
                              };

            return String.Format("{0} - {1}", spanish[Dia.Value], Hora);
        }
    }
}