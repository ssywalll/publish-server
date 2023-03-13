using CleanArchitecture.Domain.Entities;
using MediatR;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Exceptions;
using System.Net;
using System.IO;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CleanArchitecture.Application.Images
{
    public record ReadImageQueries : IRequest<FileStreamResult>
    {
        public string FileName { get; init; }
    }

    public class ReadImageQueriesHandler : IRequestHandler<ReadImageQueries, FileStreamResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReadImageQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FileStreamResult> Handle(ReadImageQueries request, CancellationToken cancellationToken)
        {
            // if (request is null)
            //     throw new NotFoundException("Request kosong", HttpStatusCode.BadRequest);

            // var tokenInfo = request.GetTokenInfo();

            // if (tokenInfo.Is_Valid is false)
            //     throw new NotFoundException("Token tidak valid", HttpStatusCode.BadRequest);

            var myFileName = request.FileName;

            var path = Directory.GetCurrentDirectory();

            var path2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var path3 = Path.GetTempFileName();

            var imagePath = Path.Combine(path2, "images");

            var isPathExist = Directory.Exists(imagePath);

            if (isPathExist is false)
                Directory.CreateDirectory(imagePath);

            var myPath = Path.Combine(imagePath, myFileName);

            // var stream = await File.ReadAllBytesAsync(myPath, cancellationToken);
            var stream2 = File.OpenRead(myPath);
            // var stream3 = await File.ReadAs(myPath, cancellationToken);
            // await myFile.CopyToAsync(stream);

            // var fileBuffer = new RequestFie

            // var fileStream = new FileStream(Path.Combine(path2, "images", myFile.FileName), FileMode.CreateNew, FileAccess.Write);

            // fileStream.WriteAsync(myFile.Length, 0, 1, cancellationToken);

            // await _context.SaveChangesAsync(cancellationToken);
            return new FileStreamResult(stream2, "images/*");
        }
    }
}