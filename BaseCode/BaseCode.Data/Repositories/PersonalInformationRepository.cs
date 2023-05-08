using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BaseCode.Data.Repositories
{
    public class PersonalInformationRepository : BaseRepository, IPersonalInformationRepository
    {
        public PersonalInformationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(PersonalInformation personalInfo)
        {
            GetDbSet<PersonalInformation>().Add(personalInfo);
            UnitOfWork.SaveChanges();
        }

        public void Update(PersonalInformation personalInfo)
        {
            var personalInfoUpdate = FindPersonalInformation(personalInfo.Id);
            personalInfoUpdate.FirstName = personalInfo.FirstName;
            personalInfoUpdate.MiddleName = personalInfo.MiddleName;
            personalInfoUpdate.LastName = personalInfo.LastName;
            personalInfoUpdate.Sex = personalInfo.Sex;
            personalInfoUpdate.DateOfBirth = personalInfo.DateOfBirth;
            personalInfoUpdate.Country = personalInfo.Country;
            personalInfoUpdate.Province = personalInfo.Province;
            personalInfoUpdate.ZipCode = personalInfo.ZipCode;
            personalInfoUpdate.AddressLine1 = personalInfo.AddressLine1;
            personalInfoUpdate.AddressLine2 = personalInfo.AddressLine2;
            personalInfoUpdate.EmailAddress = personalInfo.EmailAddress;
            personalInfoUpdate.PhoneNumber = personalInfo.PhoneNumber;
            UnitOfWork.SaveChanges();
        }

        public void Delete(PersonalInformation personalInfo)
        {
            GetDbSet<PersonalInformation>().Remove(personalInfo);
            UnitOfWork.SaveChanges();
        }
    
        public PersonalInformation FindPersonalInformation(int Id)
        {
            return GetDbSet<PersonalInformation>().FirstOrDefault(x => x.Id == Id);
        }

        public bool PersonalInformationExists(PersonalInformation personalInfo)
        {
          return GetDbSet<PersonalInformation>().
                Any(x => x.FirstName == personalInfo.FirstName
                    && x.MiddleName == personalInfo.MiddleName
                    && x.LastName == personalInfo.LastName
                    && x.Sex == personalInfo.Sex
                    && x.DateOfBirth == personalInfo.DateOfBirth
                    && x.Country == personalInfo.Country
                    && x.Province == personalInfo.Province
                    && x.ZipCode == personalInfo.ZipCode
                    && x.AddressLine1 == personalInfo.AddressLine1
                    && x.AddressLine2 == personalInfo.AddressLine2
                    && x.EmailAddress == personalInfo.EmailAddress
                    && x.PhoneNumber == personalInfo.PhoneNumber);
        }

        public IQueryable<PersonalInformation> RetrieveAll()
        {
            return GetDbSet<PersonalInformation>();
        }

        public ListViewModel FindPersonalInformations(PersonalInformationSearchViewModel searchModel)
        {
            var personalInfos = RetrieveAll();

            if (searchModel.Page == 0) searchModel.Page = 1;

            var totalCount = personalInfos.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Constants.Subject.PageSize);

            var results = personalInfos.Skip(Constants.Subject.PageSize * (searchModel.Page - 1))
                .Take(Constants.Subject.PageSize)
                .AsEnumerable()
                .Select(personalInfo => new
                {
                    id = personalInfo.Id,
                    firstName = personalInfo.FirstName,
                    middleName = personalInfo.MiddleName,
                    lastName = personalInfo.LastName,
                    sex = personalInfo.Sex,
                    dateOfBirth = personalInfo.DateOfBirth,
                    country = personalInfo.Country,
                    province = personalInfo.Province,
                    zipCode = personalInfo.ZipCode,
                    addressLine1 = personalInfo.AddressLine1,
                    addressLine2 = personalInfo.AddressLine2,
                    emailAddress = personalInfo.EmailAddress,
                    phoneNumber = personalInfo.PhoneNumber,
                }).ToList();

            var pagination = new
            {
                pages = totalPages,
                size = totalCount
            };

            return new ListViewModel { Pagination = pagination, Data = results };
        }
    }
}
