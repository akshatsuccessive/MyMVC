using CRUDApplication.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace CRUDApplication.Validator
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(emp => emp.Email).EmailAddress();
        }
    }
}
