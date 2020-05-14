using System.ComponentModel.DataAnnotations;

namespace CurrencyPortalAPI.Validators
{
	public class HaveSameValueAttribute : ValidationAttribute
	{
		private readonly string _firstProperty;
		private readonly string _secondProperty;

		public HaveSameValueAttribute(string firstPropertyName , string secondPropertyName)
		{
			_firstProperty = firstPropertyName;
			_secondProperty = secondPropertyName;

			ErrorMessage = $"{firstPropertyName} and {secondPropertyName} are not the same";
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var firstPropertyValue = validationContext.ObjectType.GetProperty(_firstProperty)
																 .GetValue(validationContext.ObjectInstance);

			var secondPropertyValue = validationContext.ObjectType.GetProperty(_secondProperty)
																  .GetValue(validationContext.ObjectInstance);

			if (firstPropertyValue == null || secondPropertyValue == null)
				return new ValidationResult(getErrorMessage());

			else if (firstPropertyValue.ToString() != secondPropertyValue.ToString())
				return new ValidationResult(getErrorMessage());

			else
				return ValidationResult.Success;
		}

		private string getErrorMessage()
			=> string.Format(ErrorMessage, _firstProperty, _secondProperty);
	}
}
