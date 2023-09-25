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
        /// <summary>
        /// Adds a <see cref="Platform"/> based on <paramref name="input"/>.
        /// </summary>
        /// <param name="input">The <see cref="AddPlatformInput"/>.</param>
        /// <param name="context">The <see cref="AppDbContext"/>.</param>
        /// <param name="eventSender">The <see cref="ITopicEventSender"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>The added <see cref="Platform"/>.</returns>
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

        /// <summary>
        /// Adds a <see cref="Command"/> based on <paramref name="input"/>.
        /// </summary>
        /// <param name="input">The <see cref="AddCommandInput"/>.</param>
        /// <param name="context">The <see cref="AppDbContext"/>.</param>
        /// <returns>The added <see cref="Command"/>.</returns>
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
                return new EditCommandPayload(command);
            }

            command.HowTo = input.HowTo;
            command.CommandLine = input.CommandLine;
            command.PlatformId = input.PlatformId;


            context.Commands.Update(command);
            await context.SaveChangesAsync();

            return new EditCommandPayload(command);
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
