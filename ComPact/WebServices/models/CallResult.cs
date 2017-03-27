using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComPact.models
{
	public class CallResult<T>
	{
		public int status_code { get; set; }
		public string message { get; set; }
		[JsonProperty("data")]
		public CallResultData<T> Data { get; set; }
	}

	public class CallResultData<T>
	{
		[JsonProperty("result_count")]
		public int Count { get; set; }
		[JsonProperty("results")]
		public List<T> Results { get; set; }
	}
}
