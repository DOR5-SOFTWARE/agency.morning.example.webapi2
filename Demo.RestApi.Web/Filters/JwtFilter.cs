using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Demo.RestApi.Domain.DataProviders;
using Demo.RestApi.Domain.Services;

namespace Demo.RestApi.Web.Filters
{
	public class JwtFilter : AuthorizationFilterAttribute
	{
		private readonly IJwtService _jwtService;
		private readonly IResponseBuilderService _responseBuilderService;
		private readonly IRequestDataProvider _requestDataProvider;

		public JwtFilter(IJwtService jwtService, IResponseBuilderService responseBuilderService, IRequestDataProvider requestDataProvider)
		{
			_jwtService = jwtService;
			_responseBuilderService = responseBuilderService;
			_requestDataProvider = requestDataProvider;
		}

		public override void OnAuthorization(HttpActionContext actionContext)
		{
			var shouldExtract = !actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();

			if (shouldExtract)
			{
				try
				{
					var authorizationHeader = actionContext.Request.Headers.Authorization;

					if (authorizationHeader != null)
					{
						var jwtTokenString = authorizationHeader.ToString();

						var newToken = _jwtService.HandleIncomingToken(jwtTokenString);

						_requestDataProvider.SetResponseAuthorizationHeader(newToken);
					}
					else
					{
						throw _responseBuilderService.GenerateUnauthorizedAccessResponse();
					}
				}
				catch
				{
					throw _responseBuilderService.GenerateUnauthorizedAccessResponse();
				}
			}
			base.OnAuthorization(actionContext);
		}
	}
}
