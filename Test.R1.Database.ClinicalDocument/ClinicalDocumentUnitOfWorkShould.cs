using System;
using Database.Abstraction.ClinicalDocument.Contract.Repository;
using Database.ClinicalDocument.DataAccess;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Test.R1.Database.ClinicalDocument
{
    public class ClinicalDocumentUnitOfWorkShould
    {
        /// <summary>
        /// Throws ArgumentNullException When IClinicalDocumentRepository Is Null
        /// </summary>
        [Fact]
        public void ThrowsArgumentNullExceptionWhenIClinicalDocumentRepositoryIsNull()
        {
            //Act
            var mockDbContext = new Mock<DbContext>();
            var mockIDocumentCrosswalkConfigurationRepository = new Mock<IDocumentTypeXWalkRepository>();
            var mockIDocumentCrosswalkRepository = new Mock<IDocumentTypeRepository>();

            //Assert
            Assert.Throws<ArgumentNullException>(() => new ClinicalDocumentUnitOfWork(null, mockIDocumentCrosswalkConfigurationRepository.Object, mockIDocumentCrosswalkRepository.Object, mockDbContext.Object));

        }


        [Fact]
        public async void SaveClinicalDocumentReturnNotNullResult()
        {
            //Act
            var mockIClinicalDocumentRepository = new Mock<IClinicalDocumentRepository>();
            var mockDbContext = new Mock<DbContext>();
            var mockIDocumentCrosswalkConfigurationRepository = new Mock<IDocumentTypeXWalkRepository>();
            var mockIDocumentCrosswalkRepository = new Mock<IDocumentTypeRepository>();
            //Arrange
            var mockClinicalDocumentUnitOfWork = new ClinicalDocumentUnitOfWork(mockIClinicalDocumentRepository.Object, mockIDocumentCrosswalkConfigurationRepository.Object, mockIDocumentCrosswalkRepository.Object, mockDbContext.Object);
            var result = await mockClinicalDocumentUnitOfWork.SaveClinicalDetails(TestConstants.clinicalDocuments);

            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public async void SaveClinicalDocumentReturnId()
        {
            //Act
            var mockIClinicalDocumentRepository = new Mock<IClinicalDocumentRepository>();
            var mockIDocumentCrosswalkConfigurationRepository = new Mock<IDocumentTypeXWalkRepository>();
            var mockIDocumentCrosswalkRepository = new Mock<IDocumentTypeRepository>();
            var mockDbContext = new Mock<DbContext>();

            //Arrange
            var mockClinicalDocumentUnitOfWork = new ClinicalDocumentUnitOfWork(mockIClinicalDocumentRepository.Object, mockIDocumentCrosswalkConfigurationRepository.Object, mockIDocumentCrosswalkRepository.Object, mockDbContext.Object);
            var result = await mockClinicalDocumentUnitOfWork.SaveClinicalDetails(TestConstants.clinicalDocuments);

            //Assert
            Assert.True(result.ToString().Length > 0);
        }


        /// <summary>
        /// Throw an Exception when IDocumentCrosswalkConfigurationRepository is null
        /// </summary>
        [Fact]
        public void ThrowsArgumentNullExceptionWhenIDocumentCrosswalkConfigurationRepositoryIsNull()
        {
            //Act
            var mockDbContext = new Mock<DbContext>();
            var mockIClinicalDocumentRepository = new Mock<IClinicalDocumentRepository>();
            var mockIDocumentCrosswalkRepository = new Mock<IDocumentTypeRepository>();

            //Assert
            Assert.Throws<ArgumentNullException>(() => new ClinicalDocumentUnitOfWork(mockIClinicalDocumentRepository.Object, null, mockIDocumentCrosswalkRepository.Object, mockDbContext.Object));

        }


        /// <summary>
        /// Throw an Exception when IDocumentCrosswalkRepository is null
        /// </summary>
        [Fact]
        public void ThrowsArgumentNullExceptionWhenIDocumentCrosswalkRepositoryIsNull()
        {
            //Act
            var mockDbContext = new Mock<DbContext>();
            var mockIClinicalDocumentRepository = new Mock<IClinicalDocumentRepository>();
            var mockIDocumentCrosswalkConfigurationRepository = new Mock<IDocumentTypeXWalkRepository>();

            //Assert
            Assert.Throws<ArgumentNullException>(() => new ClinicalDocumentUnitOfWork(mockIClinicalDocumentRepository.Object, mockIDocumentCrosswalkConfigurationRepository.Object, null, mockDbContext.Object));

        }

        /// <summary>
        /// test for clinical document null result
        /// </summary>
        [Fact]
        public async void GetClinicalDocumentReturnNullResult()
        {
            //Act
            var mockIClinicalDocumentRepository = new Mock<IClinicalDocumentRepository>();
            var mockDbContext = new Mock<DbContext>();
            var mockIDocumentCrosswalkConfigurationRepository = new Mock<IDocumentTypeXWalkRepository>();
            var mockIDocumentCrosswalkRepository = new Mock<IDocumentTypeRepository>();
            mockIClinicalDocumentRepository.Setup(m => m.GetClinicalDocumentDetails(TestConstants.FaclityCodeSJPR, TestConstants.AccountNumberForNullRecord)).ReturnsAsync(TestConstants.ListOfClinicalDocumentsNull);

            //Arrange
            var ClinicalDocumentUnitOfWork = new ClinicalDocumentUnitOfWork(mockIClinicalDocumentRepository.Object, mockIDocumentCrosswalkConfigurationRepository.Object, mockIDocumentCrosswalkRepository.Object, mockDbContext.Object);
            var result = await ClinicalDocumentUnitOfWork.GetClinicalDocumentDetails(TestConstants.FaclityCodeSJPR, TestConstants.AccountNumberForNullRecord);


            //Assert
            Assert.Null(result);
        }

        /// <summary>
        /// test for clinical document empty records
        /// </summary>
        [Fact]
        public async void GetClinicalDocumentReturnNotNullEmptyRecords()
        {
            //Act
            var mockIClinicalDocumentRepository = new Mock<IClinicalDocumentRepository>();
            var mockDbContext = new Mock<DbContext>();
            var mockIDocumentCrosswalkConfigurationRepository = new Mock<IDocumentTypeXWalkRepository>();
            var mockIDocumentCrosswalkRepository = new Mock<IDocumentTypeRepository>();
            mockIClinicalDocumentRepository.Setup(m => m.GetClinicalDocumentDetails(TestConstants.FaclityCodeSJPK, TestConstants.AccountNumberForEmptyRecord)).ReturnsAsync(TestConstants.ListOfClinicalDocumentsEmptyRecords);

            //Arrange
            var ClinicalDocumentUnitOfWork = new ClinicalDocumentUnitOfWork(mockIClinicalDocumentRepository.Object, mockIDocumentCrosswalkConfigurationRepository.Object, mockIDocumentCrosswalkRepository.Object, mockDbContext.Object);
            var result = await ClinicalDocumentUnitOfWork.GetClinicalDocumentDetails(TestConstants.FaclityCodeSJPK, TestConstants.AccountNumberForEmptyRecord);


            //Assert
            Assert.Empty(result);
        }

        /// <summary>
        /// test for clinical document with records
        /// </summary>
        [Fact]
        public async void GetClinicalDocumentReturnWithListOfRecords()
        {
            //Act
            var mockIClinicalDocumentRepository = new Mock<IClinicalDocumentRepository>();
            var mockDbContext = new Mock<DbContext>();
            var mockIDocumentCrosswalkConfigurationRepository = new Mock<IDocumentTypeXWalkRepository>();
            var mockIDocumentCrosswalkRepository = new Mock<IDocumentTypeRepository>();
            mockIClinicalDocumentRepository.Setup(m => m.GetClinicalDocumentDetails(TestConstants.FaclityCodeBOMC, TestConstants.AccountNumberWithRecord)).ReturnsAsync(TestConstants.ListOfClinicalDocumentsWithRecords);

            //Arrange
            var ClinicalDocumentUnitOfWork = new ClinicalDocumentUnitOfWork(mockIClinicalDocumentRepository.Object, mockIDocumentCrosswalkConfigurationRepository.Object, mockIDocumentCrosswalkRepository.Object, mockDbContext.Object);
            var result = await ClinicalDocumentUnitOfWork.GetClinicalDocumentDetails(TestConstants.FaclityCodeBOMC, TestConstants.AccountNumberWithRecord);


            //Assert
            Assert.NotNull(result);
        }


        /// <summary>
        /// when document crosswalk configuration return null
        /// </summary>
        [Fact]
        public async void GetDocumentCrosswalkConfigurationReturnNullResult()
        {
            //Act
            var mockIClinicalDocumentRepository = new Mock<IClinicalDocumentRepository>();
            var mockDbContext = new Mock<DbContext>();
            var mockIDocumentCrosswalkConfigurationRepository = new Mock<IDocumentTypeXWalkRepository>();
            var mockIDocumentCrosswalkRepository = new Mock<IDocumentTypeRepository>();
            mockIDocumentCrosswalkConfigurationRepository.Setup(m => m.GetDocumentCrosswalkConfiguration()).ReturnsAsync(TestConstants.ListOfDocumentCrosswalkConfigurationNull);

            //Arrange
            var ClinicalDocumentUnitOfWork = new ClinicalDocumentUnitOfWork(mockIClinicalDocumentRepository.Object, mockIDocumentCrosswalkConfigurationRepository.Object, mockIDocumentCrosswalkRepository.Object, mockDbContext.Object);
            var result = await ClinicalDocumentUnitOfWork.GetDocumentCrosswalkConfiguration();


            //Assert
            Assert.Null(result);
        }

        /// <summary>
        /// when document crosswalk configuration returns empty collection
        /// </summary>
        [Fact]
        public async void GetDocumentCrosswalkConfigurationReturnNotNullEmptyRecords()
        {
            //Act
            var mockIClinicalDocumentRepository = new Mock<IClinicalDocumentRepository>();
            var mockDbContext = new Mock<DbContext>();
            var mockIDocumentCrosswalkConfigurationRepository = new Mock<IDocumentTypeXWalkRepository>();
            var mockIDocumentCrosswalkRepository = new Mock<IDocumentTypeRepository>();
            mockIDocumentCrosswalkConfigurationRepository.Setup(m => m.GetDocumentCrosswalkConfiguration()).ReturnsAsync(TestConstants.ListOfDocumentCrosswalkConfigurationEmpty);

            //Arrange
            var ClinicalDocumentUnitOfWork = new ClinicalDocumentUnitOfWork(mockIClinicalDocumentRepository.Object, mockIDocumentCrosswalkConfigurationRepository.Object, mockIDocumentCrosswalkRepository.Object, mockDbContext.Object);
            var result = await ClinicalDocumentUnitOfWork.GetDocumentCrosswalkConfiguration();


            //Assert
            Assert.Empty(result);
        }

        /// <summary>
        /// when document crosswalk configuratin return collection with values
        /// </summary>
        [Fact]
        public async void GetDocumentCrosswalkConfigurationReturnWithListOfRecords()
        {
            //Act
            var mockIClinicalDocumentRepository = new Mock<IClinicalDocumentRepository>();
            var mockDbContext = new Mock<DbContext>();
            var mockIDocumentCrosswalkConfigurationRepository = new Mock<IDocumentTypeXWalkRepository>();
            var mockIDocumentCrosswalkRepository = new Mock<IDocumentTypeRepository>();
            mockIDocumentCrosswalkConfigurationRepository.Setup(m => m.GetDocumentCrosswalkConfiguration()).ReturnsAsync(TestConstants.ListOfDocumentCrosswalkConfigurationWithRecords);

            //Arrange
            var ClinicalDocumentUnitOfWork = new ClinicalDocumentUnitOfWork(mockIClinicalDocumentRepository.Object, mockIDocumentCrosswalkConfigurationRepository.Object, mockIDocumentCrosswalkRepository.Object, mockDbContext.Object);
            var result = await ClinicalDocumentUnitOfWork.GetDocumentCrosswalkConfiguration();



            //Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// when document crosswalk returns null
        /// </summary>
        [Fact]
        public async void GetDocumentCrosswalkReturnNullResult()
        {
            //Act
            var mockIClinicalDocumentRepository = new Mock<IClinicalDocumentRepository>();
            var mockDbContext = new Mock<DbContext>();
            var mockIDocumentCrosswalkConfigurationRepository = new Mock<IDocumentTypeXWalkRepository>();
            var mockIDocumentCrosswalkRepository = new Mock<IDocumentTypeRepository>();
            mockIDocumentCrosswalkRepository.Setup(m => m.GetDocumentCrosswalkDetails()).ReturnsAsync(TestConstants.ListOfDocumentCrosswalkNull);

            //Arrange
            var ClinicalDocumentUnitOfWork = new ClinicalDocumentUnitOfWork(mockIClinicalDocumentRepository.Object, mockIDocumentCrosswalkConfigurationRepository.Object, mockIDocumentCrosswalkRepository.Object, mockDbContext.Object);
            var result = await ClinicalDocumentUnitOfWork.GetDocumentCrosswalkDetails();


            //Assert
            Assert.Null(result);
        }

        /// <summary>
        /// when document crosswalk returns empty collection
        /// </summary>
        [Fact]
        public async void GetDocumentCrosswalkReturnNotNullEmptyRecords()
        {
            //Act
            var mockIClinicalDocumentRepository = new Mock<IClinicalDocumentRepository>();
            var mockDbContext = new Mock<DbContext>();
            var mockIDocumentCrosswalkConfigurationRepository = new Mock<IDocumentTypeXWalkRepository>();
            var mockIDocumentCrosswalkRepository = new Mock<IDocumentTypeRepository>();
            mockIDocumentCrosswalkRepository.Setup(m => m.GetDocumentCrosswalkDetails()).ReturnsAsync(TestConstants.ListOfDocumentCrosswalkEmpty);

            //Arrange
            var ClinicalDocumentUnitOfWork = new ClinicalDocumentUnitOfWork(mockIClinicalDocumentRepository.Object, mockIDocumentCrosswalkConfigurationRepository.Object, mockIDocumentCrosswalkRepository.Object, mockDbContext.Object);
            var result = await ClinicalDocumentUnitOfWork.GetDocumentCrosswalkDetails();


            //Assert
            Assert.Empty(result);
        }

        /// <summary>
        /// when document crosswalk returns collection with values
        /// </summary>
        [Fact]
        public async void GetDocumentCrosswalkReturnWithListOfRecords()
        {
            //Act
            var mockIClinicalDocumentRepository = new Mock<IClinicalDocumentRepository>();
            var mockDbContext = new Mock<DbContext>();
            var mockIDocumentCrosswalkConfigurationRepository = new Mock<IDocumentTypeXWalkRepository>();
            var mockIDocumentCrosswalkRepository = new Mock<IDocumentTypeRepository>();
            mockIDocumentCrosswalkRepository.Setup(m => m.GetDocumentCrosswalkDetails()).ReturnsAsync(TestConstants.ListOfDocumentCrosswalkWithRecords);

            //Arrange
            var ClinicalDocumentUnitOfWork = new ClinicalDocumentUnitOfWork(mockIClinicalDocumentRepository.Object, mockIDocumentCrosswalkConfigurationRepository.Object, mockIDocumentCrosswalkRepository.Object, mockDbContext.Object);
            var result = await ClinicalDocumentUnitOfWork.GetDocumentCrosswalkDetails();




            //Assert
            Assert.NotNull(result);
        }


    }
}
