namespace Portal.Api.Base;

public interface IApiProcessor
{
    Task<TResponse> Process<TResponse>(IApiResult<TResponse> model);
}
