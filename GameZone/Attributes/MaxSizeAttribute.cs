namespace GameZone.Attributes
{
	public class MaxSizeAttribute : ValidationAttribute
	{
		private readonly int _maxSize;

        public MaxSizeAttribute(int MaxSize)
        {
            _maxSize = MaxSize;
        }
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile;
			if (file != null)
			{
				if(file.Length > _maxSize)
				{
					return new ValidationResult($"Maximum allowed size is {_maxSize/(1024*1024)}MB");
				}
			}
			return ValidationResult.Success;
			
		}	
	}
}
