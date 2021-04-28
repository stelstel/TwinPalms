using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwinPalmsKPI.ActionFilters
{
    public class ValidateRoomsReportExistsAttribute : IAsyncActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;

        public ValidateRoomsReportExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)

        {
            var trackChanges = context.HttpContext.Request.Method.Equals("Put");
            var id = (int)context.ActionArguments["id"];
            var roomsReport = await _repository.RoomsReport.GetRoomsReportAsync(id, trackChanges);
            if (roomsReport == null)
            {
                _logger.LogInfo($"RoomsReport with id {id} doesn't exist in the database");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("roomsReport", roomsReport);
                await next();
            }
        }
    }
}
