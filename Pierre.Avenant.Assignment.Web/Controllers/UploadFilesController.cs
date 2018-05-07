using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pierre.Avenant.Assignment.Core.Interfaces.Database;
using Pierre.Avenant.Assignment.Core.Interfaces.Excel;
using Pierre.Avenant.Assignment.Core.Interfaces.Services;
using Pierre.Avenant.Assignment.Core.Services.FileInputService;
using Pierre.Avenant.Assignment.Web.Models;

namespace Pierre.Avenant.Assignment.Web.Controllers
{
    public class UploadFilesController : Controller
    {
        private IExcelFileProcessor _excelFileUploader;
        private ICurrencyCodeRepository _currencyCodeRepository;
        private IFileUploadRepository _fileUploadRepository;
        private IAccountTransactionRepository _accountTransactionRepository;

        public UploadFilesController(IExcelFileProcessor excelFileUploader, ICurrencyCodeRepository currencyCodeRepository,
                                     IFileUploadRepository fileUploadRepository,IAccountTransactionRepository accountTransactionRepository)
        {
            _excelFileUploader = excelFileUploader;
            _currencyCodeRepository = currencyCodeRepository;
            _fileUploadRepository = fileUploadRepository;
            _accountTransactionRepository = accountTransactionRepository;
        }

        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            UploadFilesModel viewModel = new UploadFilesModel();
            if (!ValidatePost(viewModel, files))
            {
                return View(viewModel);
            }

            viewModel.UserFileName = files[0].FileName;
            var fileToDownload = files[0];
            var tempFilePath = Path.GetTempFileName();

            await UploadFile(fileToDownload, tempFilePath);

            try
            {
                IFileUploadService service = new FileUploadService(_excelFileUploader, _currencyCodeRepository, _fileUploadRepository, _accountTransactionRepository);
                var result = service.ImportAccountTransactionFile(tempFilePath,viewModel.UserFileName);
                viewModel.BuildModelViewFromResult(result);
                viewModel.SuccessMessage = result.FileUpload.NumberOfRecords == result.FileUpload.NumberOfSuccessfullRecords ? "successfully uploaded." : "partially uploaded.";
            }
            catch (FormatException fe)
            {
                viewModel.Exception = true;
                viewModel.ExceptionMessage = fe.Message;
                return View(viewModel);
            }

            return View(viewModel);
        }

        private static async Task UploadFile(IFormFile fileToDownload, string tempFilePath)
        {
            if (fileToDownload.Length > 0)
            {
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await fileToDownload.CopyToAsync(stream);
                }
            }
        }

        private bool ValidatePost(UploadFilesModel viewMode, List<IFormFile> files)
        {
            if (!ValidateFileSelected(viewMode, files)) return false;
            if (!ValidateOnlyOneFileSelected(viewMode, files)) return false;
            if (!ValidateFileContentType(viewMode, files)) return false;
            if (!ValidateFileNotEmpoty(viewMode, files)) return false;

            return true;
        }

        private static bool ValidateFileContentType(UploadFilesModel viewMode, List<IFormFile> files)
        {
            if (files[0].ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                viewMode.Exception = true;
                viewMode.ExceptionMessage = "File is not a valid file format. Please select a valid .xlsx file.";
                return false;
            }
            return true;
        }

        private static bool ValidateOnlyOneFileSelected(UploadFilesModel viewMode, List<IFormFile> files)
        {
            if (files.Count > 1)
            {
                viewMode.Exception = true;
                viewMode.ExceptionMessage = "Only 1 file may be uploaded at a time.";
                return false;
            }
            return true;
        }

        private static bool ValidateFileSelected(UploadFilesModel viewMode, List<IFormFile> files)
        {
            if (files.Count == 0)
            {
                viewMode.Exception = true;
                viewMode.ExceptionMessage = "No file selected. Please select a valid file.";
                return false;
            }
            return true;
        }

        private bool ValidateFileNotEmpoty(UploadFilesModel viewMode, List<IFormFile> files)
        {
            if (files[0].Length == 0)
            {
                viewMode.Exception = true;
                viewMode.ExceptionMessage = "Empty File. Please select a valid file";
                return false;
            }
            return true;
        }
    }
}
