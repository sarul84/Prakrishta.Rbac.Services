using FluentAssertions;
using Moq;
using RbacService.Application.Users.Commands;
using RbacService.Application.Validators;
using RbacService.Application.Validators.User;
using RbacService.Domain.Interfaces.Repositories;
using RulesEngine.Models;

namespace RbacService.Tests.Validators.User
{
    public class GenericValidatorServiceTests
    {
        [Fact]
        public async Task ValidateAsync_ShouldReturnFluentValidationErrors()
        {
            var mockRepo = new Mock<IUserRepository>();
            var fluentValidator = new CreateUserValidator(mockRepo.Object);
            var service = new ValidatorService<CreateUser>(fluentValidator);

            var command = new CreateUser("Valid Name", "not-an-email", null,
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null);

            var errors = await service.ValidateAsync(command);

            errors.Should().Contain(e => e.Contains("Email must be a valid email address"));
        }

        [Fact]
        public async Task ValidateAsync_ShouldReturnRulesEngineErrors()
        {
            var mockRepo = new Mock<IUserRepository>();
            var fluentValidator = new CreateUserValidator(mockRepo.Object);

            var workflow = new Workflow
            {
                WorkflowName = nameof(CreateUser),
                Rules =
                [
                    new Rule
                    {
                        RuleName = "DesignationRequired",
                        Expression = "input.Designation == null",
                        ErrorMessage = "Designation is required"
                    }
                ]
            };

            var rulesEngine = new RulesEngine.RulesEngine(new[] { workflow }, null);
            var service = new ValidatorService<CreateUser>(fluentValidator, rulesEngine);

            var command = new CreateUser("Valid Name", "valid@example.com", null,
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null);

            var errors = await service.ValidateAsync(command);

            errors.Should().Contain("Designation is required");
        }

        [Fact]
        public async Task ValidateAsync_ShouldReturnCombinedErrors()
        {
            var mockRepo = new Mock<IUserRepository>();
            var fluentValidator = new CreateUserValidator(mockRepo.Object);

            var workflow = new Workflow
            {
                WorkflowName = nameof(CreateUser),
                Rules =
                [
                    new Rule
                    {
                        RuleName = "DesignationRequired",
                        Expression = "input.Designation == null",
                        ErrorMessage = "Designation is required"
                    }
                ]
            };

            var rulesEngine = new RulesEngine.RulesEngine(new[] { workflow }, null);
            var service = new ValidatorService<CreateUser>(fluentValidator, rulesEngine);

            var command = new CreateUser("", "not-an-email", null,
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null);

            var errors = await service.ValidateAsync(command);

            errors.Should().Contain(e => e.Contains("Email must be a valid email address"));
            errors.Should().Contain(e => e.Contains("Name is required"));
            errors.Should().Contain("Designation is required");
        }


    }
}
