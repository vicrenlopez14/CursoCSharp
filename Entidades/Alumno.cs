namespace CoreEscuela.Entidades
{
    public class Alumno: ObjetoEscuelaBase
    {
        public List<Evaluaci�n> Evaluaciones { get; set; } = new List<Evaluaci�n>();
    }
}