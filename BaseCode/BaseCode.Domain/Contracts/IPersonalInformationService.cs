using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCode.Domain.Contracts
{
    public interface IPersonalInformationService
    {
        void Create(PersonalInformation personalInfo);
        void Update(PersonalInformation personalInfo);
        void Delete(PersonalInformation personalInfo);
        bool PersonalInformationExists(PersonalInformation personalInfo);
        PersonalInformation FindPersonalInformation(int Id);
        ListViewModel FindPersonalInformations(PersonalInformationSearchViewModel searchModel);

    }
}
