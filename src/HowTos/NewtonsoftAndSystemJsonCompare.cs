#:package Newtonsoft.Json@13.0.1

using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading.Tasks.Dataflow;
using Newtonsoft.Json;


Action serialiseUsingSystemJson = () =>
{
    RecordOversightOutcomeCommand person = new()
    {
        ApplicationId = Guid.NewGuid(),
        ApproveGateway = true,
        ApproveModeration = false,
        OversightStatus = OversightReviewStatus.Successful,
        UserId = "user123",
        UserName = "John Doe",
        InternalComments = "All good",
        ExternalComments = "Approved"
    };
    string jsonString = System.Text.Json.JsonSerializer.Serialize(person);
    Console.WriteLine(jsonString);
};

serialiseUsingSystemJson();

Action deserialiseEmptyUsingSystemJson = () =>
{
    Person? deserializedPerson = System.Text.Json.JsonSerializer.Deserialize<Person>("");
    Console.WriteLine(deserializedPerson);
};

//deserialiseEmptyUsingSystemJson();

Action deserialiseEmptyUsingNewstonsoftJson = () =>
{
    //    Person person = new Person("Alice", 30);
    //    string jsonString = JsonSerializer.Serialize(person);
    Person? deserializedPerson = JsonConvert.DeserializeObject<Person>("");
    //Person? deserializedPerson = JsonSerializer.Deserialize<Person>("");
    Console.WriteLine(deserializedPerson?.ToString() ?? "nullllll");
};


//deserialiseEmptyUsingNewstonsoftJson();


List<CourseType> courseTypes = [CourseType.ApprenticeshipUnit];

var route = courseTypes.Count switch
{
    0 => "ReviewYourDetails",
    1 when courseTypes.Contains(CourseType.Apprenticeship) => "Standards",
    1 when courseTypes.Contains(CourseType.ApprenticeshipUnit) => "Units",
    _ => "chooseType"
};
Console.WriteLine(route);
public record Person(string Name, int Age);
enum CourseType
{
    Apprenticeship,
    ApprenticeshipUnit
}

public class RecordOversightOutcomeCommand
{
    public Guid ApplicationId { get; set; }
    public bool? ApproveGateway { get; set; }
    public bool? ApproveModeration { get; set; }
    public OversightReviewStatus OversightStatus { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string InternalComments { get; set; }
    public string ExternalComments { get; set; }
}

public enum OversightReviewStatus
{
    None = 0,
    Successful = 1,
    SuccessfulAlreadyActive = 2,
    SuccessfulFitnessForFunding = 3,
    Unsuccessful = 4,
    InProgress = 5,
    Rejected = 6,
    Withdrawn = 7,
    Removed = 8
}