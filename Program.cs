using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;

            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            var evalList = reporteador.GetListaEvaluaciones();
            var listaAsig = reporteador.GetListaAsignaturas();
            var listaEvalXAsig = reporteador.GetDicEvaluacionesPorAsignatura();
            var listaPromXAsig = reporteador.GetPromedioAlumnosPorAsignatura();
            var listaTopAlumXAsig = reporteador.GetTopAlumnosPorAsignatura();
        }

        private static void AccionDelEvento(object? sender, EventArgs e)
        {
            Printer.WriteTitle("SALIENDO");
            Printer.WriteTitle("SALIÓ");
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {

            Printer.WriteTitle("Cursos de la Escuela");


            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }
    }
}
