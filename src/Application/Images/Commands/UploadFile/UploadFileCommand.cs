using CleanArchitecture.Domain.Entities;
using MediatR;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Exceptions;
using System.Net;
using System.IO;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.Images
{
    public record UploadFileCommand : UseAprizax, IRequest
    {
        [FileExtensions(Extensions = "jpg,png,jpeg")]
        public IFormFile? MyFile { get; init; }
    }

    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UploadFileCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request kosong", HttpStatusCode.BadRequest);

            var tokenInfo = request.GetTokenInfo();

            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token tidak valid", HttpStatusCode.BadRequest);

            var myFile = request.MyFile;

            var path = Directory.GetCurrentDirectory();

            var path2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var path3 = Path.GetTempFileName();

            var imagePath = Path.Combine(path2, "images");

            var isPathExist = Directory.Exists(imagePath);

            if (isPathExist is false)
                Directory.CreateDirectory(imagePath);

            var myPath = Path.Combine(imagePath, myFile.FileName);

            using (var stream = File.Create(myPath))
            {
                await myFile.CopyToAsync(stream, cancellationToken);
            }

            // var fileBuffer = new RequestFie

            // var fileStream = new FileStream(Path.Combine(path2, "images", myFile.FileName), FileMode.CreateNew, FileAccess.Write);

            // fileStream.WriteAsync(myFile.Length, 0, 1, cancellationToken);

            // await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}