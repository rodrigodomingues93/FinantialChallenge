using Moq.AutoMock;

namespace RequestTests
{
	public class ProductRequestTest
	{
		private readonly AutoMocker _mocker;
		public ProductRequestTest()
		{
			_mocker = new AutoMocker();
		}

	}
}
