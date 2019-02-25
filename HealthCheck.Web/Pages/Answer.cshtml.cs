using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCheck.Web.Pages
{
    public class AnswerModel : PageModel
    {
        public string SentBy { get; set; }

        public void OnGet(string sentBy)
        {
            this.SentBy = sentBy;
        }
    }
}