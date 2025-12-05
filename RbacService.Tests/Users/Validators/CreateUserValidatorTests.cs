using FluentValidation.TestHelper;
using Moq;
using RbacService.Application.Users.Commands;
using RbacService.Application.Validators.User;
using RbacService.Domain.Interfaces.Repositories;

namespace RbacService.Tests.Users.Validators
{
    public class CreateUserValidatorTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly CreateUserValidator _validator;

        public CreateUserValidatorTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _validator = new CreateUserValidator(_mockRepo.Object);
        }

        [Fact]
        public async Task Should_HaveError_WhenEmailIsInvalid()
        {
            var command = new CreateUser("Valid Name", "not-an-email",  null,
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null);

            var result = await _validator.TestValidateAsync(command);
            result.ShouldHaveValidationErrorFor(c => c.Email);
        }

        [Fact]
        public async Task Should_HaveError_WhenEmailAlreadyExists()
        {
            _mockRepo.Setup(r => r.ExistsByEmailAsync("duplicate@example.com", null, CancellationToken.None))
                     .ReturnsAsync(true);

            var command = new CreateUser("Valid Name", "duplicate@example.com",  null,
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null);

            var result = await _validator.TestValidateAsync(command);
            result.ShouldHaveValidationErrorFor(c => c.Email);
        }

        [Fact]
        public async Task Should_NotHaveError_WhenDesignationIsEmpty()
        {
            var command = new CreateUser("Valid Name", "valid@example.com", null,
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null);

            var result = await _validator.TestValidateAsync(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Designation);
        }

        [Fact]
        public async Task Should_HaveError_WhenDesignationTooLong()
        {
            var longDesignation = new string('X', 51);
            var command = new CreateUser("Valid Name", "valid@example.com",  longDesignation,
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null);

            var result = await _validator.TestValidateAsync(command);
            result.ShouldHaveValidationErrorFor(c => c.Designation);
        }

    }
}
