using System;
using System.Collections.Generic;

namespace ComPact
{
	public class WebServiceCallResult<T>
	{
		public int status_code { get; set; }
		public string message { get; set; }
		[Newtonsoft.Json.JsonProperty("data")]
		public WebServiceCallResultData<T> Data { get; set; }
	}

	public class WebServiceCallResultData<T>
	{
		[Newtonsoft.Json.JsonProperty("result_count")]
		public int Count { get; set; }
		[Newtonsoft.Json.JsonProperty("results")]
		public List<T> Results { get; set; }
	}
}
