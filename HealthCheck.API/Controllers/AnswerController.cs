﻿using HealthCheck.API.Services;
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
        public IEnumerable<GuestUserAnswer> GetGuestAnswers(Expression<Func<GuestUserAnswer, bool>> exp)
        {
            return answerRepository.GetGuestAnswers(exp);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<Answer>> GetAll()
        {
            return await answerRepository.GetAll();
        }

        [HttpPost]
        public async Task<Answer> InsertOrUpdate([FromBody] Answer answer)
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

            await answerRepository.InsertOrUpdateAnswer(answer);
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
        public void Update(int id, [FromBody] Answer answerIn)
        {
            answerRepository.Update(answerIn);
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
        public ActionResult ExportSessionsAnswersToExcelAsync(int currentSessionId)
        {            
            var answers = answerRepository.GetAnswers(x => x.SessionId == currentSessionId);
            Dictionary<string, string> answerDictionary = new Dictionary<string, string>();

            var reportItems = new List<AnswerReportItem>();
            foreach (var answer in answers)
            {
                answerDictionary.Add($"{answer.User.Name},{answer.Category.Name}", answer.AnswerOption.Option);
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