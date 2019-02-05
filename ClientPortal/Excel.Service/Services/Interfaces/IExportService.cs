using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Excel.Service.Services.Interfaces
{
    public interface IExportService
    {
        Task<(bool isSuccess, string message)> ImportFile(IFormFile file);
    }
}
