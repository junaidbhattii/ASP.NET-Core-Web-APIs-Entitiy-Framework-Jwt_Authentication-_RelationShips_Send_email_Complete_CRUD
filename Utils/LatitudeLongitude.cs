using Newtonsoft.Json.Linq;

namespace JwtAuthentication_Relations_Authorization.Utils
{
    public class LatitudeLongitude
    {
        private readonly HttpClient _httpClient;
        public LatitudeLongitude(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(string Latitude, string Longitude)> GetCoordinatesFromAddress(string address)
        {
            string apiKey = "AIzaSyDRnWPFPLvEgKnTwxWOJDAIH8Yyek00cmM";
            string encodedAddress = Uri.EscapeDataString(address);
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={encodedAddress}&key={apiKey}";

            using (HttpResponseMessage response = await _httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(json);

                    string status = data["status"].ToString();
                    if (status == "OK")
                    {
                        JToken location = data["results"][0]["geometry"]["location"];
                        string latitude = location["lat"].ToString();
                        string longitude = location["lng"].ToString();
                        return (latitude, longitude);
                    }
                    else
                    {
                        throw new Exception("Failed to geocode address.");
                    }
                }
                else
                {
                    throw new HttpRequestException($"Failed to geocode address. Status code: {response.StatusCode}");
                }
            }
        }
    }
}
