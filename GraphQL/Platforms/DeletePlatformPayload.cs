using GraphQL.API.HotChocolate.Models;

namespace GraphQL.API.HotChocolate.GraphQL.Platforms
{
    public record DeletePlatformPayload(Platform platform, bool success);
}
