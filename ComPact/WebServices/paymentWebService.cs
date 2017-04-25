using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ComPact.Models;
using Newtonsoft.Json;

namespace ComPact
{
	public class paymentWebService : BaseWebService<WebPayment>, IPaymentWebService
	{
		public override async Task<WebPayment> Create(string urlExtend, WebPayment payment)
		{
			//Normel data
			//Image image = obj.Image;
			//obj.Image = null;
			//return await base.Create(urlExtend, obj);

			//ImagePost
			var client = GetHttpClient();
			MultipartFormDataContent form = new MultipartFormDataContent();

			if (payment.Image != null && payment.Image.ImageValue != null && payment.Image.Path != null)
			{
				form.Add(new ByteArrayContent(payment.Image.ImageValue), "attachment", payment.Image.Path);
			}

			form.Add(new StringContent(payment.Name, Encoding.UTF8, "application/json"), "name");
			form.Add(new StringContent(payment.Description, Encoding.UTF8, "application/json"), "description");
			form.Add(new StringContent(payment.Price.ToString(), Encoding.UTF8, "application/json"), "price");
			form.Add(new StringContent(payment.MemberId, Encoding.UTF8, "application/json"), "memberId");
			form.Add(new StringContent(payment.CreatedAt.ToString(), Encoding.UTF8, "application/json"), "createdAt");
			form.Add(new StringContent(payment.Image.Path, Encoding.UTF8, "application/json"), "path");

			try
			{
				HttpResponseMessage response = await client.PostAsync(urlExtend, form);
				if (response.StatusCode == HttpStatusCode.Conflict)
				{
					throw new ArgumentException();
				}
				response.EnsureSuccessStatusCode();
				var result = JsonConvert.DeserializeObject<WebPayment>(await response.Content.ReadAsStringAsync());
				return result;
			}
			catch (ArgumentException)
			{
				throw new ArgumentException();
			}
			catch (Exception ex)
			{
				throw new Exceptions.WebException("Create failed", ex);
			}
		}
	}
}
