using Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using AutoMapper;
using System.Threading.Tasks;
using System.IO;

namespace TwinPalmsKPI.Helpers
{
    public class DeleteImages
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        //private readonly IMapper _mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public DeleteImages(IRepositoryManager repository, ILoggerManager logger, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _repository = repository;
            env = environment;
            config = configuration;
            _logger = logger;
        }

        public async Task DelImgs(int[] outletIds)
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
                var reports = await _repository.FbReport.GetAllOutletFbReportsForOutlets(outletIds, dateForFirstDelete, theFuture, trackChanges: true);
                //var folderName = Path.Combine("Resources", "Images");
                //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                foreach (var rep in reports)
                {
                    // Find image name and delete it
                    if (rep.ImagePath != null && rep.ImagePath.Length > 39)
                    {
                            var file = rep.ImagePath;

                            try    
                            {
                                // Check if file exists with its full path    
                                if (File.Exists(file))
                                {
                                    // If file found, delete it    
                                    File.Delete(file);
                                    _logger.LogDebug($"File {file} deleted.");
                                }
                                else
                                {
                                    _logger.LogDebug("File not found"); 
                                }    
                            }    
                            catch (IOException ioExp)    
                            {
                                _logger.LogDebug(ioExp.Message);    
                            }   
                    }
                    // Find and change the ImagePath field in FbReport DB table 
                    // rep.ImagePath = "deleted"; // Doesn't work
                    //_repository.FbReport.UpdateFbReport(rep); // Doesn't work
                }
            }
            catch (Exception ex) 
            {
                string str = ex.ToString();
            }
        }
    }
}
