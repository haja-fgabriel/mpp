using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CSharpRestClient
{
	class MainClass
	{
		static HttpClient client = new HttpClient();

		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			RunAsync().Wait();
		}


		static async Task RunAsync()
		{
			client.BaseAddress = new Uri("http://localhost:8080/chat/greeting");
			client.DefaultRequestHeaders.Accept.Clear();
			//client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			// Get the string
			//String text = await GetTextAsync("http://localhost:8080/chat/greeting");
			//Console.WriteLine("am obtinut {0}", text);
			//Get one user
				String username = "ana";
				Console.WriteLine("Get user {0}", username);
				User result = await GetUserAsync("http://localhost:8080/chat/users/"+username);
				Console.WriteLine("Am primit {0}", result);
				Console.ReadLine();
		}

		static async Task<String> GetTextAsync(string path)
		{
			String product = null;
			HttpResponseMessage response = await client.GetAsync(path);
			if (response.IsSuccessStatusCode)
			{
				product = await response.Content.ReadAsStringAsync();
			}
			return product;
		}


		static async Task<User> GetUserAsync(string path)
		{
			User product = null;
			HttpResponseMessage response = await client.GetAsync(path);
			if (response.IsSuccessStatusCode)
			{
				product = await response.Content.ReadAsAsync<User>();
			}
			return product;
		}

	}
	public class User
	{
		public string Passwd { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public User[] Friends { get; set; } 

		public override string ToString()
		{
			return string.Format("[User: Passwd={0}, Id={1}, Name={2}, Friends={3}]", Passwd, Id, Name, Friends);
		}
	}
	
}
