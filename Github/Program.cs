
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitHubExplorer
{
	public class Secrets{ public string Token{ get; set; }
    }

        internal static class Program
    {
        static readonly HttpClient httpClient = new HttpClient();

        const string myUserName = "TreeSloths";
        static string token;
        
        static Secrets LoadAndValidateSecrets(){
            Secrets secrets;
            if (!File.Exists("secrets.json")){
                secrets = new Secrets();
                File.WriteAllText("secrets.json", JsonSerializer.Serialize(secrets));
            }
 		else{
                secrets = JsonSerializer.Deserialize<Secrets>(File.ReadAllText("secrets.json"));
            }
		if (!string.IsNullOrEmpty(secrets?.Token)) return secrets;
            	throw new Exception($@"ERROR: Needs to define a token in file {Path.Combine(System.Environment 
                .CurrentDirectory, "secrets.json")}");
        }
	static string[] StringSplit(this string stringToSplit)
        {
            var splitString = stringToSplit.Split(",");
            
            return splitString;
        }
	static async Task GetHtmlFromWebsite(string userName){
            
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GitHubExplorer", "1.0"));

            try{
                var response = await httpClient.GetAsync(userName);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(response);
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException exception){
                Console.WriteLine(exception);
            }
            
            httpClient.Dispose();
        }
        
        static void Main(string[] args){

            token = LoadAndValidateSecrets().Token;
            httpClient.BaseAddress = new Uri("https://api.github.com/users/");

            var task = GetHtmlFromWebsite(myUserName);
            task.Wait();
        }
    }
}