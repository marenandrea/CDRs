using System.Text.Json;

var data = File.ReadAllText("cdrs.json");
var cdrs = JsonSerializer.Deserialize<List<Cdr>>(data);

Console.WriteLine("Top 3 Most Active Callers:");
var topCallers = cdrs.GroupBy(g => g.Caller).OrderByDescending(o => o.Count()).Take(3);
foreach (var i in topCallers)
{
    Console.WriteLine($"{i.Key}: {i.Count()} calls");
}

var topCaller = topCallers.First().Key;
var durationOfCallsToTopCaller = cdrs.Where(w => w.Receiver == topCaller).Sum(s => s.Duration);
Console.WriteLine($"Total Duration of Calls to {topCaller}: {durationOfCallsToTopCaller} seconds");

var uniquePhoneNumbers = cdrs.SelectMany(s => new List<string> { s.Caller, s.Receiver }).Distinct().Count();
Console.WriteLine($"Total Unique Phone Numbers: {uniquePhoneNumbers}");

public class Cdr
{
    public string Caller { get; set; }
    public string Receiver { get; set; }
    public string StartTime { get; set; }
    public int Duration { get; set; }

}