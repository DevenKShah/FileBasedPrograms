#:property PackageId=CheckDns
#:property ToolCommandName=check-dns
#:property NoDefaultExcludes=true
#:property PackageOutputPath=./nupkg
#:property Version=1.0.0.0

/// increment the version
/// dotnet pack CheckDns.cs --output ./nupkg --force
/// dotnet tool install -g --add-source ./nupkg CheckDns

using System.Net;

if (args.Length == 0)
{
    Console.WriteLine("Usage: checkdns <hostname>");
    return;
}

var host = args[0];

try
{
    var entry = await Dns.GetHostEntryAsync(host);
    Console.WriteLine($"Resolved {host} to {entry.AddressList[0]}");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to resolve {host}: {ex.Message}");
}

try
{
    var addresses = await Dns.GetHostAddressesAsync(host);
    Console.WriteLine($"Addresses for {host}: {string.Join(", ", addresses)}");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to get addresses for {host}: {ex.Message}");
}