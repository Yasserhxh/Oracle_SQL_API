using Domain.Authentication;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IAuthentificationRepository
    {
        Task<bool> Register(RegisterModel userModel);
        Task<ApplicationUser> Login(LoginModel loginModel);
        IEnumerable<COMPTEUR_H> getCompteurs();
        CompteurViewModel checkCompteur(string compteurID);
        IEnumerable<RELEVE_EAU> getReleves();
        decimal findLastIndex(string compteurID);
        Task<bool> CreateReleve(RELEVE_EAU releve);
        IEnumerable<RELEVE_EAU> showHist(string etat, string search);
        IEnumerable<int> findLastYearIndex(string compteurID);
        RELEVE_EAU findFormulaireIndex(string date, string compteurID, string codeCentre);
    }
}
