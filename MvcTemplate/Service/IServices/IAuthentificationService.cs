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
        IEnumerable<COMPTEUR_HModel> getCompteurs();

    }
}
