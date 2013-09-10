using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace DefectLog.Web.Validation.Framework
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new List<Error>();
        }

        public bool IsValid
        {
            get { return !Errors.Any(); }
        }
        
        public ICollection<Error> Errors { get; set; }

        public void AddError(string propertyName, string errorMessage)
        {
            Errors.Add(new Error {PropertyName = propertyName, ErrorMessage = errorMessage});
        }

        public void AddError<T>(Expression<Func<T, object>> expression, string errorMessage)
        {
            var body = (MemberExpression) expression.Body;
            var propertyName = body.Member.Name;

            AddError(propertyName, errorMessage);
        }

        public ValidationResult Populate(ModelStateDictionary modelState)
        {
            foreach (var error in Errors)
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);

            return this;
        }
    }
}