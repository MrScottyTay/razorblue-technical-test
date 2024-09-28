using System.Text.RegularExpressions;

namespace TechnicalTest.DataImport;
public record ValidRegistrationResult(List<Car> ValidCars, int InvalidCount);

public static class Extensions
{
    public static CarWithoutFuel ToWithoutFuel(this Car car) => new(car.Registration, car.Make, car.Model, car.Colour);

    public const string ValidRegistrationPattern = @"[A-Z]{2}[0-9]{2} [A-Z]{3}";
    public static ValidRegistrationResult GetValidRegistrations(this List<Car> cars)
    {
        List<Car> validCars = [.. cars.ToList().Where(x => Regex.Match(x.Registration, ValidRegistrationPattern).Success)];
        int invalidCount = cars.Count - validCars.Count;
        return new(validCars, invalidCount);
    }

    public static List<String> FuelTypes = ["Petrol", "Diesel", "Hybrid", "Electric"];
    public static void CorrectFuel(this Car car)
    {
        car.Fuel = FuzzySharp.Process.ExtractOne(car.Fuel, FuelTypes).Value;
    }
}
