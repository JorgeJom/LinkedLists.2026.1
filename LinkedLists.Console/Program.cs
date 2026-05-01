using DoubleList;

var list = new DoubleLinkedList<string>();

var option = string.Empty;
var value = string.Empty;

do
{
    option = Menu();

    switch (option)
    {
        case "1":
            Console.Write("Ingresa un valor: ");
            value = Console.ReadLine() ?? string.Empty;
            list.InsertOrdered(value);         // Inserción ordenada ascendente
            break;

        case "2":
            Console.WriteLine("Lista hacia adelante:");
            Console.WriteLine(list.ToString());
            break;

        case "3":
            Console.WriteLine("Lista hacia atrás:");
            Console.WriteLine(list.ToStringReverse());
            break;

        case "4":
            list.Sort();
            Console.WriteLine("Lista ordenada descendentemente:");
            Console.WriteLine(list.ToString());
            break;

        case "5":
            var modes = list.Mode();
            if (modes.Count == 0)
            {
                Console.WriteLine("La lista está vacía.");
            }
            else if (modes.Count == 1)
            {
                Console.WriteLine($"La moda es: {modes[0]}");
            }
            else
            {
                Console.WriteLine($"Las modas son: {string.Join(", ", modes)}");
            }
            break;

        case "6":
            var chart = list.Chart();
            if (string.IsNullOrEmpty(chart))
                Console.WriteLine("La lista está vacía.");
            else
                Console.WriteLine(chart);
            break;

        case "7":
            Console.Write("Ingresa un valor a buscar: ");
            value = Console.ReadLine() ?? string.Empty;
            if (list.Contains(value))
                Console.WriteLine($"El valor '{value}' SÍ existe en la lista.");
            else
                Console.WriteLine($"El valor '{value}' NO existe en la lista.");
            break;

        case "8":
            list.RemoveFirstMode();
            Console.WriteLine("Primera ocurrencia eliminada.");
            Console.WriteLine("Lista actual:");
            Console.WriteLine(list.ToString());
            break;

        case "9":
            list.RemoveAllModes();
            Console.WriteLine("Todas las ocurrencias eliminadas.");
            Console.WriteLine("Lista actual:");
            Console.WriteLine(list.ToString());
            break;

        case "0":
            Console.WriteLine("Saliendo...");
            break;

        default:
            Console.WriteLine("Opción inválida. Intenta de nuevo.");
            break;
    }

} while (option != "0");


string Menu()
{
    Console.WriteLine();
    Console.WriteLine("══════════════════════════════");
    Console.WriteLine("   LISTA DOBLEMENTE LIGADA    ");
    Console.WriteLine("══════════════════════════════");
    Console.WriteLine("1. Adicionar");
    Console.WriteLine("2. Mostrar hacia adelante");
    Console.WriteLine("3. Mostrar hacia atrás");
    Console.WriteLine("4. Ordenar descendentemente");
    Console.WriteLine("5. Mostrar la(s) moda(s)");
    Console.WriteLine("6. Mostrar gráfico");
    Console.WriteLine("7. Existe");
    Console.WriteLine("8. Eliminar una ocurrencia");
    Console.WriteLine("9. Eliminar todas las ocurrencias");
    Console.WriteLine("0. Salir");
    Console.WriteLine("══════════════════════════════");
    Console.Write("Ingresa tu opción: ");
    return Console.ReadLine() ?? string.Empty;
}
