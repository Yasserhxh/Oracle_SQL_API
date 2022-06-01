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
        [HttpPost]
        [Route("getCompteurs")]
        public JsonResult getCompteurs([FromBody] CompteurViewModel model)
        {
            var res = authentificationService.getCompteurs(model.Code_Centre, model.Installation);
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
        [Route("getRelevesAgent/{agent}")]
        public JsonResult getRelevesAgent(string agent)
        {
            var res = authentificationRepository.getRelevesSQL(agent);
            return new JsonResult(res);
        }
        [HttpGet]
        [Route("getRelevesChefCentre/{CodeCentre}")]
        public JsonResult getRelevesChefCentre(string CodeCentre)
        {
            var res = authentificationRepository.getRelevesChefCentreSQL(CodeCentre);
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
        public async Task<JsonResult> insertReleveAsync([FromBody] ReleveViewModel releveView)
        {
            var releve = new RELEVE_EAUModel()
            {
                CODCT = releveView.centre,
                INST_CPT = releveView.installation,
                LIB_CPT = releveView.libelle,
                NUM_CTR = releveView.numCompteur,
                IDX = Convert.ToInt32(releveView.index),
                ESTIM = releveView.estimation,
                VOLUME = releveView.volume,
                STATUT_REL = "En attente",
                DATE_REL = DateTime.Now,
                DATECREA = DateTime.Now,
                CREE_PAR = releveView.utilisateur,
                TYPE_SAISIE = releveView.type_saisie,
                //PICTURE = releveView.imageJson,
            };
            //byte[] bytes = Encoding.UTF8.GetBytes(releveView.imageJson);
            //releve.IMG = bytes;
            //releve.IMG = releveView.imageJson;
            releve.TOTAL = releve.VOLUME + releve.ESTIM;
            var res = await authentificationService.CreateReleve(releve);
            return new JsonResult(res);
        }
        [HttpGet]
        [Route("showHist/{numCtr}")]
        public JsonResult showHist(string numCtr)
        {
            var res = authentificationRepository.showHist(numCtr);
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
        [HttpPost]
        [Route("ValidateRel")]
        public async Task<JsonResult> ValidateRel([FromBody] ReleveViewModel releveView)
        {
            if (releveView.etat_rel == "Validé")
                releveView.coherence = "1";
            else
                releveView.coherence = "0";

            var res = await authentificationService.ValidateRel(releveView);
            return new JsonResult(res);
        }
        [HttpGet]
        [HttpGet("getCentre/{userEmail}", Name = "getCentre")]
        public JsonResult getCentre(string userEmail)
        {
            var res = authentificationRepository.getCentre(userEmail);
            return new JsonResult(res);
        }
        [HttpGet]
        [HttpGet("getInstallation/{codecentre}", Name = "codecentre")]
        public JsonResult getInstallation(string codecentre)
        {
            var res = authentificationRepository.getInstallation(codecentre);
            return new JsonResult(res);
        }
    }   
}
