﻿using System.ComponentModel.DataAnnotations;

namespace DefectLog.Forms
{
    public class ForgotPasswordForm
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}