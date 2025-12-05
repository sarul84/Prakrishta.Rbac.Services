using FluentValidation.TestHelper;
using Moq;
using RbacService.Application.Users.Commands;
using RbacService.Application.Validators.User;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Tests.Users.Validators
{
    public class UpdateUserValidatorTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly UpdateUserValidator _validator;

        public UpdateUserValidatorTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _validator = new UpdateUserValidator(_mockRepo.Object);
        }

        [Fact]
        public async Task Should_HaveError_WhenUserDoesNotExist()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(value: null);

            var command = new UpdateUser(Guid.NewGuid(), "Valid Name", "valid@example.com",  null,
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null);

            var result = await _validator.TestValidateAsync(command);
            result.ShouldHaveValidationErrorFor(c => c.UserId)
                .WithErrorMessage("User does not exist");

        }

        [Fact]
        public async Task Should_HaveError_WhenEmailAlreadyExists()
        {
            var userId = Guid.NewGuid();
            _mockRepo.Setup(r => r.ExistsByEmailAsync("duplicate@example.com", userId))
                     .ReturnsAsync(true);

            var command = new UpdateUser(userId, "Valid Name", "duplicate@example.com",  null,
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null);

            var result = await _validator.TestValidateAsync(command);
            result.ShouldHaveValidationErrorFor(c => c.Email);
        }

        [Fact]
        public async Task Should_NotHaveError_WhenDesignationIsEmpty()
        {
            var userId = Guid.NewGuid();
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(value: null);

            var command = new UpdateUser(userId, "Valid Name", "valid@example.com",  null,
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null);

            var result = await _validator.TestValidateAsync(command);

            result.ShouldNotHaveValidationErrorFor(c => c.Designation);
        }

        [Fact]
        public async Task Should_HaveError_WhenDesignationTooLong()
        {
            var userId = Guid.NewGuid();
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(value: null);

            var longDesignation = new string('X', 51);
            var command = new UpdateUser(userId, "Valid Name", "valid@example.com", longDesignation,
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null);

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(c => c.Designation)
                  .WithErrorMessage("Designation must not exceed 50 characters");
        }

    }

}
