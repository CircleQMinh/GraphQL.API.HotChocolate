using GraphQL.API.HotChocolate.Models;

namespace GraphQL.API.HotChocolate.GraphQL.Commands
{
    public class CommandPayload
    {
    }
    public record AddCommandPayload(Command command);
    public record EditCommandPayload(Command command);
}
