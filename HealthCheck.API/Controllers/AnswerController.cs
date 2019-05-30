using HealthCheck.API.Services;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace HealthCheck.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        private readonly AnswerRepository answerService;
        private readonly ExcelExportService excelExportService;

        public AnswerController(AnswerRepository answerService, ExcelExportService excelExportService)
        {
            this.answerService = answerService;
            this.excelExportService = excelExportService;
        }

        [HttpGet("{id}")]
        [Route("[action]")]
        public Answer GetById(int id)
        {
            return answerService.GetById(id);
        }

        [HttpGet]
        public IEnumerable<Answer> Get(Expression<Func<Answer, bool>> exp)
        {
            return answerService.GetAnswers(exp);
        }

        [HttpPost]
        public Answer Create([FromBody] Answer answer)
        {
            if (answer == null)
            {
                return null;
            }

            if (answer.CategoryId == default)
            {
                return null;
            }

            if (answer.SessionId == default)
            {
                return null;
            }

            if (answer.UserId == default)
            {
                return null;
            }

            var persistedAnswer = answerService.Create(answer);
            return persistedAnswer;
        }

        [HttpPost]
        [Route("[action]")]
        public IEnumerable<Answer> CreateList([FromBody] IEnumerable<Answer> answers)
        {
            if (answers == null)
            {
                return null;
            }

            var persistedAnswers = answerService.Create(answers);
            return persistedAnswers;
        }

        [HttpPut("{id}")]
        public Answer Update(int id, [FromBody] Answer answerIn)
        {
            var updatedAnswer = answerService.Update(answerIn);
            return updatedAnswer;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var answer = answerService.GetById(id);
            answerService.Delete(answer);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult ExportSessionsAnswersToExcel(int currentSessionId)
        {            
            var answers = answerService.GetAnswers(x => x.SessionId == currentSessionId);

            var reportItems = new List<AnswerReportItem>();
            foreach (var answer in answers)
            {
                var reportItem = new AnswerReportItem()
                {
                    AnsweredBy = answer.User.Name,
                    Answer = answer.AnswerOption.Option,
                    CategoryName = answer.Category.Name
                };
                reportItems.Add(reportItem);
            }

            ExcelExportService.StringReplacementDelegate headingReplacer = s =>
            {
                switch (s)
                {
                    case "CategoryChosen":
                        return "Answer";
                    case "CategoryId":
                        return "Category";
                    case "UserId":
                        return "User";
                    default:
                        return excelExportService.PascalToSpacedString(s);
                }                
            };

            var fileName = $"Team Health Check {DateTime.Today:MMM yyyy}.xlsx";
            return File(excelExportService.ExportToExcel(reportItems, "Answers", false, headingReplacer), ExcelExportService.ExcelMimeType, fileName);
        }
    }
}