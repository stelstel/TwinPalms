using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwinPalmsKPI.ActionFilters
{
    public class ValidateUserExistsAttribute : IAsyncActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
      

        public ValidateUserExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");


            var id = (string)context.ActionArguments["id"];
            var user = await _repository.User.GetUserAsync(id, trackChanges);

            if (user == null)
            {
                _logger.LogInfo($"User with id {id} doesn't exist in the database");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("user", user);
                await next();
            }
        }
    }
}
