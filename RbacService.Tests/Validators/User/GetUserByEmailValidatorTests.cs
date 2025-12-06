using FluentValidation.TestHelper;
using Moq;
using RbacService.Application.Validators.User;
using RbacService.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Tests.Validators.User
{
    public class GetUserByEmailValidatorTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly GetUserByEmailValidator _validator;

        public GetUserByEmailValidatorTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _validator = new GetUserByEmailValidator();
        }

        [Fact]
        public async Task Should_HaveError_WhenEmailIsInvalid()
        {
            // Arrange & Act
            var query = new Application.Users.Queries.GetUserByEmail("not-an-email");
            var result = await _validator.TestValidateAsync(query);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Email);
        }

        [Fact]
        public async Task Should_NotHaveError_WhenEmailIsValid()
        {
            // Arrange & Act
            var query = new Application.Users.Queries.GetUserByEmail("validemail@example.com");
            var result = await _validator.TestValidateAsync(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.Email);
        }
    }
}
