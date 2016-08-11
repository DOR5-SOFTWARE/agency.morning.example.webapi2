using Demo.RestApi.Domain.Models.ExampleDataObjects;

namespace Demo.RestApi.Domain.Services.ExampleDataProcessors
{
	public class SecondExampleDataProcessor : IExampleDataProcessor
	{
		public bool CanProcess(ExampleDataObject dataObject)
		{
			return dataObject.Source == "Second Source";
		}

		public string Process(ExampleDataObject dataObject)
		{
			return dataObject.Data;
		}
	}
}
