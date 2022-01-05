using CoreEscuela.Util;
using System;
using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    public class Curso : ObjetoEscuelaBase, ILugar
    {
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas { get; set; } = new List<Asignatura>();
        public List<Alumno> Alumnos { get; set; } = new List<Alumno>();

        public string Dirección { get; set; }

        public void LimpiarLugar()
        {
            Printer.DrawLine();
            Console.WriteLine("Limpiando Establecimiento...");
            Console.WriteLine($"Curso {Nombre} está limpio");
        }

    }
}