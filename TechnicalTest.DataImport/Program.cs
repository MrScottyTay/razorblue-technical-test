using CsvHelper;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using TechnicalTest.DataImport;

// reading csv
using StreamReader streamReader = new("Technical Test Data.csv");
using CsvReader csvReader = new(streamReader, CultureInfo.InvariantCulture);
//csvReader.Context.RegisterClassMap<CarMap>();
List<Car> cars = [.. csvReader.GetRecords<Car>()];

// write csvs for each fuel type
cars.ForEach(x => x.CorrectFuel());
IEnumerable<IGrouping<string, Car>> carsByFuel = cars.GroupBy(x => x.Fuel);
foreach (IGrouping<string, Car> carGroup in carsByFuel)
{
    using StreamWriter streamWriter = new($"{carGroup.Key} cars.csv");
    using CsvWriter csvWriter = new(streamWriter, CultureInfo.InvariantCulture);
    csvWriter.WriteRecords(carGroup.Select(x => x.ToWithoutFuel()));
}

// print list of valid registrations and count invalids
ValidRegistrationResult validRegistrationResult = cars.GetValidRegistrations();
Console.WriteLine("Cars with valid registrations:");
foreach (Car car in validRegistrationResult.ValidCars) Console.WriteLine($"{car.Registration} - {car.Colour} {car.Make} {car.Model} ({car.Fuel})");
Console.WriteLine($"\nCars with invalid registrations: {validRegistrationResult.InvalidCount}");