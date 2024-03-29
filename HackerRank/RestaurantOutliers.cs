using Newtonsoft.Json;

namespace HackerRank;

/// <summary>
/// solution by Srikant - his was not accepted and object's fields have wrong names...
/// </summary>
public class RestaurantOutliers
{
    public class FoodOutlet
    {
        public string City { get; set; }
        public string Name { get; set; }
        public int Estimated_Cost { get; set; }
    }

    public class FoodAPIResponse
    {
        public int Page { get; set; }
        public int Per_Page { get; set; }
        public int Total { get; set; }
        public int Total_Pages { get; set; }
        public List<FoodOutlet> Data { get; set; }
    }

    private const string BaseUrl = "https://jsonmock.hackerrank.com/api/food_outlets?city={0}&page={1}";

    public static List<string> getRelevantFoodOutlets(string city, int maxCost)
    {
        List<string> relavantOutlets = new List<string>();
        int pageNumber = 1;
        bool hasNextPage = true;

        while (hasNextPage)
        {
            string url = string.Format(BaseUrl, city, pageNumber);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;
                    FoodAPIResponse apiResponse = JsonConvert
                        .DeserializeObject<FoodAPIResponse>(jsonResponse);

                    foreach (FoodOutlet outLet in apiResponse.Data)
                    {
                        if (outLet.City == city && outLet.Estimated_Cost <= maxCost)
                            relavantOutlets.Add(outLet.Name);
                    }

                    if (pageNumber < apiResponse.Total_Pages)
                        pageNumber++;
                    else
                        hasNextPage = false;
                }
                else
                {
                    throw new Exception("Failed to fetch data from API");
                }
            }
        }
        return relavantOutlets;
    }
}