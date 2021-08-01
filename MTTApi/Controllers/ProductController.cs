using Dapper;
using Microsoft.AspNetCore.Mvc;
using MTTApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MTTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/<ProductController>
        [HttpGet]

        public List<Product> Get(string strCategories)
        {
            List<string> featuredCategories = strCategories.Split(',').ToList();

            var featuredProducts = new List<Product> { };
            try
            {
                string dbConnection = @"data source=localhost\SQLEXPRESS;initial catalog=MMTShop;integrated security=True;";

                using (var con = new SqlConnection(dbConnection))
                {
                    con.Open();

                    var table = new DataTable();
                    table.Columns.Add("ID", typeof(int));

                    foreach (var cat in featuredCategories)
                    {
                        var row = table.NewRow();
                        row["ID"] = Convert.ToInt32(cat);
                        table.Rows.Add(row);
                    }

                    var result = con.Query<Product>("EXEC GetFeaturedProducts @List", new
                    {
                        list = table.AsTableValuedParameter("dbo.CategoryList")
                    });

                    featuredProducts = (List<Product>)result;

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                // log error
            }

            return featuredProducts;
        }

        [HttpGet("Categories")]
        public List<Category> GetCategories()
        {
            var availableCategories = new List<Category> { };

            try
            {
                string dbConnection = @"data source=localhost\SQLEXPRESS;initial catalog=MMTShop;integrated security=True;";

                using (var con = new SqlConnection(dbConnection))
                {
                    con.Open();

                    availableCategories = con.Query<Category>("GetCategories",
                        commandType: CommandType.StoredProcedure).ToList();

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                // log error
            }

            return availableCategories;
        }

        [HttpGet("ByCategory")]
        public List<Product> Get(int id)
        {
            var products = new List<Product> { };

            try
            {
                string dbConnection = @"data source=localhost\SQLEXPRESS;initial catalog=MMTShop;integrated security=True;";

                using (var con = new SqlConnection(dbConnection))
                {
                    con.Open();

                    products = con.Query<Product>("GetProductsByCategory", new { CategoryId = id },
                        commandType: CommandType.StoredProcedure).ToList();

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                // log error
            }


            return products;
        }

    }
}
