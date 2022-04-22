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
        [Route("getCompteurs")]
        public JsonResult getCompteurs()
        {
            var res = authentificationRepository.getCompteurs();
            return new JsonResult(res);
        }
        [HttpGet]
        [Route("getReleves")]
        public JsonResult getReleves()
        {
            var res = authentificationRepository.getReleves();
            return new JsonResult(res);
        }
        [HttpGet]
        [HttpGet("getCptData/{compteurID}", Name = "getCptData")]
        public JsonResult getCptData(string compteurID)
        {             
            var res = authentificationRepository.checkCompteur(compteurID);
            return new JsonResult(res);
        } 
        [HttpPost]
        [Route("insertReleve")]
        public JsonResult insertReleve([FromBody] ReleveViewModel releveView)
        {
            var releve = new RELEVE_EAUModel()
            {
                CODCT = releveView.centre,
                INST_CPT = releveView.installation,
                LIB_CPT = releveView.libelle,
                NUM_CTR = releveView.numCompteur,
                IDX = releveView.index,
                ESTIM = releveView.estimation,
                VOLUME = releveView.volume,
                IMG_REL = releveView.imageJson,
                STATUT_REL = "En attente",
                DATE_REL = DateTime.Now,
                DATECREA = DateTime.Now,
                
            };
            releve.TOTAL = releve.VOLUME + releve.ESTIM;
            var res = authentificationService.CreateReleve(releve);
            return new JsonResult(res);
        }
        [HttpGet]
        [Route("showHist")]
        public JsonResult showHist([FromBody] SearchModel model)
        {
            var res = authentificationRepository.showHist(model.etat, model.search);
            return new JsonResult(res);
        }
        [HttpGet]
        [HttpGet("findLastYearIndex/{compteurID}", Name = "findLastYearIndex")]
        public JsonResult findLastYearIndex(string compteurID)
        {
            var res = authentificationRepository.findLastYearIndex(compteurID);
            return new JsonResult(res);
        }
        /*[HttpGet]
        [Route("showHist")]
        public JsonResult findFormulaireIndex(string compteurID)
        {
            var res = authentificationRepository.findLastYearIndex(compteurID);
            return new JsonResult(res);
        }*/
    }   
}
