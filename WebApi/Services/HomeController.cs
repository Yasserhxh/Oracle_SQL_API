using Domain.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiONEE.Helpers;

namespace WebApiONEE.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IAuthentificationService authentificationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthentificationRepository authentificationRepository;

        public HomeController(IOptions<AppSettings> appSettings, IAuthentificationService authentificationService, UserManager<ApplicationUser> userManager, IAuthentificationRepository authentificationRepository)
        {
            _appSettings = appSettings.Value;
            this.authentificationService = authentificationService;
            _userManager = userManager;
            this.authentificationRepository = authentificationRepository;
        }
        // GET: api/authentification
        [HttpGet]
        [Authorize]
        public JsonResult Get()
        {
            string message = "Bonjour dans le service d'authentification";

            return new JsonResult(message);
        }

        // POST: api/authentification/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await this.authentificationService.Register(registerModel));
        }
        [HttpGet]
        [Route("testConnection")]
        public JsonResult TestConnection()
        {
            var res = this.authentificationRepository.getCompteurs();
            return new JsonResult(res);
        }
    }
}
