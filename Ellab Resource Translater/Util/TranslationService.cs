using Azure;
using Azure.AI.Translation.Text;
using Azure.Core;
using Ellab_Resource_Translater.Objects;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static Org.BouncyCastle.Math.EC.ECCurve;
using static System.Net.Mime.MediaTypeNames;

namespace Ellab_Resource_Translater.Util
{
    public class TranslationService(string creds, Uri uri, string region)
    {
        private readonly TextTranslationClient _client = new(new AzureKeyCredential(creds), uri, region);
        private readonly Uri _uri = uri;
        public int msWaitTime = 100;

        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task<List<(string source, string[] translation)>> TranslateTextAsync(string[] texts, string targetLanguage)
        {
            await _semaphore.WaitAsync();
            try
            {
                return await TranslateText(texts, targetLanguage);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task<List<(string source, string[] translation)>> TranslateText(string[] texts, string targetLanguage)
        {
            // Due to limit of Azure, you can only translate a set amount at a time.
            // It would surprise me if 10 lines hits the limit.
            List<(string source, string[] translation)> outputList = [];
            for (int i = 0; i < texts.Length; i += 10)
            {
                var smallTexts = texts.Skip(i).Take(10).ToArray();
                var response = await _client.TranslateAsync(targetLanguage: targetLanguage, content: smallTexts, sourceLanguage: "en");
                outputList.AddRange(response.Value
                    .Select((translation, index) => (smallTexts[index], translation.Translations.Select((x) => x.Text).ToArray())) // Pair source with translation
                    .ToList());

                // Waiting between each call to hopefully avoid being being denied due to DDoS security
                Task.Delay(msWaitTime).Wait();
            }
            return outputList;
        }

        public async Task<bool> CanReachAzure()
        {
            if(creds == null)
            {
                return false;
            }
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", creds);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region", region);

                var requestBody = "";

                using HttpContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(_uri + "/translate?api-version=3.0&to=fr", content);
                    return ((int)response.StatusCode) == 400;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to reach Azure: {ex.Message}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to reach Azure: {ex.Message}");
                return false;
            }
        }
    }

}
