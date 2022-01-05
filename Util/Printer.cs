using static System.Console;

namespace CoreEscuela.Util
{
    public static class Printer
    {
        public static void DrawLine(int tam = 10)
        {
            WriteLine("".PadLeft(tam, '='));
        }

        public static void WriteTitle(string titulo)
        {
            var tamaño =titulo.Length + 4;
            DrawLine(tamaño);
            WriteLine($"| {titulo} |");
            DrawLine(tamaño);
        }

        public static void BeepNow(int hz = 2000, int tiempo=500, int cantidad =1)
        {
            while (cantidad-- > 0)
            {
#pragma warning disable CA1416 // Validate platform compatibility
                Beep(hz, tiempo);
#pragma warning restore CA1416 // Validate platform compatibility
            }
        }
    }
}