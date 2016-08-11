using System.Net;
using System.Web.Http;
using Demo.RestApi.Common.Configuration;
using Demo.RestApi.Common.Enums;
using Demo.RestApi.Domain.DataProviders;
using Demo.RestApi.Domain.Models.API;
using Demo.RestApi.Domain.Models.API.Responses;
using Demo.RestApi.Domain.Models.Session;
using Demo.RestApi.Domain.Services;
using Swashbuckle.Swagger.Annotations;

namespace Demo.RestApi.Web.API.V1
{
	[RoutePrefix("api/v1/demo")]
	public class Demo1Controller : ApiController
	{
		protected readonly IJwtService JwtService;
		protected readonly IResponseBuilderService ResponseBuilderService;
		protected readonly IRequestDataProvider RequestDataProvider;

		public Demo1Controller(IJwtService jwtService, IResponseBuilderService responseBuilderService, IRequestDataProvider requestDataProvider)
		{
			JwtService = jwtService;
			ResponseBuilderService = responseBuilderService;
			RequestDataProvider = requestDataProvider;
		}

		[Route("authenticate")]
		[HttpPost]
		[AllowAnonymous]
		[SwaggerResponse(HttpStatusCode.PreconditionFailed, @"Returns a dictionary<string,string[]> of validation messages", Type = typeof (ValidationResponse))]
		[SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof (ErrorResponse))]
		public virtual string Authenticate([FromUri] DemoAuthenticationModel authenticationModel)
		{
			if (ModelState.IsValid)
			{
				var tokenData = new JwtData
					                {
						                Id = authenticationModel.Id,
						                PhoneNumber = authenticationModel.MobilePhone,
						                IsCaptchaRequired = false,
						                ClientProcessStep = ClientProcessStep.NA
					                };

				return "token : " + JwtService.CreateToken(tokenData);
			}

			throw ResponseBuilderService.GenerateValidationResponse(ModelState);
		}

		[Route("decrypt-token")]
		[HttpGet]
		public virtual JwtData DecryptJwtString()
		{
			var tokenBody = RequestDataProvider.GetCachedItem<JwtData>("JwtContent");
			return tokenBody;
		}

		[Route("update-client-process")]
		[HttpPut]
		public virtual string UpdateClientProcess(ClientProcessStep processStep)
		{
			if (!ModelState.IsValid)
				throw ResponseBuilderService.GenerateValidationResponse(ModelState);

			var currentData = RequestDataProvider.GetCachedItem<JwtData>(CommonNames.JWT_CONTENT);
			currentData.ClientProcessStep = processStep;

			var newToken = JwtService.CreateToken(currentData);

			RequestDataProvider.SetResponseAuthorizationHeader(newToken);

			return newToken;
		}
	}
}
