namespace RbacService.Application.Interfaces
{
    public interface IValidatorService<T>
    {
        Task<IList<string>> ValidateAsync(T command, CancellationToken cancellationToken = default);
    }
}
