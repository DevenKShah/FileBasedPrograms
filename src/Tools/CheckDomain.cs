#:property PackageId=CheckDomain
#:property ToolCommandName=check-domain
#:property NoDefaultExcludes=true
#:property PackageOutputPath=./nupkg
#:property Version=1.1.0.1

/// increment the version
/// dotnet pack CheckDomain.cs --output ./nupkg --force
/// dotnet tool install -g --add-source ./nupkg CheckDomain


#:package DnsClient@1.8.0

using System.Net;
using DnsClient;

if (args.Length == 0)
{
    Console.WriteLine("Usage: check-domain <domain>");
    return;
}

var domain = args[0];

try
{
    var entry = await Dns.GetHostEntryAsync(domain);
    Console.WriteLine($"Resolved {domain} to {entry.AddressList[0]}");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to resolve {domain}: {ex.Message}");
}

try
{
    var addresses = await Dns.GetHostAddressesAsync(domain);
    Console.WriteLine($"Addresses for {domain}: {string.Join(", ", addresses)}");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to get addresses for {domain}: {ex.Message}");
}

var lookupClient = new LookupClient();

// Explicitly query for Mail Exchanger (MX) records
var result = await lookupClient.QueryAsync(domain, QueryType.MX);
var mxRecord = result.Answers.MxRecords().FirstOrDefault()?.Exchange.ToString();

Console.WriteLine($"MX record for {domain}: {mxRecord}");

if (result.HasError)
{
    Console.WriteLine($"DNS Error: {result.ErrorMessage}");
    return;
}