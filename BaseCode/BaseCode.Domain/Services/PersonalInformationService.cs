using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.Repositories;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCode.Domain.Services
{
    public class PersonalInformationService : IPersonalInformationService
    {
        private readonly IPersonalInformationRepository _personalInfoRepository;

        public PersonalInformationService(IPersonalInformationRepository personalInfoRepository)
        {
            _personalInfoRepository = personalInfoRepository;
        }

        public void Create(PersonalInformation personalInfo)
        {
            _personalInfoRepository.Create(personalInfo);
        }
        public void Update(PersonalInformation personalInfo)
        {
            _personalInfoRepository.Update(personalInfo);
        }

        public void Delete(PersonalInformation personalInfo)
        {
            _personalInfoRepository.Delete(personalInfo);
        }

        public PersonalInformation FindPersonalInformation(int Id)
        {
            return _personalInfoRepository.FindPersonalInformation(Id);
        }

        public ListViewModel FindPersonalInformations(PersonalInformationSearchViewModel searchModel)
        {
            return _personalInfoRepository.FindPersonalInformations(searchModel);
        }

        public bool PersonalInformationExists(PersonalInformation personalInfo)
        {
            return _personalInfoRepository.PersonalInformationExists(personalInfo);
        }
    }
}