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
    }
}
