using FluentValidation;

namespace Finervo.Application.Common.Validators
{
    public static class SharedValidationRules
    {
        #region Name (First Name, Last Name)
        public static IRuleBuilderOptions<T, string> MustBeValidName<T>(
            this IRuleBuilder<T, string> ruleBuilder, string fieldName) =>
            ruleBuilder
                .NotEmpty().WithMessage($"{fieldName} is required")
                .MinimumLength(2).WithMessage($"{fieldName} must be atleast 2 characters")
                .MaximumLength(20).WithMessage($"{fieldName} cannot exceed 20 characters")
                .Matches("^[a-zA-Z\\s'-]+$").WithMessage($"{fieldName} can only contain letters, spaces, hyphens(-) and apostrophes(s')");
        #endregion

        #region Email
        public static IRuleBuilderOptions<T, string> MustBeValidEmail<T>(
            this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Please enter a valid email address")
                .MaximumLength(256).WithMessage("Email address cannot exceed 256 characters");
        #endregion

        #region Password
        public static IRuleBuilderOptions<T, string> MustBeValidPassword<T>(
        this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(64).WithMessage("Password cannot exceed 64 characters")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
        #endregion

        #region UserName
        public static IRuleBuilderOptions<T, string> MustBeValidUserName<T>(
        this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters")
            .MaximumLength(40).WithMessage("Username cannot exceed 40 characters")
            .Matches("^[a-zA-Z0-9_.]+$").WithMessage("Username can only contain letters, numbers, periods(.) and underscores(_)");
        #endregion
    }
}
