using System;
using System.Collections.Generic;

namespace ComPact.Models
{
	public class CallResult//<T>
	{
		public int status_code { get; set; }
		public bool success { get; set; }
		public string message { get; set; }
		//[Newtonsoft.Json.JsonProperty("data")]
		//public CallResultData<T> Data { get; set; }
	}

	public class CallResultData<T>
	{
		[Newtonsoft.Json.JsonProperty("result_count")]
		public int Count { get; set; }
		[Newtonsoft.Json.JsonProperty("results")]
		public List<T> Results { get; set; }	
	}
}
