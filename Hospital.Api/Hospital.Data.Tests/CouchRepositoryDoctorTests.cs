using Hospital.Data.Factories;
using Hospital.Data.IRepositories;
using Hospital.Model;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Hospital.Data.Tests
{
    public class CouchRepositoryDoctorTests
    {
        private readonly IRepository<Doctor> _doctorsRepo;
        public CouchRepositoryDoctorTests()
        {
            _doctorsRepo = new Repository<Doctor>(new TestCouchConnectionFactory());
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
            bool result = await _doctorsRepo.DeleteAsync(returnedDoc._id);
            Assert.True(result);
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
            bool result = await _doctorsRepo.DeleteAsync(returnedSameDoc._id);
            Assert.True(result);
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


            bool result = await _doctorsRepo.DeleteAsync(returnedSameDoc._id);
            Assert.True(result);
        }

        [Fact]
        public async Task DoctorRepository_UpdateDoctorWithPreviousRev_ShitHappens()
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
            var updatedOldDoc = await _doctorsRepo.UpdateAsync(oldDoc);

            //Assert.False(returnedDocRev == returnedSameDoc._rev);
            //Assert.True(returnedSameDoc.FirstName == newName);


            //bool result = await _doctorsRepo.DeleteAsync(returnedSameDoc._id);
            //Assert.True(result);
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
            bool result = await _doctorsRepo.DeleteAsync(docFromDb._id);
            Assert.True(result);
            Doctor deletedDocFromDb = await _doctorsRepo.GetByIdAsync(returnedDoc._id);
            Assert.Null(deletedDocFromDb);
        }

        [Fact]
        public async Task DoctorRepository_DeleteDoctorEntityThatDoesntExists_DeletesDoctorEntityAsync()
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
            bool result = await _doctorsRepo.DeleteAsync(docFromDb._id);
            Assert.True(result);
            result = await _doctorsRepo.DeleteAsync(docFromDb._id);
            Assert.False(result);
        }

    }
}
