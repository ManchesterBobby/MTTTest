using MTTTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TestApi
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            RunApiTest();
        }

        static async void RunApiTest()
        {
            client.BaseAddress = new Uri("https://localhost:44312/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // The featured products
            var list = new List<int> {1, 2, 3 };
            Console.WriteLine("Featured Products");

            HttpResponseMessage response = await client.GetAsync($"api/products/{list}");
            if (response.IsSuccessStatusCode)
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());

                if (products != null)
                {
                    foreach(var prod in products)
                    {
                        ShowProduct(prod);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }

            // Products for a specific category
            HttpResponseMessage response2 = await client.GetAsync($"api/productsbycategory/1");
            Console.WriteLine("Products for Category");

            if (response.IsSuccessStatusCode)
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(await response2.Content.ReadAsStringAsync());

                if (products != null)
                {
                    foreach (var prod in products)
                    {
                        ShowProduct(prod);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }

            // Available categories
            HttpResponseMessage response3 = await client.GetAsync($"api/categories");
            Console.WriteLine("Available Categories");

            if (response.IsSuccessStatusCode)
            {
                var categories = JsonConvert.DeserializeObject<List<Category>>(await response3.Content.ReadAsStringAsync());

                if (categories != null)
                {
                    foreach (var cat in categories)
                    {
                        ShowCategory(cat);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }

        }

        static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.Name}\tPrice: " +
                $"{product.Price}\tCategory: {product.Category}");
        }

        static void ShowCategory(Category category)
        {
            Console.WriteLine($"ID: {category.ID}\tName: {category.Name}");

        }
    }
}
