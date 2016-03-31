using System.Security.Policy;
using Jal.RestClient.Fluent;
using Jal.RestClient.Interface;

namespace Jal.RestClient.Impl
{
    public class RestHandlerBuilder : IRestHandlerBuilder
    {
        private readonly IRestHandler _restHandler;

        public RestHandlerBuilder(IRestHandler restHandler)
        {
            _restHandler = restHandler;
        }

        public IAcceptedTypeDescriptor Get(string url)
        {
            return new RestHandlerDescriptor(url, (u, a, c, t, at) => _restHandler.Get(u, at, a), (u, a, c, t, at) => _restHandler.GetAsync(u, at, a));
        }

        public IContentDescriptor Post(string url)
        {
            return new RestHandlerDescriptor(url, (u, a, c, t, at) => _restHandler.Post(u, c, t, a), (u, a, c, t, at) => _restHandler.PostAsync(u, c, t, a));
        }

        public IContentDescriptor Put(string url)
        {
            return new RestHandlerDescriptor(url, (u, a, c, t, at) => _restHandler.Put(u, c, t, a), (u, a, c, t,at) => _restHandler.PutAsync(u, c, t, a));
        }

        public IAuthenticatorDescriptor Delete(string url)
        {
            return new RestHandlerDescriptor(url, (u, a, c, t, at) => _restHandler.Delete(u, a), (u, a, c, t, at) => _restHandler.DeleteAsync(u, a));
        }
    }
}
