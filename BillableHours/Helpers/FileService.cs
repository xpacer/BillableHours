using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace BillableHours.Helpers
{
    /// <summary>
    /// FileService
    /// </summary>
    public class FileService
    {
        private readonly IFileProvider _fileProvider;

        public FileService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        /// <summary>
        /// Returns file as a FileStream Result
        /// </summary>
        /// <param name="relativePath">Relative Path</param>
        /// <param name="contentType">Content Type</param>
        /// <returns></returns>
        public FileStreamResult GetFileAsStream(string relativePath, string contentType)
        {
            var stream = _fileProvider
                .GetFileInfo(relativePath)
                .CreateReadStream();

            return new FileStreamResult(stream, contentType);
        }
    }
}