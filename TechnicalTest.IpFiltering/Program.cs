using TechnicalTest.IpFiltering;

// would use EF to create a DbContext and have AllowedAddresses as a DbSet.
// Would maybe have a model that also has an Enum for determining if it's a singular address, a standard range, or a CIDR range
// so that the IpService can check that enum rather than parsing that from the string itself
List<string> allowedAddresses = [
    "192.168.1.1",
    "10.0.0.5",
    "172.16.0.10",
    "192.168.2.1 - 192.168.2.10",
    "10.0.1.5 - 10.0.1.15",
    "172.16.1.10 - 172.16.1.20",
    "192.168.3.0/24",
    "10.0.2.0/24",
    "172.16.2.0/20"
    ];

IpService ipService = new(allowedAddresses);

while (true)
{
    Console.WriteLine("Enter Ipv4 Address to see if it is an allowed address: (CTRL+C or type 'exit' to exit)");
    string? input = Console.ReadLine();

    if (input == "exit") break;

    if (input == null || !IpService.IsIpAddress(input))
    {
        Console.WriteLine("You did not enter a valid IP Address, try again\n");
        continue;
    }

    if (ipService.IsAllowed(input)) Console.WriteLine("That is an allowed address\n");
    else Console.WriteLine("That is NOT an allowed address\n");
}