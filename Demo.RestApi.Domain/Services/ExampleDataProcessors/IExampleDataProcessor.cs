using Demo.RestApi.Domain.Models.ExampleDataObjects;

namespace Demo.RestApi.Domain.Services.ExampleDataProcessors
{
	public interface IExampleDataProcessor
	{
		bool CanProcess(ExampleDataObject dataObject);
		string Process(ExampleDataObject dataObject);
	}
}
