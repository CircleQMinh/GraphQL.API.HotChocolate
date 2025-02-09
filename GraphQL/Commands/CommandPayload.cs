using GraphQL.API.HotChocolate.Models;

namespace GraphQL.API.HotChocolate.GraphQL.Commands
{
    public record AddCommandPayload(Command command);
    public record EditCommandPayload(Command command);
    public record DeleteCommandPayload(Command command, bool success);
}
