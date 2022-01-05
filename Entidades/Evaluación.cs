using System;

namespace CoreEscuela.Entidades
{
    public class Evaluaci�n : ObjetoEscuelaBase
    {
        public Alumno? Alumno { get; set; }
        public Asignatura? Asignatura { get; set; }

        public float Nota { get; set; }

        public override string ToString()
        {
            return $"{Nota}, {Alumno.Nombre}, {Asignatura}";
        }

    }
}