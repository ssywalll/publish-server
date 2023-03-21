using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using CleanArchitecture.Application.Common.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace CleanArchitecture.Application.FoodDrinkMenus.Commands.CreateFoodDrinkMenu
{
    public record CreateFoodDrinkMenuCommand : UseAprizax, IRequest<FoodDrinkMenu>
    {
        public string Name { get; init; } = string.Empty;
        public float Price { get; init; }
        public int Min_Order { get; init; }
        public string Description { get; init; } = string.Empty;
        public type Type { get; init; }

        [FileExtensions(Extensions = "jpg,png,jpeg")]
        public IFormFile? ImageUrl { get; set; }
    }

    public class CreateFoodDrinkMenuCommandHandler : IRequestHandler<CreateFoodDrinkMenuCommand, FoodDrinkMenu>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateFoodDrinkMenuCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FoodDrinkMenu> Handle(CreateFoodDrinkMenuCommand request, CancellationToken cancellationToken)
        {
            var entity = new FoodDrinkMenu
            {
                Name = request.Name,
                Price = request.Price,
                Min_Order = request.Min_Order,
                Description = request.Description,
                Type = request.Type,
            };

            if (request is null)
                throw new NotFoundException("Request kosong", HttpStatusCode.BadRequest);

            var tokenInfo = request.GetTokenInfo();

            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token tidak valid", HttpStatusCode.BadRequest);



            if (request.ImageUrl.ImageValidate() is false)
                throw new NotFoundException("Ekstensi berkas bukan merupkan ekstensi gambar yang diperbolehkan", HttpStatusCode.BadRequest);

            if (request.ImageUrl.SizeValidate() is false)
                throw new NotFoundException("Ukuran berkas melebihi 2MB", HttpStatusCode.BadRequest);

            var dateName = DateTime.Now.ToString("yyyy-MM-dd");
            var fileExtension = Path.GetExtension(request.ImageUrl!.FileName);
            var fileName = $"{entity.Id}-avatar-{dateName}{fileExtension}";
            var myPath = Path.Combine("menu", fileName);

            using (var stream = File.Create(myPath.GetFullPath()))
            {
                await request.ImageUrl.CopyToAsync(stream, cancellationToken);
            }

            entity.Image_Url = myPath;

            return await Aprizax.Insert<FoodDrinkMenu>
            (_context, _context.FoodDrinkMenus, entity, cancellationToken);
        }

        //konteks, tabel konteks, record baru, cancelationToken
    }
}