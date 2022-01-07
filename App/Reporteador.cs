using CoreEscuela.Entidades;
using CoreEscuela.Util;
using System.Linq;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlavesDiccionari, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlavesDiccionari, IEnumerable<ObjetoEscuelaBase>> dicObsEsc)
        {
            if (dicObsEsc == null)
            {
                throw new ArgumentNullException(nameof(dicObsEsc));
            }
            _diccionario = dicObsEsc;

        }

        public IEnumerable<Evaluación> GetListaEvaluaciones()
        {
            IEnumerable<Evaluación> rta;
            if (_diccionario.TryGetValue(LlavesDiccionari.Evaluación,
                 out IEnumerable<ObjetoEscuelaBase> lista))
            {
                return lista.Cast<Evaluación>();
            }
            {
                return new List<Evaluación>();
            }

        }

        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }

        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluación> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluaciones();

            return (from Evaluación ev in listaEvaluaciones
                    select ev.Asignatura.Nombre).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluación>> GetDicEvaluacionesPorAsignatura()
        {
            var dictaRta = new Dictionary<string, IEnumerable<Evaluación>>();

            var listaAsignaturas = GetListaAsignaturas(out var listaEval);

            foreach (var asig in listaAsignaturas)
            {

                var evalAsig = from eval in listaEval
                               where eval.Asignatura.Nombre == asig
                               select eval;

                dictaRta.Add(asig, evalAsig);
            }

            return dictaRta;
        }

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetPromedioAlumnosPorAsignatura()
        {
            var rta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
            var dicEvalXAsig = GetDicEvaluacionesPorAsignatura();

            foreach (var asigConEval in dicEvalXAsig)
            {
                var promsAlumn = from eval in asigConEval.Value
                                 group eval by new { eval.Alumno.UniqueId, eval.Alumno.Nombre, eval.Nota }
                            into grupoEvalsAlumno
                                 select new AlumnoPromedio
                                 {
                                     alumnoId = grupoEvalsAlumno.Key.UniqueId,
                                     alumnoNombre = grupoEvalsAlumno.Key.Nombre,
                                     promedio = grupoEvalsAlumno.Average(evaluacion => evaluacion.Nota)
                                 };


                rta.Add(asigConEval.Key, promsAlumn);
            }

            return rta;
        }

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetTopAlumnosPorAsignatura(int top = 5)
        {
            var mejores = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
            var dicPromAlPorAsig = GetPromedioAlumnosPorAsignatura();

            foreach (var asignatura in dicPromAlPorAsig)
            {
                Printer.WriteTitle(asignatura.Key);

                var selection = (from alu in asignatura.Value
                                 orderby alu.promedio descending
                                 select alu).Take(top);

                mejores.Add(asignatura.Key, selection);
            }

            return mejores;
        }
    }
}
