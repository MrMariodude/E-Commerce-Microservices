using Concordia;

namespace BuildingBlocks.CQRS;

//? non generic version of ICommand for mediatR with no return
public interface ICommand : IRequest { }

//? This is a generic ICommand return TResponse version for mediatR
public interface ICommand<out TResponse> : IRequest<TResponse> { }
