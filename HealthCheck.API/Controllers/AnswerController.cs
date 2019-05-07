using HealthCheck.API.Models;
using HealthCheck.API.Services;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthCheck.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        private readonly AnswerService answerService;
        private readonly ExcelExportService excelExportService;

        public AnswerController(AnswerService answerService, ExcelExportService excelExportService)
        {
            this.answerService = answerService;
            this.excelExportService = excelExportService;
        }

        [HttpGet("{id:length(24)}")]
        [Route("[action]")]
        public async Task<Answer> GetById(int id)
        {
            return await answerService.GetById(id);
        }

        [HttpGet]
        public IEnumerable<Answer> Get(Expression<Func<Answer, bool>> exp)
        {
            return answerService.GetAnswers(exp);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<Answer>> GetAll()
        {
            return await answerService.GetAll();
        }

        [HttpPost]
        public async Task<Answer> Create([FromBody] Answer answer)
        {
            if (answer == null)
            {
                return null;
            }

            await answerService.Create(answer);
            answerService.SaveChanges();
            return answer;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IEnumerable<Answer>> CreateList([FromBody] IEnumerable<Answer> answers)
        {
            if (answers == null)
            {
                return null;
            }

            await answerService.Create(answers);
            answerService.SaveChanges();
            return answers;
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] Answer answerIn)
        {
            await answerService.Update(answerIn);
            answerService.SaveChanges();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var answer = await answerService.GetById(id);
            answerService.Delete(answer);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> ExportSessionsAnswersToExcelAsync(int currentSessionId)
        {            
            var answers = answerService.GetAnswers(x => x.SessionId == currentSessionId);

            List<AnswerReportItem> reportItems = new List<AnswerReportItem>();
            foreach (var answer in answers)
            {
                var reportItem = new AnswerReportItem()
                {
                    AnsweredBy = answer.UserId.ToString(),
                    Answer = answer.AnswerId.ToString(),
                    Category = answer.CategoryId.ToString()
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

            var fileName = "Answers.xlsx";
            return File(excelExportService.ExportToExcel(reportItems, "Answers", false, headingReplacer), ExcelExportService.ExcelMimeType, fileName);
        }
    }
}