using Demo.RestApi.Domain.Models.ExampleDataObjects;

namespace Demo.RestApi.Domain.Services.ExampleDataProcessors
{
	public class ThirdExampleDataProcessor : IExampleDataProcessor
	{
		public bool CanProcess(ExampleDataObject dataObject)
		{
			return dataObject.Source == "Third Source";
		}

		public string Process(ExampleDataObject dataObject)
		{
			return dataObject.Data;
		}
	}
}
