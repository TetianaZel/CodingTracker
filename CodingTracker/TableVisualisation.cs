
using ConsoleTableExt;

internal class TableVisualisation
{
    internal static void ShowTable<T>(List<T> list) where T : class
    {
        Console.WriteLine("\n\n");

        ConsoleTableBuilder
            .From(list)
            .WithTitle("Coding")
            .ExportAndWriteLine();
        Console.WriteLine("\n\n");
    }
}