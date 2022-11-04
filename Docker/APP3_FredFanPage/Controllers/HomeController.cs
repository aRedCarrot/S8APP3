using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using APP3_FredFanPage.Areas.Identity.Data;
using APP3_FredFanPage.Models;
using System.Text.Json;
using APP3_FredFanPage.Services;

namespace APP3_FredFanPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqliteConnection _dbConnection;

        private readonly ILoggerClient _logger;

        private readonly UserManager<FredFanPageUser> _userManager;

        public HomeController(ILoggerClient logger, UserManager<FredFanPageUser> userManager, IConfiguration configuration)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._dbConnection = new SqliteConnection(configuration.GetConnectionString("FredFanPageContextConnection"));
        }

        public IActionResult Index()
        {
            this.ViewData["Message"] = "La page officiel des FAN de Fred <3";
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Comments()
        {
            _logger.LogInformation("Comments page");

            var comments = new List<string>();

            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.View(comments);
            }

            var cmd = new SqliteCommand($"Select Comment from Comments where UserId ='{user.Id}'", this._dbConnection);
            
            
            try
            { 
                this._dbConnection.Open();
                var rd = await cmd.ExecuteReaderAsync();

                while (rd.Read())
                {
                    comments.Add(rd.GetString(0));
                }

                rd.Close();
                this._dbConnection.Close();
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }

            return this.View(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Comments(string comment)
        {
            _logger.LogInformation("New comment");

            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                throw new InvalidOperationException("Vous devez vous connecter");
            }

            var cmd = new SqliteCommand(
                $"insert into Comments (UserId, CommentId, Comment) Values ('{user.Id}','{Guid.NewGuid()}','" + comment + "')",
                this._dbConnection);
            try 
            { 
                this._dbConnection.Open();
                await cmd.ExecuteNonQueryAsync();
        }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }

            return this.Ok("Commentaire ajouté");
        }

        public async Task<IActionResult> Search(string searchData)
        {
            _logger.LogInformation("New search");

            var searchResults = new List<string>();

            //var user = await this._userManager.GetUserAsync(this.User);
            if (string.IsNullOrEmpty(searchData))
            {
                return this.View(searchResults);
            }

            var cmd = new SqliteCommand($"Select Comment from Comments where Comment like '%{searchData}%'", this._dbConnection);

            try
            {

                this._dbConnection.Open();
                var rd = await cmd.ExecuteReaderAsync();
                while (rd.Read())
                {
                    searchResults.Add(rd.GetString(0));
                }

                rd.Close();
                this._dbConnection.Close();
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }

            return Ok(JsonSerializer.Serialize(searchResults));
        }

        public IActionResult About()
        {
            _logger.LogInformation("About page");

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}