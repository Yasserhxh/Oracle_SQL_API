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
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork unitOfWork;

        public AuthentificationRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            this.unitOfWork = unitOfWork;

        }

        public IEnumerable<COMPTEUR_H> getCompteurs()
        {
            var res = _db.compteurs_h.Take(20).AsEnumerable();
            return res;
        }
        public IEnumerable<RELEVE_EAU> getReleves()
        {
            var res = _db.releves_eau.Take(20).AsEnumerable();
            return res;
        }
        public IEnumerable<ReleveViewModel> showHist()
        {
            var query = _db.releves_eau.OrderByDescending(p => p.DATE_REL).AsEnumerable().Take(50);
            var rels = new List<ReleveViewModel>();
            foreach(var item in query)
            {
                var rel = new ReleveViewModel()
                {
                    libelle = item.LIB_CPT,
                    index = item.IDX.ToString(),
                    installation = item.INST_CPT,
                    numCompteur = item.NUM_CTR,
                    centre = item.CODCT,
                    etat_rel = item.STATUT_REL,
                    estimation = item.ESTIM,
                };
                rels.Add(rel);
            }
            return rels;
        }
        public decimal findLastIndex(string compteurID)
        {
            var res = _db.releves_eau.Where(p=>p.NUM_CTR == compteurID).AsEnumerable().OrderByDescending(p=>p.DATE_REL).FirstOrDefault().IDX;
            return res;
        }
        public IEnumerable<int> findLastYearIndex(string compteurID)
        {
            var resQ = _db.releves_eau.Where(p => p.NUM_CTR == compteurID).AsEnumerable().OrderByDescending(p => p.DATE_REL);//.Take(12);
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
            var res = _db.releves_eau.Where(p => p.DATE_REL == Convert.ToDateTime(date) && p.NUM_CTR == compteurID && p.CODCT == codeCentre).FirstOrDefault();
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
            await _db.releves_eau.AddAsync(releve);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return true;
            else
                return false;
            
        }
        public CompteurViewModel checkCompteur(string compteurID)
        {
            var compteur = _db.compteurs_h.Where(p=>p.NUM_CTR == compteurID).FirstOrDefault();
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
            var compteur1 = _db.releves_eau.Where(p=>p.NUM_CTR == releveViewModel.numCompteur && p.CODCT == releveViewModel.centre && p.STATUT_REL == "En attente"  ).AsEnumerable();
            var compteur = compteur1.Where(p => p.DATE_REL.Date == newDate.Date).FirstOrDefault();
            if(compteur != null)
            {

                //var res = _db.releves_eau.Where(p => p.NUM_CTR == compteurID).AsEnumerable().OrderByDescending(p => p.DATE_REL).FirstOrDefault().IDX;
                compteur.STATUT_REL = releveViewModel.etat_rel ;
                compteur.COHERENCE = releveViewModel.coherence;
                compteur.MOTIF = releveViewModel.motif;
                _db.Entry(compteur).State = EntityState.Modified;
                 var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}
