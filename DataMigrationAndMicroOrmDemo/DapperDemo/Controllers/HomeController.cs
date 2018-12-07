using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using DapperDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;

namespace DapperDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration config;

        public HomeController(IConfiguration config)
        {
            this.config = config;
        }

        public IActionResult Index()
        {
            return View(new IndexModel
            {
                Animals = GetAnimals()
            });
        }


        private IEnumerable<AnimalModel> GetAnimals()
        {
            using (var conn = new SqlConnection(config.GetConnectionString("DefaultConnection")))
            {
                conn.Open();

                var selectSql = "SELECT * FROM Animal";

                foreach (var animal in conn.Query<Animal>(selectSql))
                {
                    yield return new AnimalModel
                    {
                        Name = animal.Name,
                        Age = new DateTime(DateTime.Now.Subtract(animal.Birthday).Ticks).Year - 1
                    };
                }
            }
        }
    }
}