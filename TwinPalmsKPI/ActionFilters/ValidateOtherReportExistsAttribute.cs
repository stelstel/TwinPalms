using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwinPalmsKPI.ActionFilters
{
    public class ValidateOtherReportExistsAttribute : IAsyncActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;

        public ValidateOtherReportExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)

        {
            var trackChanges = context.HttpContext.Request.Method.Equals("Put");
            var id = (int)context.ActionArguments["id"];
            var otherReport = await _repository.OtherReport.GetOtherReportAsync(id, trackChanges);
            if (otherReport == null)
            {
                _logger.LogInfo($"OtherReport with id {id} doesn't exist in the database");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("otherReport", otherReport);
                await next();
            }
        }
    }
}
