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
        public IEnumerable<RELEVE_EAU> showHist(string etat, string search)
        {
            var query = _db.releves_eau.AsEnumerable();
            /*if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.LIB_CPT == search);
            if (!string.IsNullOrEmpty(etat))
                query = query.Where(p => p.STATUT_REL == etat);*/
            return query.AsEnumerable().Take(50);
        }
        public decimal findLastIndex(string compteurID)
        {
            var res = _db.releves_eau.Where(p=>p.NUM_CTR == compteurID).AsEnumerable().OrderByDescending(p=>p.DATE_REL).FirstOrDefault().IDX;
            return res;
        }
        public IEnumerable<int> findLastYearIndex(string compteurID)
        {
            var res = _db.releves_eau.Where(p=>p.NUM_CTR == compteurID).AsEnumerable().OrderByDescending(p=>p.DATE_REL).Take(12).Select(p=>p.IDX);
            return res;
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
                var res = _db.releves_eau.Where(p => p.NUM_CTR == compteurID).AsEnumerable().OrderByDescending(p => p.DATE_REL).FirstOrDefault().IDX;

                var compteurView = new CompteurViewModel()
                {
                    Code_Centre = compteur.CODCT,
                    Installation = compteur.NUM_INST,
                    Libelle = compteur.LIB_CTR,
                    Code_Compteur = compteurID,
                    Index = res
                };
                return compteurView;
            }
            else
            {
                return null;
            }
        }
    }
}
