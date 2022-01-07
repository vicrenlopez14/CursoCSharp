using CoreEscuela.Entidades;
using CoreEscuela.Util;

namespace CoreEscuela.App
{
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }


        public void Inicializar()
        {
            Escuela = new Escuela("Platzi Academy", 2012, TiposEscuela.Primaria,
            ciudad: "Bogotá", pais: "Colombia"
            );

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();

        }

        public void ImprimirDiccionario(Dictionary<LlavesDiccionari, IEnumerable<ObjetoEscuelaBase>> dic, bool imprEval = false)
        {
            foreach (var obj in dic)
            {
                Printer.WriteTitle(obj.Key.ToString());

                foreach (var val in obj.Value)
                {
                    switch (obj.Key)
                    {
                        case LlavesDiccionari.Evaluación:
                            if (imprEval)
                            {
                                Console.WriteLine(val);
                            }
                            break;
                        case LlavesDiccionari.Escuela:
                            Console.WriteLine($"Escuela: " + val);
                            break;
                        case LlavesDiccionari.Alumno:
                            Console.WriteLine($"Alumno: " + val.Nombre);
                            break;
                        case LlavesDiccionari.Curso:
                            var curtmp = val as Curso;
                            if (curtmp != null)
                            {
                                int count = curtmp.Alumnos.Count;
                                Console.WriteLine($"Curso: {val.Nombre} Cantidad Alumnos: {count}");
                            }
                            break;
                        default:
                            Console.WriteLine(val);
                            break;
                    }
                }
            }
        }

        public Dictionary<LlavesDiccionari, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos()
        {
            var diccionario = new Dictionary<LlavesDiccionari, IEnumerable<ObjetoEscuelaBase>>();

            diccionario.Add(LlavesDiccionari.Escuela, new[] { Escuela });
            diccionario.Add(LlavesDiccionari.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>());

            var listatmp = new List<Evaluación>();
            var listatmpas = new List<Asignatura>();
            var listatmpal = new List<Alumno>();

            foreach (var curso in Escuela.Cursos)
            {
                listatmpas.AddRange(curso.Asignaturas);
                listatmpal.AddRange(curso.Alumnos);

                foreach (var alumno in curso.Alumnos)
                {
                    listatmp.AddRange(alumno.Evaluaciones);
                }

            }
            diccionario.Add(LlavesDiccionari.Evaluación, listatmp.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlavesDiccionari.Alumno, listatmpal.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlavesDiccionari.Asignatura, listatmpas.Cast<ObjetoEscuelaBase>());



            return diccionario;
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
        out int conteoEvaluaciones,
        out int conteoCursos,
        out int conteoAsignaturas,
        bool traeEvaluaciones = true,
        bool traeAlumnos = true,
        bool traeAsignaturas = true,
        bool traeCursos = true
        )
        {

            return GetObjetosEscuela(
                    out conteoEvaluaciones,
                    out conteoCursos,
                    out conteoAsignaturas,
                    out int dummy,
                    traeEvaluaciones: traeEvaluaciones,
                    traeCursos: traeCursos,
                    traeAsignaturas: traeAsignaturas,
                    traeAlumnos: traeAlumnos
                );
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
        out int conteoEvaluaciones,
        out int conteoCursos,
        bool traeEvaluaciones = true,
        bool traeAlumnos = true,
        bool traeAsignaturas = true,
        bool traeCursos = true
        )
        {

            return GetObjetosEscuela(
                    out conteoEvaluaciones,
                    out conteoCursos,
                    out int dummy,
                    out dummy,
                    traeEvaluaciones: traeEvaluaciones,
                    traeCursos: traeCursos,
                    traeAsignaturas: traeAsignaturas,
                    traeAlumnos: traeAlumnos
                );
        }

        #region Métodos de generación
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
        out int conteoEvaluaciones,
        bool traeEvaluaciones = true,
        bool traeAlumnos = true,
        bool traeAsignaturas = true,
        bool traeCursos = true
        )
        {
            conteoEvaluaciones = 0;

            return GetObjetosEscuela(
                    out conteoEvaluaciones,
                    out int dummy,
                    out dummy,
                    out dummy,
                    traeEvaluaciones: traeEvaluaciones,
                    traeCursos: traeCursos,
                    traeAsignaturas: traeAsignaturas,
                    traeAlumnos: traeAlumnos
                );
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
    bool traeEvaluaciones = true,
    bool traeAlumnos = true,
    bool traeAsignaturas = true,
    bool traeCursos = true
    )
        {
            return GetObjetosEscuela(
                    out int dummy,
                    out dummy,
                    out dummy,
                    out dummy,
                    traeEvaluaciones = traeEvaluaciones,
                    traeCursos = traeCursos,
                    traeAsignaturas = traeAsignaturas,
                    traeAlumnos = traeAlumnos
                );
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoCursos,
            out int conteoAsignaturas,
            out int conteoAlumnos,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
            )
        {

            conteoEvaluaciones = conteoCursos = conteoAsignaturas = conteoAlumnos = 0;
            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);
            listaObj.AddRange(Escuela.Cursos);

            if (traeCursos)
            {

                conteoCursos = Escuela.Cursos.Count;
                foreach (var curso in Escuela.Cursos)
                {
                    conteoCursos += curso.Asignaturas.Count;
                    conteoAlumnos = curso.Alumnos.Count;
                    if (traeAsignaturas)
                    {
                        listaObj.AddRange(curso.Asignaturas);
                    }

                    if (traeAlumnos)
                    {
                        listaObj.AddRange(curso.Alumnos);
                    }

                    if (traeEvaluaciones)
                    {
                        foreach (var alumno in curso.Alumnos)
                        {
                            conteoEvaluaciones = alumno.Evaluaciones.Count;
                            listaObj.AddRange(alumno.Evaluaciones);
                        }
                    }

                }

            }

            return listaObj.AsReadOnly();
        }


        private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }

        #endregion

        #region
        private void CargarEvaluaciones()
        {
            var rnd = new Random();

            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {

                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluación
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                                Nota = (float)Math.Round((float)(5 * rnd.NextDouble()), 2),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }

        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>(){
                            new Asignatura{Nombre="Matemáticas"} ,
                            new Asignatura{Nombre="Educación Física"},
                            new Asignatura{Nombre="Castellano"},
                            new Asignatura{Nombre="Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>(){
                        new Curso(){ Nombre = "101", Jornada = TiposJornada.Mañana },
                        new Curso() {Nombre = "201", Jornada = TiposJornada.Mañana},
                        new Curso{Nombre = "301", Jornada = TiposJornada.Mañana},
                        new Curso(){ Nombre = "401", Jornada = TiposJornada.Tarde },
                        new Curso() {Nombre = "501", Jornada = TiposJornada.Tarde},
            };

            Random rnd = new Random();
            foreach (var c in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }
        }

        #endregion Métodos de carga Métodos de carga
    }
}