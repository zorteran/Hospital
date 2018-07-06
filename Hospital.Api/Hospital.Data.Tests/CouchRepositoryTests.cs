using Hospital.Data.Exceptions;
using Hospital.Data.IRepositories;
using Hospital.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Data.DbManagers;
using Xunit;

namespace Hospital.Data.Tests
{
    public class CouchRepositoryTests
    {
        private const string dbUrl = "http://127.0.0.1:5984";
        private const string DbName = "unit-tests";
        private readonly IDoctorRepository _doctorsRepo;
        private readonly IPatientRepository _patientsRepo;
        public CouchRepositoryTests()
        {
            _doctorsRepo = new DoctorCouchDbGenericCouchDbRepository(new CouchDbManager(dbUrl, DbName));
            _patientsRepo = new PatientCouchDbGenericCouchDbRepository(new CouchDbManager(dbUrl, DbName));
        }
        [Fact]
        public async Task DoctorRepository_AddDoctorEntity_AddsDoctorEntityAsyncAndDeletesIt()
        {
            var newDoc = new Doctor()
            {
                FirstName = "Karol",
                LastName = "Wielki",
                Professions = { "Chirurg", "Internista" }
            };
            var returnedDoc = await _doctorsRepo.InsertAsync(newDoc);
            Assert.False(String.IsNullOrWhiteSpace(returnedDoc._id));
            var docFromDb = await _doctorsRepo.GetByIdAsync(returnedDoc._id);
            Assert.True(returnedDoc._rev == docFromDb._rev && returnedDoc.FirstName == docFromDb.FirstName);
            await _doctorsRepo.DeleteAsync(returnedDoc);
        }

        [Fact]
        public async Task DoctorRepository_AddDoctorEntityWithTheSameId_UpdatesRevOfExistingDoctor()
        {
            var newDoc = new Doctor()
            {
                FirstName = "Karol",
                LastName = "Wielki",
                Professions = { "Chirurg", "Internista" }
            };
            var returnedDoc = await _doctorsRepo.InsertAsync(newDoc);
            var returnedDocRev = returnedDoc._rev;

            var returnedSameDoc = await _doctorsRepo.InsertAsync(returnedDoc);
            Assert.False(returnedDocRev == returnedSameDoc._rev);
            await _doctorsRepo.DeleteAsync(returnedSameDoc);
        }

        [Fact]
        public async Task DoctorRepository_UpdateDoctorNewName_UpdatesName()
        {
            string newName = "Jan";
            var newDoc = new Doctor()
            {
                FirstName = "Karol",
                LastName = "Wielki",
                Professions = { "Chirurg", "Internista" }
            };
            var returnedDoc = await _doctorsRepo.InsertAsync(newDoc);
            returnedDoc.FirstName = newName;
            var returnedDocRev = returnedDoc._rev;

            var returnedSameDoc = await _doctorsRepo.UpdateAsync(returnedDoc);
            Assert.False(returnedDocRev == returnedSameDoc._rev);
            Assert.True(returnedSameDoc.FirstName == newName);

            await _doctorsRepo.DeleteAsync(returnedSameDoc);
        }

        [Fact]
        public async Task DoctorRepository_UpdateDoctorWithPreviousRev_ThrowsExceptionConflict()
        {
            string newName = "Jan";
            var newDoc = new Doctor()
            {
                FirstName = "Karol",
                LastName = "Wielki",
                Professions = { "Chirurg", "Internista" }
            };
            var returnedDoc = await _doctorsRepo.InsertAsync(newDoc);

            var oldDoc = new Doctor(newDoc);

            returnedDoc.FirstName = newName;

            var updatedDoc = await _doctorsRepo.UpdateAsync(returnedDoc);

            oldDoc.FirstName = newName;
            await Assert.ThrowsAsync<CouchDbException>(() => _doctorsRepo.UpdateAsync(oldDoc));
        }

        [Fact]
        public async Task DoctorRepository_DeleteDoctorEntity_DeletesDoctorEntityAsync()
        {
            var newDoc = new Doctor()
            {
                FirstName = "Karol",
                LastName = "Wielki",
                Professions = { "Chirurg", "Internista" }
            };
            var returnedDoc = await _doctorsRepo.InsertAsync(newDoc);
            Assert.False(String.IsNullOrWhiteSpace(returnedDoc._id));
            var docFromDb = await _doctorsRepo.GetByIdAsync(returnedDoc._id);
            Assert.True(returnedDoc._rev == docFromDb._rev && returnedDoc.FirstName == docFromDb.FirstName);
            await _doctorsRepo.DeleteAsync(docFromDb);
            Doctor deletedDocFromDb = await _doctorsRepo.GetByIdAsync(returnedDoc._id);
            Assert.Null(deletedDocFromDb);
        }

        [Fact]
        public async Task DoctorRepository_DeleteDoctorEntityThatDoesntExists_ThrowsCouchDbExceptionNotFound()
        {
            var newDoc = new Doctor()
            {
                FirstName = "Karol",
                LastName = "Wielki",
                Professions = { "Chirurg", "Internista" }
            };
            var returnedDoc = await _doctorsRepo.InsertAsync(newDoc);
            Assert.False(String.IsNullOrWhiteSpace(returnedDoc._id));
            var docFromDb = await _doctorsRepo.GetByIdAsync(returnedDoc._id);
            Assert.True(returnedDoc._rev == docFromDb._rev && returnedDoc.FirstName == docFromDb.FirstName);
            await _doctorsRepo.DeleteAsync(docFromDb);
            await Assert.ThrowsAsync<CouchDbException>(() => _doctorsRepo.DeleteAsync(docFromDb));
        }

        [Fact]
        public async Task DoctorRepository_ListAllDoctors_ListsAllDoctorsAsync()
        {
            int length = 10;
            var doctors = await _doctorsRepo.ListAsync();
            var patients = await _doctorsRepo.ListAsync();
            for (int i = 0; i < length; i++)
            {
                await _doctorsRepo.InsertAsync(CreateDoctor());
                await _patientsRepo.InsertAsync(CreatePatient());
            }
            var doctorsAfterInsert = await _doctorsRepo.ListAsync();
            var patientsAfterInsert = await _doctorsRepo.ListAsync();
            Assert.True(doctors.Count() + length == doctorsAfterInsert.Count());
            Assert.True(patients.Count() + length == patientsAfterInsert.Count());
        }

        private static Doctor CreateDoctor()
        {
            return new Doctor()
            {
                FirstName = "Karol",
                LastName = "Wielki",
                Professions = { "Chirurg", "Internista" }
            };
        }

        private static Patient CreatePatient()
        {
            return new Patient()
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Address = { "Warszawa", "Kaliskiego 2" },
                NfzInsurance = true,
                NfzInsuranceValidDate = new DateTime().AddYears(1)
            };
        }
    }
}
