namespace Portal.Api.Base;

public interface IApiHandler<TRequest, TResponse> where TRequest : IApiResult<TResponse>
{
    Task<TResponse> Handle(TRequest request);
}