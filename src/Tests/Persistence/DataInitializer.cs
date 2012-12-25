using System;
using NHibernate;
using Centros.Model;
using Centros.Model.Repositories;

namespace Centros.Tests.Persistence
{
    public class DataInitializer
    {
        readonly ISessionFactory _sf;

        readonly IRepository<Jurisdiccion> _jurisdiccionesRepository;
        readonly IRepository<Institucion> _institucionesRepository;
        readonly IRepository<Educador> _educadoresRepository;
        readonly IRepository<Centro> _centrosRepository;
        readonly IRepository<Taller> _talleresRepository;

        public DataInitializer(
            ISessionFactory sessionFactory,
            IRepository<Jurisdiccion> jurisdiccionesRepository,
            IRepository<Institucion> institucionesRepository,
            IRepository<Educador> educadoresRepository,
            IRepository<Centro> centrosRepository,
            IRepository<Taller> talleresRepository)
        {
            _sf = sessionFactory;
            _jurisdiccionesRepository = jurisdiccionesRepository;
            _institucionesRepository = institucionesRepository;
            _educadoresRepository = educadoresRepository;
            _centrosRepository = centrosRepository;
            _talleresRepository = talleresRepository;
        }

        public void Initialize()
        {
            var buenosAires = new Jurisdiccion { Nombre = "Buenos Aires" };
            var normal20 = new Institucion { Nombre = "Normal 20", Jurisdiccion = buenosAires };
            var carlos = new Educador { Nombre = "Carlos", Apellido = "Peix" };
            var evitaPueblo1 = new Centro { Nombre = "Evita Pueblo I", EducadorACargo = carlos, Institucion = normal20, Horario = new Horario() };
            var evitaPueblo2 = new Centro { Nombre = "Evita Pueblo II", EducadorACargo = carlos, Institucion = normal20, Horario = new Horario() };
            var evitaPueblo3 = new Centro { Nombre = "Evita Pueblo II", EducadorACargo = carlos, Institucion = normal20, Horario = new Horario() };
            var cocinaBasica = new Taller { Nombre = "Cocina basica", Centro = evitaPueblo1, Inicio = DateTime.Today, Fin = DateTime.Today };

            evitaPueblo1.Horario.Dia = DayOfWeek.Monday;
            evitaPueblo2.Horario.Dia = DayOfWeek.Wednesday;
            evitaPueblo3.Horario.Dia = DayOfWeek.Wednesday;

            using (new TestUnitOfWork(_sf))
            {
                _jurisdiccionesRepository.Add(buenosAires);
                _institucionesRepository.Add(normal20);
                _educadoresRepository.Add(carlos);
                _centrosRepository.Add(evitaPueblo1);
                _centrosRepository.Add(evitaPueblo2);
                _centrosRepository.Add(evitaPueblo3);
                _talleresRepository.Add(cocinaBasica);
            }
        }
    }
}
