namespace CoreEscuela.Entidades
{
    public class Alumno: ObjetoEscuelaBase
    {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }

        public List<Evaluaci�n> Evaluaciones { get; set; } = new List<Evaluaci�n>();

        public Alumno() => UniqueId = Guid.NewGuid().ToString();
    }
}