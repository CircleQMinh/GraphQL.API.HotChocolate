namespace GraphQL.API.HotChocolate.GraphQL.Commands
{
    public record AddCommandInput(string HowTo, string CommandLine, int PlatformId);
    public record EditCommandInput(int Id, string HowTo, string CommandLine, int PlatformId);
    public record DeleteCommandInput(int Id);
}
