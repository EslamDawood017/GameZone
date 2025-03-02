namespace GameZone.Attributes
{
	public class AllowedExtentionAttribute : ValidationAttribute
	{
		private readonly string _allowedExtention;

		public AllowedExtentionAttribute(string allowedExtenios)
		{
			_allowedExtention = allowedExtenios;
		}

		protected override ValidationResult? IsValid
			(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile; 

			if (file != null)
			{
				var Extention = Path.GetExtension(file.FileName);

				var IsAllowed = _allowedExtention.Split(',').Contains(Extention, StringComparer.OrdinalIgnoreCase);

				if (!IsAllowed)
				{
					return new ValidationResult($"Only {_allowedExtention} Are allowed ");
				}
			}
			return ValidationResult.Success;
		}
	}
}
