using EducationalInstitutionAPI.Data.Helpers;
using EducationalInstitutionAPI.Utils;
using System;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Models_Tests
{
    public class AccessTests
    {
        [Fact]
        public void CreatingAnInstance_ShouldReturnFalseIsDisabledField()
        {
            //Act
            var access = new Access();

            //Assert
            Assert.False(access.IsDisabled);
        }

        [Fact]
        public void CreatingAnInstance_ShouldReturnNullDateForPermanentDeletionField()
        {
            //Act
            var access = new Access();

            //Assert
            Assert.Null(access.DateForPermanentDeletion);
        }

        [Fact]
        public void GivenAnInstance_CallingScheduleForDeletionMethod_ShouldSetIsDisabledToTrue()
        {
            //Arrange
            var access = new Access();

            //Act
            access.ScheduleForDeletion();

            //Assert
            Assert.True(access.IsDisabled);
        }

        [Fact]
        public void GivenAnInstance_CallingScheduleForDeletionMethod_ShouldSetDateForPermanentDeletionDate()
        {
            //Arrange
            var access = new Access();

            var expectedDateForPermanentDeletion = DateTime.UtcNow.Date;
            var daysFromConfigFile = ConfigurationHelper.GetCurrentSettings("DaysUntilDeletion");
            int days = int.Parse(daysFromConfigFile);
            expectedDateForPermanentDeletion = expectedDateForPermanentDeletion.AddDays(days).ToUniversalTime();

            //Act
            access.ScheduleForDeletion();

            //Assert
            Assert.Equal(expectedDateForPermanentDeletion.Date, access.DateForPermanentDeletion.Value.Date);
        }

        [Fact]
        public void GivenAnInstance_CallingScheduleForDeletionMethod_ShouldSetDateForPermanentDeletionDay()
        {
            //Arrange
            var access = new Access();

            var expectedDateForPermanentDeletion = DateTime.UtcNow.Date;
            var daysFromConfigFile = ConfigurationHelper.GetCurrentSettings("DaysUntilDeletion");
            int days = int.Parse(daysFromConfigFile);
            expectedDateForPermanentDeletion = expectedDateForPermanentDeletion.AddDays(days).ToUniversalTime();

            //Act
            access.ScheduleForDeletion();

            //Assert
            Assert.Equal(expectedDateForPermanentDeletion.Day, access.DateForPermanentDeletion.Value.Day);
        }

        [Fact]
        public void GivenAnInstance_ThatIsScheduledForDeletion_CallingRemoveDeletionScheduleMethod_ShouldSetIsDisabledToFalse()
        {
            //Arrange
            var access = new Access();
            access.ScheduleForDeletion();

            //Act
            access.RemoveDeletionSchedule();

            //Assert
            Assert.False(access.IsDisabled);
        }

        [Fact]
        public void GivenAnInstance_ThatIsScheduledForDeletion_CallingRemoveDeletionScheduleMethod_ShouldSetDateForPermanentDeletionToNull()
        {
            //Arrange
            var access = new Access();
            access.ScheduleForDeletion();

            //Act
            access.RemoveDeletionSchedule();

            //Assert
            Assert.Null(access.DateForPermanentDeletion);
        }
    }
}