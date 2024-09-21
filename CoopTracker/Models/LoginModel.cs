using System;
using System.ComponentModel.DataAnnotations;

namespace CoopTracker.Models
{
	public class LoginModel
	{
		[Display(Name = "Tenant Secret")]
        public string TenantSecret { get; set; }
	}
}

