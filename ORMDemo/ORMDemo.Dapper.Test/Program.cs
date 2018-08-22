using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ORMDemo.Dapper.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new MySqlConnection(@"server=127.0.0.1;database=ormdemo;uid=root;pwd=Open0001;SslMode=none;");

            var selectAllProductSQL = @"SELECT * FROM Product";
            var allProduct = connection.Query<Product>(selectAllProductSQL);

            var selectProductByCategoryIdSQL = @"SELECT * FROM Product WHERE CategoryId = @CategoryId";
            var productsByCategory = connection.Query<Product>(selectProductByCategoryIdSQL, new { CategoryId = 2 });

            var selectAllProductWithCategorySQL = @"select * from product p 
                inner join category c on c.Id = p.CategoryId
                Order by p.Id";
            var allProductWithCategory = connection.Query<Product, Category, Product>(selectAllProductWithCategorySQL, (prod, cg) => { prod.Category = cg; return prod; });

            Product newProd = new Product { Name = "HuaWei Mata 7", Price = 1899, Quantity = 8, CategoryId = 1};
            string insertProductSQL = @"INSERT INTO product 
                                (Name
                                ,Quantity
                                ,Price
                                ,CategoryId)
                            VALUES
                                (@Name
                                ,@Quantity
                                ,@Price
                                ,@CategoryId)";
            int newId = connection.Execute(insertProductSQL, newProd);

            Console.ReadKey();
        }
    }

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
