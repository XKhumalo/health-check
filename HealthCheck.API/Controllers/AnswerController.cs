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
        private readonly AnswerRepository answerRepository;
        private readonly ExcelExportService excelExportService;

        public AnswerController(AnswerRepository answerRepository, ExcelExportService excelExportService)
        {
            this.answerRepository = answerRepository;
            this.excelExportService = excelExportService;
        }

        [HttpGet("{id}")]
        [Route("[action]")]
        public async Task<Answer> GetById(int id)
        {
            return await answerRepository.GetById(id);
        }

        [HttpGet]
        public IEnumerable<Answer> Get(Expression<Func<Answer, bool>> exp)
        {
            return answerRepository.GetAnswers(exp);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<Answer>> GetAll()
        {
            return await answerRepository.GetAll();
        }

        [HttpPost]
        public async Task<Answer> Create([FromBody] Answer answer)
        {
            if (answer == null)
            {
                return null;
            }

            await answerRepository.Create(answer);
            answerRepository.SaveChanges();
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

            await answerRepository.Create(answers);
            answerRepository.SaveChanges();
            return answers;
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] Answer answerIn)
        {
            await answerRepository.Update(answerIn);
            answerRepository.SaveChanges();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var answer = await answerRepository.GetById(id);
            answerRepository.Delete(answer);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> ExportSessionsAnswersToExcelAsync(int currentSessionId)
        {            
            var answers = answerRepository.GetAnswers(x => x.SessionId == currentSessionId);

            List<AnswerReportItem> reportItems = new List<AnswerReportItem>();
            foreach (var answer in answers)
            {
                var reportItem = new AnswerReportItem()
                {
                    AnsweredBy = answer.UserId.ToString(),
                    Answer = answer.AnswerId.ToString(),
                    CategoryName = answer.CategoryId.ToString()
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

            var fileName = $"Health Check Answers {DateTime.Today}.xlsx";
            return File(excelExportService.ExportToExcel(reportItems, "Answers", false, headingReplacer), ExcelExportService.ExcelMimeType, fileName);
        }
    }
}