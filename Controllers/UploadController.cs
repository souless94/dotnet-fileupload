using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_fileupload.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly ILogger<UploadController> _logger;
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _config;
    public UploadController(ILogger<UploadController> logger,
                            IWebHostEnvironment env,
                            IConfiguration configuration)
    {
        _logger = logger;
        _env = env;
        _config = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
    {

        if (file.Length > 0)
        {
            string filePath = Path.Combine(_env.ContentRootPath, _config["uploadFolder"], file.FileName);
            _logger.LogInformation($"Uploading file {filePath}");
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
        }
        return Ok("File uploaded!");
    }
}
