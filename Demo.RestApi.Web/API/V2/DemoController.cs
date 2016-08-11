using System;
using System.Linq;
using System.Web.Http;
using Demo.RestApi.Common.Enums;
using Demo.RestApi.Common.ExtensionMethods;
using Demo.RestApi.Domain.DataProviders;
using Demo.RestApi.Domain.Models.API;
using Demo.RestApi.Domain.Models.ExampleDataObjects;
using Demo.RestApi.Domain.Models.Session;
using Demo.RestApi.Domain.Services;
using Demo.RestApi.Domain.Services.ExampleDataProcessors;
using Demo.RestApi.Web.API.V1;

namespace Demo.RestApi.Web.API.V2
{
	[RoutePrefix("api/v2/demo")]
	public class Demo2Controller : Demo1Controller
	{
		protected readonly IExampleDataProcessor[] ExampleDataProcessors;

		public Demo2Controller(IJwtService jwtService, IResponseBuilderService responseBuilderService, IRequestDataProvider requestDataProvider, IExampleDataProcessor[] exampleDataProcessors)
			: base(jwtService, responseBuilderService, requestDataProvider)
		{
			ExampleDataProcessors = exampleDataProcessors;
		}

		[Route("authenticate")]
		public override string Authenticate([FromUri] DemoAuthenticationModel authenticationModel)
		{
			return base.Authenticate(authenticationModel);
		}

		[Route("decrypt-token")]
		public override JwtData DecryptJwtString()
		{
			return new JwtData
				       {
					       Id = "This is an overriden method to see the real data please go to V1 of the API",
					       PhoneNumber = "This is an overriden method to see the real data please go to V1 of the API",
					       ClientProcessStep = ClientProcessStep.NA,
					       IsCaptchaRequired = false,
					       exp = DateTime.Now.ToUnixDateTime()
				       };
		}

		[Route("update-client-process")]
		public override string UpdateClientProcess(ClientProcessStep processStep)
		{
			return base.UpdateClientProcess(processStep);
		}

		[Route("process-data")]
		[HttpPost]
		[AllowAnonymous]
		public virtual string ProcessData(ExampleDataObject dataObject)
		{
			if (!ModelState.IsValid)
				throw ResponseBuilderService.GenerateValidationResponse(ModelState);

			var exampleDataProcessor = ExampleDataProcessors.FirstOrDefault(p => p.CanProcess(dataObject));

			if (exampleDataProcessor == null)
			{
				throw ResponseBuilderService.GenerateErrorResponse("No Suitable Processor Found");
			}
			return exampleDataProcessor.Process(dataObject);
		}
	}
}
