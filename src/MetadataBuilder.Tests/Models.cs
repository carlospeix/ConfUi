using System;

namespace MetadataBuilder.Tests
{
	public class Customer
	{
		public int Id { get; private set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Country Country { get; set; }
	}

	public class Provider
	{
		public int Code { get; private set; }
		public string Name { get; set; }
		public string CategoryCode { get; set; }
	}

	public class Country
	{
		public int Id { get; private set; }
		public string Name { get; set; }
		public DayOfWeek FirstLaborableDay { get; set; }
	}
}
