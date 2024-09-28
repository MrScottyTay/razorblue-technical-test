using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace TechnicalTest.DataImport;

//public record Car(string Registration, string Make, string Model, string Colour, string Fuel);
public class Car
{
    [Name("Car Registration")]
    public string Registration { get; set; }
    [Name("Make")]
    public string Make { get; set; }
    [Name("Model")]
    public string Model { get; set; }
    [Name("Colour")]
    public string Colour { get; set; }
    [Name("Fuel")]
    public string Fuel { get; set; }

    public Car()
    {
        Registration = string.Empty;
        Make = string.Empty;
        Model = string.Empty;
        Colour = string.Empty;
        Fuel = string.Empty;
    }

    public Car(string registration, string make, string model, string colour, string fuel)
    {
        Registration = registration;
        Make = make;
        Model = model;
        Colour = colour;
        Fuel = fuel;
    }
}

public record CarWithoutFuel(string Registration, string Make, string Model, string Colour);