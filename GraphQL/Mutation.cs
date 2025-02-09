using GraphQL.API.HotChocolate.Data;
using GraphQL.API.HotChocolate.GraphQL.Commands;
using GraphQL.API.HotChocolate.GraphQL.Platforms;
using GraphQL.API.HotChocolate.Models;
using HotChocolate.Execution.Processing;
using HotChocolate.Subscriptions;

namespace GraphQL.API.HotChocolate.GraphQL
{
    [GraphQLDescription("Represents the mutations available.")]
    public class Mutation
    {

        [GraphQLDescription("Adds a platform.")]
        public async Task<AddPlatformPayload> AddPlatformAsync(
            AddPlatformInput input,
            AppDbContext context,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken
            )
        {
            var platform = new Platform
            {
                Name = input.Name,
                LicenseKey = input.LicenseKey
            };

            context.Platforms.Add(platform);
            await context.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);

            return new AddPlatformPayload(platform);
        }
        [GraphQLDescription("Edits a platform.")]
        public async Task<EditPlatformPayload> EditPlatformAsync(EditPlatformInput input, AppDbContext context)
        {
            var platform = await context.Platforms.FindAsync(input.Id);
            if (platform == null)
            {
                return new EditPlatformPayload(new Platform());
            }

            platform.Name = input.Name;
            platform.LicenseKey = input.LicenseKey;


            context.Platforms.Update(platform);
            await context.SaveChangesAsync();

            return new EditPlatformPayload(platform);
        }

        [GraphQLDescription("Deletes a platform.")]
        public async Task<DeletePlatformPayload> DeletePlatformAsync(DeletePlatformInput input, AppDbContext context)
        {
            var entity = context.Platforms.FirstOrDefault(q=>q.Id == input.Id);
            if (entity == null) { 
                return new DeletePlatformPayload(new Platform(), false);
            }

            context.Platforms.Remove(entity);
            await context.SaveChangesAsync();

            return new DeletePlatformPayload(entity, true);
        }


        [GraphQLDescription("Adds a command.")]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, AppDbContext context)
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };

            context.Commands.Add(command);
            await context.SaveChangesAsync();

            return new AddCommandPayload(command);
        }


        [GraphQLDescription("Edits a command.")]
        public async Task<EditCommandPayload> EditCommandAsync(EditCommandInput input, AppDbContext context)
        {
            var command = await context.Commands.FindAsync(input.Id);
            if (command == null)
            {
                return new EditCommandPayload(new Command());
            }

            command.HowTo = input.HowTo;
            command.CommandLine = input.CommandLine;
            command.PlatformId = input.PlatformId;


            context.Commands.Update(command);
            await context.SaveChangesAsync();

            return new EditCommandPayload(command);
        }

        [GraphQLDescription("Deletes a command.")]
        public async Task<DeleteCommandPayload> DeleteCommandAsync(DeleteCommandInput input, AppDbContext context)
        {
            var entity = context.Commands.FirstOrDefault(q => q.Id == input.Id);
            if (entity == null)
            {
                return new DeleteCommandPayload(new Command(), false);
            }

            context.Commands.Remove(entity);
            await context.SaveChangesAsync();

            return new DeleteCommandPayload(entity, true);
        }
    }


//    mutation{
//    addPlatform(input:{
//    name: "Ubuntu",
//        licenseKey: "489"
//    })
//    {
//        platform
//        {
//            id
//            name
//        }
//    }
//}
}
