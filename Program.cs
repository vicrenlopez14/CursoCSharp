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

            Printer.WriteTitle("Captura de una Evaluación por Consola");
            var newEval = new Evaluación();
            string nombre, notaString;
            float nota;
            nombre = ObtenerEntradaDeConsola("Ingrese el nombre la evaluación", "El valor del nombre no puede ser vacío", "El nombre de la evaluacíón ha sido ingresado correctamente");
            notaString = ObtenerEntradaDeConsola("Ingrese la nota de la evaluación", "El valor de la nota no puede ser vacío", "La nota de la evaluacíón ha sido ingresada correctamente");
            nota = float.Parse(notaString);
        }

        private static string ObtenerEntradaDeConsola(string mensajeDeEntrada, string mensajeDeError, string mensajeDeExito)
        {
            string entrada;
            Printer.WriteTitle(mensajeDeEntrada);
            Printer.PresioneEnter();
            entrada = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(entrada))
            {
                Printer.WriteTitle("El valor no puede ser vacío");
                Printer.WriteTitle("Saliendo");
            }
            else
            {
                Printer.WriteTitle(mensajeDeExito);
            }

            return entrada;
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
