using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pierre.Avenant.Assignment.Infrastructure.Database;
using Pierre.Avenant.Assignment.Infrastructure.Excel;
using Pierre.Avenant.Assignment.Web.Controllers;
using Pierre.Avenant.Assignment.Web.Models;

namespace Pierre.Avenant.Assignment.Test.Web
{
    [TestClass]
    public class UploadFilesControllerTest
    {
        [TestMethod]
        public void UploadFiles_NoFileSelected_ValidationFailure()
        {
            var controller = new UploadFilesController(null, null, null, null);
            List<IFormFile> files = new List<IFormFile>();

            var result = controller.Post(files).Result;
            Assert.AreEqual(((UploadFilesModel) ((ViewResult) result).Model).ExceptionMessage, "No file selected. Please select a valid file.");
            Console.WriteLine(result.ToString());
        }

        [TestMethod]
        public void UploadFiles_FileTypeNotValidException()
        {
            var controller = new UploadFilesController(null, null, null, null);
            Mock< IFormFile> fileMock = new Mock<IFormFile>();
            List<IFormFile> files = new List<IFormFile> {fileMock.Object};

            var result = controller.Post(files).Result;
            Assert.AreEqual(((UploadFilesModel)((ViewResult)result).Model).ExceptionMessage, "File is not a valid file format. Please select a valid .xlsx file.");
            Console.WriteLine(result.ToString());
        }

        //TODO file upload success
    }
}
