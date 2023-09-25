namespace GraphQL.API.HotChocolate.GraphQL.Commands
{
    public class CommandInput
    {
    }
    public record AddCommandInput(string HowTo, string CommandLine, int PlatformId);
    public record EditCommandInput(int Id, string HowTo, string CommandLine, int PlatformId);
}
