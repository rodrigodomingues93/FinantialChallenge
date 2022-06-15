namespace CashBookAPIClient.Options
{
	public class CashBookOptions
	{
		private string _address;
		public string Address
		{
			get { return _address ?? throw new InvalidOperationException("The Address must be setted"); }
			set { _address = value; }
		}

		private string _endPoint;
		public string EndPoint
		{
			get { return _endPoint ?? throw new InvalidOperationException("CashBook endpoint must be setted"); }
			set { _endPoint = value; }
		}

		public string GetCashBookEndPoint() => $"{Address}/{EndPoint}";
	}
}
