/* 
 var personas = new List<Persona>();

var reader = new StreamReader($"{Environment.CurrentDirectory}/personas.csv");
while (!reader.EndOfStream)
{
var valor = reader.ReadLine();
var elementos = valor.Split(';');

var persona = new Persona
{
Nombre = elementos[0],
Apellido = elementos[1],
Edad = elementos[2] 
};

personas.Add(persona);
}

foreach (var item in personas.OrderByDescending(p => p.Edad).Take(10))
{
Console.WriteLine($"datos del fichero: Nombre {item.Nombre},Apellido {item.Apellido} ,Edad {item.Edad}");
var edadNumber = int.Parse(item.Edad);
Console.WriteLine($"Edad convertida {edadNumber}");
}


public class Persona
{
// [Index(0)]
// public int PersonaId { get; set; }
//[Index(1)]
public string Nombre { get; set; }
//[Index(2)]
public string Apellido { get; set; }
// [Index(4)]
public string Edad { get; set; }
} */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<Persona> personas = new List<Persona>();

        do
        {
            LeerArchivo(personas);
            MostrarResultados(personas);

            Console.WriteLine("¿Desea continuar los chequeos? (s/n)");
        } while (Console.ReadLine().ToLower() == "s");//pasar a minuscula la entrada 
    }

    static void LeerArchivo(List<Persona> personas)
    {
        string path = $"{Environment.CurrentDirectory}/personas.csv";
        
        try
        {
            using (var reader = new StreamReader(path))
            {
                string line = reader.ReadLine(); // Leer encabezados
                while (!reader.EndOfStream)
                {
                    var valores = reader.ReadLine().Split(';');
                    var persona = new Persona
                    {
                        Nombre = valores[0],
                        Apellido = valores[1],
                        Edad = int.Parse(valores[2])
                    };
                    personas.Add(persona);
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"El archivo {path} no se encontró.");// mostrar el error si no se encuentra el archivo
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error al leer el archivo: {ex.Message}");// mostrar el error si no se puede leer
        }
    }

    static void MostrarResultados(List<Persona> personas)
    {
        if (personas.Count == 0)  /// atajar si no hay datos en el csv
        {
            Console.WriteLine("No se han cargado datos.");
            return;
        }

        double edadPromedio = personas.Average(p => p.Edad);
        var personaMenor = personas.OrderBy(p => p.Edad).First();
        var personaMayor = personas.OrderByDescending(p => p.Edad).First();

        Console.WriteLine($"Edad promedio: {edadPromedio}");
        Console.WriteLine($"Persona menor: {personaMenor.Nombre} {personaMenor.Apellido}, Edad: {personaMenor.Edad}");
        Console.WriteLine($"Persona mayor: {personaMayor.Nombre} {personaMayor.Apellido}, Edad: {personaMayor.Edad}");
    }
}

public class Persona // clase persona 
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public int Edad { get; set; }
}
