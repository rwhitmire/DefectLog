namespace DefectLog.Validation.Framework
{
    public interface IValidator<in T>
    {
        ValidationResult Validate(T form);
    }
}