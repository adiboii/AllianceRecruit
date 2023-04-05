using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using System.Linq;

namespace BaseCode.Data.Contracts
{
    public interface IPersonalInformationRepository
    {
        void Create(PersonalInformation personalInfo);
        void Update(PersonalInformation personalInfo);
        void Delete(PersonalInformation personalInfo);
        bool PersonalInformationExists(PersonalInformation personalInfo);
        PersonalInformation FindPersonalInformation(int Id);
        IQueryable<PersonalInformation> RetrieveAll();
        ListViewModel FindPersonalInformations(PersonalInformationSearchViewModel searchModel);
    }
}
