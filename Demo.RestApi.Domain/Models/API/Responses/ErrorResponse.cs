namespace Demo.RestApi.Domain.Models.API.Responses
{
	public class ErrorResponse
	{
		public ErrorResponse() {}
		
		public ErrorResponse(string message)
		{
			Message = message;
		}

		public string Message { get; set; }
	}
}
