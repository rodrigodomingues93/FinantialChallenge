﻿namespace Infrastructure.ErrorMessages
{
	public class Error<T> where T : class
	{
		public string Code { get; set; }
		public List<string> Message { get; set; }
		public T Contract { get; set; }

		public Error(string code, List<string> message, T contract)
		{
			Code = code;
			Message = message;
			Contract = contract;
		}
	}
}
