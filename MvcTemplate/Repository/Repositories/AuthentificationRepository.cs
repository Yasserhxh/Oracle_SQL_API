using Domain.Authentication;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Data;
using Repository.IRepositories;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AuthentificationRepository : IAuthentificationRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbOracle;
        private readonly SqlDbContext _dbSQL;
        private readonly IUnitOfWork unitOfWork;
        private readonly ISqlUnitOfWork SqlunitOfWork;

        public AuthentificationRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            RoleManager<IdentityRole> roleManager, ApplicationDbContext dbOracle, IUnitOfWork unitOfWork, SqlDbContext dbSQL, ISqlUnitOfWork SqlunitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbOracle = dbOracle;
            _dbSQL = dbSQL;
            this.unitOfWork = unitOfWork;
            this.SqlunitOfWork = SqlunitOfWork;

        }

        public IEnumerable<COMPTEUR_H> getCompteurs(string CodeCentre, string NumInstallation)
        {
            var res = _dbOracle.compteurs_h.Where(p=>p.NUM_INST == NumInstallation && p.CODCT == CodeCentre).AsEnumerable();
            return res;
        }
        public IEnumerable<RELEVE_EAU> getReleves()
        {
            var res = _dbOracle.releves_eau.Take(20).AsEnumerable();
            return res;
        } 
        public IEnumerable<ReleveViewModel> getRelevesSQL(string AgentName)
        {
            var res = _dbSQL.releves_eau.Where(p=>p.CREE_PAR == AgentName && p.DATE_REL.Month == DateTime.Now.Month).AsEnumerable();
            var rels = new List<ReleveViewModel>();
            foreach (var item in res)
            {
                var rel = new ReleveViewModel()
                {
                    libelle = item.LIB_CPT,
                    index = item.IDX.ToString(),
                    installation = item.INST_CPT,
                    Nominstallation = _dbOracle.installations.Where(p => p.NUM_INST == item.INST_CPT).FirstOrDefault().LIB_INST,
                    numCompteur = item.NUM_CTR,
                    centre = item.CODCT,
                    Nomcentre = _dbOracle.centres.Where(p => p.CODCT == item.CODCT).FirstOrDefault().LIBCT,
                    etat_rel = item.STATUT_REL,
                    estimation = (item.ESTIM == null ? 0 : item.ESTIM),
                    date_Rel = item.DATE_REL.ToString(),
                    type_saisie = item.TYPE_SAISIE,
                    coherence = (string.IsNullOrEmpty(item.COHERENCE) ? "" : item.COHERENCE),
                    statut_rel = item.STATUT_REL,
                    utilisateur = item.CREE_PAR,
                    volume = (int)item.VOLUME,
                    motif = (string.IsNullOrEmpty(item.MOTIF) ? "" : item.MOTIF),
                    imageJson = item.IMG_REL,
                    date_crea = item.DATECREA.ToString(),   

                };
                rels.Add(rel);
            }
            return rels;
        }
        public IEnumerable<ReleveViewModel> getRelevesChefCentreSQL(string CodeCentre)
        {
            var res = _dbSQL.releves_eau.Where(p=>p.CODCT == CodeCentre && p.DATE_REL.Month == DateTime.Now.Month).AsEnumerable();
            var rels = new List<ReleveViewModel>();
            foreach (var item in res)
            {
                var rel = new ReleveViewModel()
                {
                    libelle = item.LIB_CPT,
                    index = item.IDX.ToString(),
                    installation = item.INST_CPT,
                    Nominstallation = _dbOracle.installations.Where(p => p.NUM_INST == item.INST_CPT).FirstOrDefault().LIB_INST,
                    numCompteur = item.NUM_CTR,
                    centre = item.CODCT,
                    Nomcentre = _dbOracle.centres.Where(p => p.CODCT == item.CODCT).FirstOrDefault().LIBCT,
                    etat_rel = item.STATUT_REL,
                    estimation = (item.ESTIM == null ? 0 : item.ESTIM),
                    date_Rel = item.DATE_REL.ToString(),
                    type_saisie = item.TYPE_SAISIE,
                    coherence = (string.IsNullOrEmpty(item.COHERENCE) ? "" : item.COHERENCE),
                    statut_rel = item.STATUT_REL,
                    utilisateur = item.CREE_PAR,
                    volume = (int)item.VOLUME,
                    motif = (string.IsNullOrEmpty(item.MOTIF) ? "" : item.MOTIF),
                    imageJson = item.IMG_REL,
                    date_crea = item.DATECREA.ToString(),

                };
                rels.Add(rel);
            }
            return rels;
        }
        public ReleveViewModel getReleveByCompteur(string CodeCompteur, string installation, string CodeCentre)
        {
            var res = _dbSQL.releves_eau.Where(p => p.CODCT == CodeCentre && p.DATE_REL.Month == DateTime.Now.Month && p.INST_CPT == installation && p.NUM_CTR == CodeCompteur).FirstOrDefault();
            if (res == null)
            {
                return null;
            }
            else
            {
                var item = new ReleveViewModel()
                {
                    libelle = res.LIB_CPT,
                    index = res.IDX.ToString(),
                    installation = res.INST_CPT,
                    Nominstallation = _dbOracle.installations.Where(p => p.NUM_INST == res.INST_CPT).FirstOrDefault().LIB_INST,
                    numCompteur = res.NUM_CTR,
                    centre = res.CODCT,
                    Nomcentre = _dbOracle.centres.Where(p => p.CODCT == res.CODCT).FirstOrDefault().LIBCT,
                    etat_rel = res.STATUT_REL,
                    estimation = (res.ESTIM == null ? 0 : res.ESTIM),
                    date_Rel = res.DATE_REL.ToString(),
                    type_saisie = "",
                    coherence = (string.IsNullOrEmpty(res.COHERENCE) ? "" : res.COHERENCE),
                    statut_rel = res.STATUT_REL,
                    utilisateur = res.CREE_PAR,
                    volume = (int)res.VOLUME,
                    motif = (string.IsNullOrEmpty(res.MOTIF) ? "" : res.MOTIF),
                    imageJson = ""
                };
                return item;
            }
        }
        public IEnumerable<ReleveViewModel> showHist(string numCtr)
        {
            var query = _dbOracle.releves_eau.Where(p=>p.NUM_CTR == numCtr && p.DATE_REL.Year == DateTime.Now.Year).Take(12).OrderByDescending(p => p.DATE_REL).AsEnumerable();
            var rels = new List<ReleveViewModel>();
            foreach(var item in query)
            {
                var rel = new ReleveViewModel()
                {
                    libelle = item.LIB_CPT,
                    index = item.IDX.ToString(),
                    installation = item.INST_CPT,
                    Nominstallation = _dbOracle.installations.Where(p => p.NUM_INST == item.INST_CPT).FirstOrDefault().LIB_INST,
                    numCompteur = item.NUM_CTR,
                    centre = item.CODCT,
                    Nomcentre = _dbOracle.centres.Where(p => p.CODCT == item.CODCT).FirstOrDefault().LIBCT,
                    etat_rel = item.STATUT_REL,
                    estimation = (item.ESTIM == null ? 0 : item.ESTIM),
                    date_Rel = item.DATE_REL.ToString(),
                    type_saisie = item.TYPE_SAISIE,
                    coherence = (string.IsNullOrEmpty(item.COHERENCE) ? "" : item.COHERENCE),
                    statut_rel = item.STATUT_REL,
                    utilisateur = item.CREE_PAR,
                    volume = (int)item.VOLUME,
                    motif = (string.IsNullOrEmpty(item.MOTIF) ? "" : item.MOTIF),
                    imageJson = "",
                    date_crea = item.DATECREA.ToString(),

                };
                rels.Add(rel);
            }
            return rels;
        }
        public decimal findLastIndex(string compteurID)
        {
            var res = _dbSQL.releves_eau.Where(p=>p.NUM_CTR == compteurID).AsEnumerable().OrderByDescending(p=>p.DATE_REL).FirstOrDefault().IDX;
            return res;
        }
        public IEnumerable<int> findLastYearIndex(string compteurID)
        {
            var resQ = _dbSQL.releves_eau.Where(p => p.NUM_CTR == compteurID).AsEnumerable().OrderByDescending(p => p.DATE_REL);//.Take(12);
            var res = resQ.ToList();
        /*    var bt = Enumerable.Range(0, res.FirstOrDefault().IMG.Length)
                      .Where(x => x % 2 == 0)
                      .Select(x => Convert.ToByte(res.FirstOrDefault().IMG.Substring(x, 2), 16))
                      .ToArray();
            var str = Encoding.UTF8.GetString(bt);*/

            return resQ.Select(p => p.IDX);
        }
        public RELEVE_EAU findFormulaireIndex(string date, string compteurID, string codeCentre)
        {
            var res = _dbSQL.releves_eau.Where(p => p.DATE_REL == Convert.ToDateTime(date) && p.NUM_CTR == compteurID && p.CODCT == codeCentre).FirstOrDefault();
            return res;
        }
        public async Task<ApplicationUser> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                    return user;
                else
                    return null;
            }
            //return user;
            return null;
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {
            var user = new ApplicationUser { UserName = registerModel.UserName, Email = registerModel.Email };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
                return true;
            else
                return false;
        }
        public async Task<bool> CreateReleve(RELEVE_EAU releve)
        {
            await _dbSQL.releves_eau.AddAsync(releve);
            var confirm = await SqlunitOfWork.Complete();
            if (confirm > 0)
                return true;
            else
                return false;
            
        }
        public CompteurViewModel checkCompteur(string compteurID)
        {
            var compteur = _dbOracle.compteurs_h.Where(p=>p.NUM_CTR == compteurID).FirstOrDefault();
            if(compteur != null)
            {
                //var res = _db.releves_eau.Where(p => p.NUM_CTR == compteurID).AsEnumerable().OrderByDescending(p => p.DATE_REL).FirstOrDefault().IDX;

                var compteurView = new CompteurViewModel()
                {
                    Code_Centre = compteur.CODCT,
                    Installation = compteur.NUM_INST,
                    Libelle = compteur.LIB_CTR,
                    Code_Compteur = compteurID,
                   // Index = res
                };
                return compteurView;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> ValidateRel(ReleveViewModel releveViewModel)
        {
            DateTime newDate = Convert.ToDateTime(releveViewModel.date_Rel);
            var compteur1 = _dbSQL.releves_eau.Where(p=>p.NUM_CTR == releveViewModel.numCompteur && p.CODCT == releveViewModel.centre/* && p.STATUT_REL == "En attente" */ ).AsEnumerable();
            var compteur = compteur1.Where(p => p.DATE_REL.Date == newDate.Date).FirstOrDefault();
            if(compteur != null)
            {

                //var res = _db.releves_eau.Where(p => p.NUM_CTR == compteurID).AsEnumerable().OrderByDescending(p => p.DATE_REL).FirstOrDefault().IDX;
                compteur.STATUT_REL = releveViewModel.etat_rel ;
                compteur.COHERENCE = releveViewModel.coherence;
                compteur.ESTIM = releveViewModel.estimation;
                compteur.VOLUME = releveViewModel.volume;
                compteur.TOTAL = releveViewModel.estimation + releveViewModel.volume;
                compteur.MOTIF = releveViewModel.motif;
                _dbSQL.Entry(compteur).State = EntityState.Modified;
                 var confirm = await SqlunitOfWork.Complete();
                if (confirm > 0)
                    if (compteur.STATUT_REL != "Validé")
                        return await insertOracleRelve(compteur);
                    else
                        return true;
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<bool> insertOracleRelve(RELEVE_EAU compteur)
        {
            await _dbOracle.releves_eau.AddAsync(compteur);
            var confirm = await unitOfWork.Complete();
            if(confirm>0)
                return true;
            else
                return false;
            
        }
        public CentreViewModel getCentre(string userEmail)
        {
            var centreID = _dbOracle.utilisateurs.Where(p => p.MAILOFFICE == userEmail).FirstOrDefault().CODECENTRE;
            var centre = _dbOracle.centres.Where(p => p.CODCT == centreID).FirstOrDefault();//.Take(12);
            var centreView = new CentreViewModel()
            {
                CentreId = centre.CODCT,
                CentreName = centre.LIBCT
            } ;
            return centreView;
        }
        public IEnumerable<KeyValuePair<string,string>> getInstallation(string CodeCentre)
        {
            var res = _dbOracle.installations.Where(p => p.CODCT == CodeCentre).Select(p => new KeyValuePair<string, string> ( p.NUM_INST, p.LIB_INST ))
                .AsEnumerable();
            return res;
        }
    }
}
