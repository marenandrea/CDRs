using System.Text.Json;

try
{
    var data = File.ReadAllText("cdrs.json");
    var cdrs = JsonSerializer.Deserialize<List<Cdr>>(data) ?? throw new Exception("Deserialization failed.");
    if (cdrs.Count == 0)
    {
        throw new Exception("Call Detail Records are missing.");
    }

    var topThreeCallers = cdrs.GroupBy(g => g.Caller).OrderByDescending(o => o.Count()).Take(3);
    Console.WriteLine("Top 3 Most Active Callers:");
    foreach (var caller in topThreeCallers)
    {
        Console.WriteLine($"{caller.Key}: {caller.Count()} calls");
    }

    var topCaller = topThreeCallers.First().Key;
    var durationOfCallsToTopCaller = cdrs.Where(w => w.Receiver == topCaller).Sum(s => s.Duration);
    Console.WriteLine($"Total Duration of Calls to {topCaller}: {durationOfCallsToTopCaller} seconds");

    var uniquePhoneNumbers = cdrs.SelectMany(s => new List<string> { s.Caller, s.Receiver }).Distinct().Count();
    Console.WriteLine($"Total Unique Phone Numbers: {uniquePhoneNumbers}");
}
catch (Exception e)
{
    Console.WriteLine($"Something went wrong: {e.Message}");
}

public class Cdr
{
    public string Caller { get; set; }
    public string Receiver { get; set; }
    public DateTime StartTime { get; set; }
    public int Duration { get; set; }
}
