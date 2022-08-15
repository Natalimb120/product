using System;
using Product.BuildingBlocks.Application;

namespace Product.BuildingBlocks.Application
{
    public interface IApplicationQueryHandler<in TApplicationQuery, TApplicationQueryResult>
        where TApplicationQuery : IApplicationQuery
    {
        Either<Exception, ApplicationQueryResultWrapper<TApplicationQueryResult>>  Handle(TApplicationQuery applicationQuery);
    }
}
