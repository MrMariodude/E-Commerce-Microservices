using Concordia;

namespace BuildingBlocks.CQRS;

//? We marked the TResponse without out modifire why?
//? It may appears that:
//? 1) Handlers are interchangeable by response type
//? 2) Polymorphism matters here
//? But in reality we:
//? 1) MediatR won’t use that flexibility
//? 2) DI won’t benefit from it
//? 3) Your code won’t either
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull { }

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand { }
