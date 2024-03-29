using Newtonsoft.Json;

namespace Coderbyte;

class FilterJsonSolver
{
    public static async Task FilterJson()
    {
        HttpClient client = new HttpClient();
        string s = await client.GetStringAsync("https://coderbyte.com/api/challenges/json/json-cleaning");

        var initial = JsonConvert.DeserializeObject<Person>(s);
        var filtered = new Person
        {
            name = new Name
            {
                first = NullWhenBad(initial.name.first),
                middle = NullWhenBad(initial.name.middle),
                last = NullWhenBad(initial.name.last),
            },
            age = initial.age,
            DOB = NullWhenBad(initial.DOB),
            hobbies = initial.hobbies.Select(NullWhenBad).Where(v => v != null).ToArray(),
            education = initial.education.Select(p => (NullWhenBad(p.Key), NullWhenBad(p.Value)))
                .Where(p => p.Item1 != null && p.Item2 != null)
                .ToDictionary(p => p.Item1, p => p.Item2)
        };

        Console.WriteLine(JsonConvert.SerializeObject(filtered, new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        }));
    }

    private static readonly HashSet<string> _badSymbols = new(new[] { "", "-", "N/A" });
    private static string NullWhenBad(string s) => _badSymbols.Contains(s) ? null : s;

    private record Person
    {
        public Name name { get; init; }
        public int age { get; init; }
        public string DOB { get; init; }
        public string[] hobbies { get; init; }
        public Dictionary<string, string> education { get; init; }
    }

    private record Name
    {
        public string first { get; init; }
        public string middle { get; init; }
        public string last { get; init; }
    }
}