using GraphQL.API.HotChocolate.Data;
using GraphQL.API.HotChocolate.Models;
using HotChocolate.Types.Pagination;

namespace GraphQL.API.HotChocolate.GraphQL
{
    [GraphQLDescription("Represents the queries available.")]
    public class Query
    {

        [GraphQLDescription("Gets the queryable platform.")]
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseSorting]
        [UseFiltering]

        public IQueryable<Platform> GetPlatform(AppDbContext context)
        {
            return context.Platforms;
        }


        [GraphQLDescription("Gets the queryable command.")]
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseSorting]
        [UseFiltering]
        public IQueryable<Command> GetCommand(AppDbContext context)
        {
            return context.Commands;
        }
    }
    //query{
    //    platform(skip:0,take:2,order: [{ name: DESC }],
    //        where: {
    //      or: [{ name: { contains: "W" } }, { name: { contains: "U" } }]
    //      and: [{id: {gt:0}},{id:{ngt:5} }]
    //      id : {gt:3}
    //    }){
    //        items{
    //            id
    //            name
    //        }
    //        pageInfo{
    //            hasNextPage
    //            hasPreviousPage
    //        }
    //        totalCount,
    //    }
    //}


}
