namespace CoreEscuela.Entidades
{
    public class ObjetoEscuelaBase
    {
        public string UniqueId { get; set; };

        public string Name { get; set; }

        public ObjetoEscuelaBase()
        {
            UniqueId = new Guid().ToString();
        }

    }
}
