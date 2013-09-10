namespace DefectLog.Web.Validation.Framework
{
    public interface IValidator<in T>
    {
        ValidationResult Validate(T form);
    }
}