using System;

namespace CoreEscuela.Entidades
{
    public class Evaluaci�n
    {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }

        public Alumno Alumno { get; set; }
        public Asignatura Asignatura  { get; set; }

        public float Nota { get; set; }

        public Evaluaci�n() => UniqueId = Guid.NewGuid().ToString();
    }
}