using Demo.RestApi.Domain.Models.ExampleDataObjects;

namespace Demo.RestApi.Domain.Services.ExampleDataProcessors
{
	public class FirstExampleDataProcessor : IExampleDataProcessor
	{
		public bool CanProcess(ExampleDataObject dataObject)
		{
			return dataObject.Source == "First Source";
		}

		public string Process(ExampleDataObject dataObject)
		{
			return dataObject.Data;
		}
	}
}
