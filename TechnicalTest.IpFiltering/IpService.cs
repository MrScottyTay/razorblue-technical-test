using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TechnicalTest.IpFiltering;
public class IpService
{
    // replace with DbContext and replace all references of this list to the correct DbSet in the context, other logic should stay the same
    public readonly List<string> AllowedIpAddresses = [];

    // replace input of list to instead be a DbContext (dependency injected if part of a larger project)
    public IpService(List<string> allowedIpAddresses)
    {
        AllowedIpAddresses = allowedIpAddresses;
    }

    public bool IsAllowed(string ipAddress)
    {
        // check to see if input address matches single addresses in list
        if (AllowedIpAddresses.Where(x => !x.Contains('-') && !x.Contains('/')).FirstOrDefault(x => x == ipAddress) != null) return true;

        uint ipAddressValue = GetIpAddressDecimalValue(ipAddress);

        // checking if it fits within ranges
        IEnumerable<string> allowedRanges = AllowedIpAddresses.Where(x => x.Contains('-')); //IEnumerable would become IQueryable with DbContext
        foreach(string ipAddressRange in allowedRanges)
        {
            if (IsWithinRange(ipAddressValue, ipAddressRange)) return true;
        }

        // checking it fits within a CIDR range
        IEnumerable<string> allowedCidrRanges = AllowedIpAddresses.Where(x => x.Contains('/'));
        foreach(string ipAddressCidrRange in allowedCidrRanges)
        {
            if (IsWithinCidrRange(ipAddressValue, ipAddressCidrRange)) return true;
        }

        return false;
    }

    private static string GetIpAddressBinaryValue(string ipAddress)
    {
        string[] octets = ipAddress.Split(".");

        string binary = string.Empty;
        foreach(string octet in octets)
        {
            int octetDecimal = int.Parse(octet);
            string octetBinary = Convert.ToString(octetDecimal, 2).PadLeft(8, '0');
            binary += octetBinary;
        }

        return binary;
    }

    private static uint GetIpAddressDecimalValueFromBinary(string ipAddressBinary) => Convert.ToUInt32(ipAddressBinary, 2);

    private static uint GetIpAddressDecimalValue(string ipAddress)
    {
        string binary = GetIpAddressBinaryValue(ipAddress);
        return GetIpAddressDecimalValueFromBinary(binary);
    }

    private static bool IsWithinRange(uint ipAddressValue, string ipAddressRange)
    {
        string[] ipAddressRangeSplit = ipAddressRange.Replace(" ", "").Split("-");
        string ipAddressStart = ipAddressRangeSplit[0];
        string ipAddressEnd = ipAddressRangeSplit[^1];

        uint ipAddressRangeStartValue = GetIpAddressDecimalValue(ipAddressStart);

        if (ipAddressValue < ipAddressRangeStartValue) return false; // early exit - definitely not within range so don't need to process the end range

        uint ipAddressRangeEndValue = GetIpAddressDecimalValue(ipAddressEnd);

        return ipAddressValue <= ipAddressRangeEndValue; // previous check means it doesn't need to compare with the start again, if it's below/on the end it's within range
    }

    private static bool IsWithinCidrRange(uint ipAddressValue, string ipAddressCidr)
    {
        string[] ipAddressCidrSplit = ipAddressCidr.Split("/");
        string ipAddressCidrBinary = GetIpAddressBinaryValue(ipAddressCidrSplit[0]);
        string ipAddressCidrBinaryBase = ipAddressCidrBinary[.. int.Parse(ipAddressCidrSplit[^1])];

        string ipAddressCidrBinaryStart = ipAddressCidrBinaryBase.PadRight(32, '0');
        uint ipAddressCidrStartValue = GetIpAddressDecimalValueFromBinary(ipAddressCidrBinaryStart);

        if (ipAddressValue < ipAddressCidrStartValue) return false; // early exit - definitely not within range so don't need to process the end range

        string ipAddressCidrBinaryEnd = ipAddressCidrBinaryBase.PadRight(32, '1');
        uint ipAddressCidrEndValue = GetIpAddressDecimalValueFromBinary(ipAddressCidrBinaryEnd);

        return ipAddressValue <= ipAddressCidrEndValue; // previous check means it doesn't need to compare with the start again, if it's below/on the end it's within range
    }

    private const string IpAddressRegexPattern = @"[0-9]{1,3}\.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}";
    public static bool IsIpAddress(string ipAddress) => Regex.Match(ipAddress, IpAddressRegexPattern).Success;
}
