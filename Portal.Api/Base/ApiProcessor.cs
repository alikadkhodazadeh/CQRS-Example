using SimpleInjector;

namespace Portal.Api.Base;

public class ApiProcessor : IApiProcessor
{
    private readonly Container _container;

    public ApiProcessor(Container container)
    {
        _container = container;
    }

    public async Task<TResponse> Process<TResponse>(IApiResult<TResponse> model)
    {
        var handlerType = typeof(IApiHandler<,>).MakeGenericType(model.GetType(), typeof(TResponse));
        dynamic handler = _container.GetInstance(handlerType);
        return await handler.Handle((dynamic)model);
    }
}