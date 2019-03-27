using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheck.Model.Models
{
    public class Error
    {
        public string ReturnUrl { get; set; }
        public string Message { get; set; }

        public Error(string returnUrl, string message)
        {
            this.ReturnUrl = returnUrl;
            this.Message = message;
        }
    }
}
