using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IAuthentificationService
    {
        Task<bool> Register(RegisterModel userModel);
        Task<bool> Login(LoginModel loginModel);
        IEnumerable<COMPTEUR_HModel> getCompteurs(string CodeCentre, string NumInstallation);
        IEnumerable<RELEVE_EAUModel> getReleves();
        decimal findLastIndex(string compteurID);
        Task<bool> CreateReleve(RELEVE_EAUModel releveModel);
        Task<bool> ValidateRel(ReleveViewModel releveViewModel);
        CompteurViewModel checkCompteur(string compteurID);
        IEnumerable<ReleveViewModel> showHist();
        IEnumerable<int> findLastYearIndex(string compteurID);
        RELEVE_EAUModel findFormulaireIndex(string date, string compteurID, string codeCentre);
        CentreViewModel getCentre(string userEmail);
        IEnumerable<KeyValuePair<string, string>> getInstallation(string CodeCentre);
    }
}
