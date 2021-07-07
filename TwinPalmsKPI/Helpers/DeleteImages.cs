﻿using Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using AutoMapper;

namespace TwinPalmsKPI.Helpers
{
    public class DeleteImages
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        //private readonly IMapper _mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public DeleteImages(IRepositoryManager repository, ILoggerManager logger,
            /*IMapper mapper,*/ IWebHostEnvironment environment, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            env = environment;
            config = configuration;
        }

        public async void DelImgs(int[] outletIds)
        // Deletes all images between dateForFirstDelete and theFuture
        {
            int weeksToKeepImages = config.GetValue<int>("DeleteImagesConfiguration:WeeksToKeepImages");
            DateTime theFuture = new DateTime(3021, 1, 1);

            int daysToKeepImages = weeksToKeepImages * 7;

            DateTime dateForFirstDelete = new DateTime();

            if (env.IsDevelopment())
            {
                dateForFirstDelete = DateTime.UtcNow;
            }
            else if (env.IsProduction())
            {
                dateForFirstDelete = DateTime.UtcNow.AddDays(daysToKeepImages);
            }
            try
            { 

                var reports = await _repository.FbReport.GetAllOutletFbReportsForOutlets(outletIds, dateForFirstDelete, theFuture, trackChanges: false);

                foreach (var rep in reports)
                {
                    // Find image name and delete it
                    rep.ImagePath = "bengt";
                    _repository.FbReport.UpdateFbReport(rep);
                }

                await _repository.SaveAsync();
            }
            catch (Exception ex) 
            {

                string str = ex.ToString();

            }

 
            /*
            var fbReportEntity = HttpContext.Items["fbReport"] as FbReport;
            _repository.FbReport.UpdateFbReport(fbReportEntity);
            _mapper.Map(fbReport, fbReportEntity);
            await _repository.SaveAsync();
            var fbReportToReturn = _mapper.Map<FbReportDto>(fbReportEntity);
             */
        }
    }
}
