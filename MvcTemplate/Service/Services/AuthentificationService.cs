﻿using AutoMapper;
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
        private readonly IUnitOfWork unitOfWork;


        public AuthentificationService(IAuthentificationRepository authentificationRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.authentificationRepository = authentificationRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
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
        public IEnumerable<RELEVE_EAUModel> getReleves()
        {
            return mapper.Map<IEnumerable<RELEVE_EAU>, IEnumerable<RELEVE_EAUModel>>(authentificationRepository.getReleves());

        }

        public decimal findLastIndex(string compteurID)
        {
            return this.findLastIndex(compteurID);
        }

        public async Task<bool> CreateReleve(RELEVE_EAUModel releveModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    RELEVE_EAU releve = mapper.Map<RELEVE_EAUModel, RELEVE_EAU>(releveModel);
                    var idReleve = await authentificationRepository.CreateReleve(releve);
                    if (idReleve == false)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public CompteurViewModel checkCompteur(string compteurID)
        {
            return authentificationRepository.checkCompteur(compteurID);
        }

        public IEnumerable<RELEVE_EAUModel> showHist(string etat, string search)
        {
            return mapper.Map<IEnumerable<RELEVE_EAU>, IEnumerable<RELEVE_EAUModel>>(authentificationRepository.showHist(etat, search));
        }

        public IEnumerable<int> findLastYearIndex(string compteurID)
        {
            return authentificationRepository.findLastYearIndex(compteurID);
        }

        public RELEVE_EAUModel findFormulaireIndex(string date, string compteurID, string codeCentre)
        {
            return mapper.Map<RELEVE_EAU, RELEVE_EAUModel>(authentificationRepository.findFormulaireIndex(date, compteurID, codeCentre));
        }
    }
}