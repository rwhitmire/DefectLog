using System;
using System.Linq;
using System.Linq.Expressions;
using DefectLog.Web.Validation.Framework;
using Xunit;

namespace DefectLog.Tests
{
    public static class AssertionExtensions
    {
        public static void ShouldHaveError<T>(this ValidationResult result, Expression<Func<T, object>> expression, string errorMessage)
        {
            var body = (MemberExpression)expression.Body;
            var propertyName = body.Member.Name;

            result.ShouldHaveError(propertyName, errorMessage);
        }

        public static void ShouldHaveError(this ValidationResult result, string propertyName, string errorMessage)
        {
            var error = result.Errors.Where(x => x.PropertyName == propertyName).FirstOrDefault(x => x.ErrorMessage == errorMessage);
            Assert.NotNull(error);
        }
    }
}