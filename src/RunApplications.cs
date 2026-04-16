#:property PackageId=RunApplications
#:property ToolCommandName=run-apps
#:property NoDefaultExcludes=true
#:property PackageOutputPath=./nupkg

// dotnet pack RunApplications.cs  --output ./nupkg --force
// dotnet tool install -g --add-source ./nupkg RunApplications

using System;
using System.Diagnostics;

var rootDir = @"C:\Git\Esfa\";
const string terminalName = nameof(terminalName);

var changedRootDir = args.Contains("rootdir", StringComparer.OrdinalIgnoreCase);
if (changedRootDir)
{
    Console.WriteLine($"Enter the root directory for the projects (default: {rootDir}):");
    var dir = Console.ReadLine();
    rootDir = string.IsNullOrWhiteSpace(dir) ? rootDir : dir.Trim();
}


Process.Start("wt", $"-w {terminalName} nt --title \"5111 RoatpV2 API Inner\" -d \"{rootDir}das-roatp-api\\src\\SFA.DAS.RoATP.Api\" -c dotnet run");
Process.Start("wt", $"-w {terminalName} nt --title \"37952 RoATP Service\" -d \"{rootDir}das-roatp-service\\src\\SFA.DAS.RoATPService.Application.Api\" -c dotnet run --release");
Process.Start("wt", $"-w {terminalName} nt --title \"5008 LocApi\" -d \"{rootDir}das-location-api\\src\\SFA.DAS.Location.Api\" -c dotnet run --release");
Process.Start("wt", $"-w {terminalName} nt --title \"5011 CrsInApi\" -d \"{rootDir}das-courses-api\\src\\SFA.DAS.Courses.Api\" -c dotnet run --release");

// Console.WriteLine("Do you want to run the Course Management? (Y/N)");
// var runCourseManagement = Console.ReadKey().Key == ConsoleKey.Y;
var runCourseManagement = args.Contains("cm", StringComparer.OrdinalIgnoreCase);
if (runCourseManagement)
{
    Process.Start("wt", $"wt -w {terminalName} nt --title \"5334 CM Outer\" -d \"{rootDir}das-apim-endpoints\\src\\RoatpCourseManagement\\SFA.DAS.RoatpCourseManagement.Api\" -c dotnet run --release");
    Process.Start("wt", $"wt -w {terminalName} nt --title \"5021 CM Web\" -d \"{rootDir}das-roatp-coursemanagement-web\\src\\SFA.DAS.Roatp.CourseManagement.Web\" -c dotnet run --release");
}

var runFAT = args.Contains("fat", StringComparer.OrdinalIgnoreCase);
if (runFAT)
{
    Process.Start("wt", $"wt -w {terminalName} nt --title \"5003 FAT Outer\" -d \"{rootDir}das-apim-endpoints\\src\\FindApprenticeshipTraining\\SFA.DAS.FindApprenticeshipTraining.Api\" -c dotnet run --release");
    Process.Start("wt", $"wt -w {terminalName} nt --title \"5004 FAT Web\" -d \"{rootDir}das-findapprenticeshiptraining\\src\\SFA.DAS.FAT.Web\" -c dotnet run --release");
}

var runAdmin = args.Contains("admin", StringComparer.OrdinalIgnoreCase);
if (runAdmin)
{
    Process.Start("wt", $"wt -w {terminalName} nt --title \"5005 Admin Outer\" -d \"{rootDir}das-apim-endpoints\\src\\Admin\\SFA.DAS.Admin.Api\" -c dotnet run --release");
    Process.Start("wt", $"wt -w {terminalName} nt --title \"5006 Admin Web\" -d \"{rootDir}das-admin\\src\\SFA.DAS.Admin.Web\" -c dotnet run --release");
}
