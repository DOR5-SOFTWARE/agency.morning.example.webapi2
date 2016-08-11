using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Demo.RestApi.Domain.Models.API.Responses;
using Newtonsoft.Json;

namespace Demo.RestApi.Domain.Services
{
	public interface IResponseBuilderService
	{
		HttpResponseException GenerateUnexpectedExceptionResponse(Exception ex);
		HttpResponseException GenerateUnauthorizedAccessResponse();
		HttpResponseException GenerateForbiddenAccessResponse(string errorMessage);
		HttpResponseException GenerateErrorResponse(string errorMessage);
		HttpResponseException GenerateValidationResponse(ModelStateDictionary modelState);
		HttpResponseException GenerateValidationResponse(Dictionary<string, string[]> validationMessages);
		HttpResponseException GenerateResponse(Exception ex);
	}

	public class ResponseBuilderService : IResponseBuilderService
	{
		public HttpResponseException GenerateUnexpectedExceptionResponse(Exception ex)
		{
			return new HttpResponseException(
				new HttpResponseMessage
					{
						StatusCode = HttpStatusCode.InternalServerError,
						Content = new StringContent(
							JsonConvert.SerializeObject(new ErrorResponse(ex.Message)),
							Encoding.UTF8,
							"application/json"
							)
					});
		}

		public HttpResponseException GenerateUnauthorizedAccessResponse()
		{
			return new HttpResponseException(
				new HttpResponseMessage
					{
						StatusCode = HttpStatusCode.Unauthorized,
						Content = new StringContent(
							JsonConvert.SerializeObject(new ErrorResponse("Unauthorized access")),
							Encoding.UTF8,
							"application/json"
							)
					});
		}

		public HttpResponseException GenerateForbiddenAccessResponse(string errorMessage)
		{
			return new HttpResponseException(
				new HttpResponseMessage
					{
						StatusCode = HttpStatusCode.Forbidden,
						Content = new StringContent(
							JsonConvert.SerializeObject(new ErrorResponse {Message = errorMessage ?? "Forbidden Access"}),
							Encoding.UTF8,
							"application/json"
							)
					});
		}

		public HttpResponseException GenerateErrorResponse(string errorMessage)
		{
			return new HttpResponseException(
				new HttpResponseMessage
					{
						StatusCode = HttpStatusCode.BadRequest,
						Content = new StringContent(
							JsonConvert.SerializeObject(new ErrorResponse(errorMessage)),
							Encoding.UTF8,
							"application/json"
							)
					});
		}

		public HttpResponseException GenerateValidationResponse(ModelStateDictionary modelState)
		{
			var validationMessages = modelState.Where(x => x.Value.Errors.Count > 0)
				.ToDictionary(
					kvp => (kvp.Key.Split('.').Length > 1) ? kvp.Key.Split('.')[1] : kvp.Key,
					kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
				);

			return GenerateValidationResponse(validationMessages);
		}

		public HttpResponseException GenerateValidationResponse(Dictionary<string, string[]> validationMessages)
		{
			return new HttpResponseException(
				new HttpResponseMessage
					{
						StatusCode = HttpStatusCode.PreconditionFailed,
						Content = new StringContent(
							JsonConvert.SerializeObject(new ValidationResponse {ValidationMessages = validationMessages}),
							Encoding.UTF8,
							"application/json"
							)
					});
		}

		public HttpResponseException GenerateResponse(Exception ex)
		{
			if (ex is UnauthorizedAccessException)
			{
				return GenerateForbiddenAccessResponse(ex.Message);
			}

			return GenerateUnexpectedExceptionResponse(ex);
		}
	}
}
