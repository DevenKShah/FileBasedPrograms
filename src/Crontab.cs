#:package NCrontab@3.4.0

using NCrontab;

const string helperIncludingSeconds = @"
    * * * * * *
    - - - - - -
    | | | | | |
    | | | | | +--- day of week (0 - 6) (Sunday=0)
    | | | | +----- month (1 - 12)
    | | | +------- day of month (1 - 31)
    | | +--------- hour (0 - 23)
    | +----------- min (0 - 59)
    +------------- sec (0 - 59)";
const string helperExcludingSeconds = @"
    * * * * *
    - - - - -
    | | | | |
    | | | | +--- day of week (0 - 6) (Sunday=0)
    | | | +----- month (1 - 12)
    | | +------- day of month (1 - 31)
    | +--------- hour (0 - 23)
    +----------- min (0 - 59)";

string cron = " * 0 12 * */2 Mon"; // Monday of every other month
int numberOfOccurrences = 5;
bool includingSeconds = false;

if (args.Length > 0)
{
    cron = args[0];
    numberOfOccurrences = args.Length > 1 ? int.Parse(args[1]) : numberOfOccurrences;
    includingSeconds = cron.Split(" ").Length == 6;
}

var schedule = CrontabSchedule.TryParse(cron, new CrontabSchedule.ParseOptions { IncludingSeconds = includingSeconds });

if (schedule == null)
{
    Console.WriteLine($"Invalid cron expression: {cron}");
    Console.WriteLine(includingSeconds ? helperIncludingSeconds : helperExcludingSeconds);
    return;
}

var start = DateTime.Now;
var end = start.AddYears(1);

var occurrences = schedule.GetNextOccurrences(start, end);

Console.WriteLine(string.Join(Environment.NewLine,
                  from t in occurrences.Take(numberOfOccurrences)
                  select $"{t:ddd, dd MMM yyyy HH:mm}"));

