using EducationalInstitution.Domain.Building_Blocks;
using System;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Domain_Tests
{
    public class EntityHelper : GuidEntity
    {
        public EntityHelper(Guid? id) : base(id)
        {
        }
    }

    public class EntityTests
    {
        private EntityHelper entity;

        public EntityTests() => entity = new(null);

        [Fact]
        public void CreatingAnInstance_ShouldReturnFalseIsDisabledField()
        {
            //Assert
            Assert.False(entity.Access.IsDisabled);
        }

        [Fact]
        public void CreatingAnInstance_ShouldReturnNullDateForPermanentDeletionField()
        {
            //Assert
            Assert.Null(entity.Access.DateForPermanentDeletion);
        }

        [Fact]
        public void CallingScheduleForDeletionMethod_ShouldSetIsDisabledToTrue()
        {
            //Act
            entity.ScheduleForDeletion();

            //Assert
            Assert.True(entity.Access.IsDisabled);
        }

        [Fact]
        public void GivenDaysUntilDeletion_CallingScheduleForDeletionMethod_ShouldSetDateForPermanentDeletionDate()
        {
            //Arrange
            int days = 20;
            var expectedDateForPermanentDeletion = DateTime.UtcNow.Date;
            expectedDateForPermanentDeletion = expectedDateForPermanentDeletion.AddDays(days).ToUniversalTime();

            //Act
            entity.ScheduleForDeletion(days);

            //Assert
            Assert.Equal(expectedDateForPermanentDeletion.Date, entity.Access.DateForPermanentDeletion.Value.Date);
        }

        [Fact]
        public void GivenDaysUntilDeletion_CallingScheduleForDeletionMethod_ShouldSetDateForPermanentDeletionDay()
        {
            //Arrange
            int days = 20;
            var expectedDateForPermanentDeletion = DateTime.UtcNow.Date;
            expectedDateForPermanentDeletion = expectedDateForPermanentDeletion.AddDays(days).ToUniversalTime();

            //Act
            entity.ScheduleForDeletion(days);

            //Assert
            Assert.Equal(expectedDateForPermanentDeletion.Day, entity.Access.DateForPermanentDeletion.Value.Day);
        }

        [Fact]
        public void GivenDaysUntilDeletionLesserThanZero_CallingScheduleForDeletionMethod_ShouldThrowArgumentOutOfRangeException()
        {
            //Arrange
            int days = -12;

            //Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => entity.ScheduleForDeletion(days));
        }

        [Fact]
        public void GivenAnEntityThatIsScheduledForDeletion_CallingClearDeletionDateMethod_ShouldSetIsDisabledToFalse()
        {
            //Arrange
            entity.ScheduleForDeletion();

            //Act
            entity.ClearDeletionDate();

            //Assert
            Assert.False(entity.Access.IsDisabled);
        }

        [Fact]
        public void GivenAnEntityThatIsScheduledForDeletion_CallingRemoveDeletionScheduleMethod_ShouldSetDateForPermanentDeletionToNull()
        {
            //Arrange
            entity.ScheduleForDeletion();

            //Act
            entity.ClearDeletionDate();

            //Assert
            Assert.Null(entity.Access.DateForPermanentDeletion);
        }

        [Fact]
        public void GivenAnId_ShouldReturnAnInstanceWithTheGivenId()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var entityHelper = new EntityHelper(id);

            //Assert
            Assert.Equal(id, entityHelper.Id);
        }

        [Fact]
        public void CreatingAnInstanceWithNullArgument_ShouldReturnAnIdThatIsNotDefault()
        {
            //Act
            var entityHelper = new EntityHelper(null);

            //Assert
            Assert.NotEqual(Guid.Empty, entityHelper.Id);
        }

        [Fact]
        public void CreatingAnInstanceWithDefaultGuid_ShouldThrowArgumentException()
        {
            //Arrange
            var id = Guid.Empty;

            //Act & Assert
            Assert.Throws<ArgumentException>(() => { var entityHelper = new EntityHelper(id); });
        }
    }
}