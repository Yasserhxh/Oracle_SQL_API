using AutoMapper;
using Domain.Authentication;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Data;
using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly IMapper mapper;


        public AuthentificationService(IAuthentificationRepository authentificationRepository, IMapper mapper)
        {
            this.authentificationRepository = authentificationRepository;
            this.mapper = mapper;
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {

            return await this.authentificationRepository.Register(registerModel);
        }

        public async Task<bool> Login(LoginModel loginModel)
        {
            var user = await this.authentificationRepository.Login(loginModel);

            if (user == null)
                return false;

            return true;
        }

        public IEnumerable<COMPTEUR_HModel> getCompteurs()
        {
            return mapper.Map<IEnumerable<COMPTEUR_H>, IEnumerable<COMPTEUR_HModel>>(authentificationRepository.getCompteurs());

        }
    }
}
