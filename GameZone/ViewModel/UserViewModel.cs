namespace GameZone.ViewModel
{
	public class UserViewModel
	{
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Compare("Password")]
		[Display(Name ="Confirm Password")]
		public string confirmPassword { get; set; }
        public string Address { get; set; }
	}
}
