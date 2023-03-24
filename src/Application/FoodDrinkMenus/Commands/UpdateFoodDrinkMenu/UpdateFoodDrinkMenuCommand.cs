using MediatR;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using System.Net;
using CleanArchitecture.Application.Common.Context;

namespace CleanArchitecture.Application.FoodDrinkMenus.Commands.UpdateFoodDrinkMenu
{
    public record UpdateFoodDrinkMenuCommand : UseAprizax, IRequest<FoodDrinkMenuDto>
    {
        public IFormFile? ImageUrl { get; set; }
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public float Price { get; init; }
        public int MinOrder { get; init; }
        public string Description { get; init; } = string.Empty;
    }

    public class UpdateFoodDrinkMenuCommandHandler : IRequestHandler<UpdateFoodDrinkMenuCommand, FoodDrinkMenuDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateFoodDrinkMenuCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FoodDrinkMenuDto> Handle(UpdateFoodDrinkMenuCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);
            var tokenInfo = request.GetTokenInfo();
            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token anda tidak valid!", HttpStatusCode.BadRequest);
            var target = await _context.FoodDrinkMenus
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (target == null)
                throw new NotFoundException(nameof(FoodDrinkMenus), request.Id);
            _context.FoodDrinkMenus.Update(target);

            target.Name = request.Name;
            target.Price = request.Price;
            target.Min_Order = request.MinOrder;
            target.Description = request.Description;

            if (request.ImageUrl is null)
            {
                await _context.SaveChangesAsync(cancellationToken);

                return await _context.FoodDrinkMenus
                .Where(x => x.Id.Equals(request.Id))
                .AsNoTracking()
                .ProjectTo<FoodDrinkMenuDto>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);
            }


            if (request.ImageUrl.ImageValidate() is false)
                throw new NotFoundException("Ekstensi berkas bukan merupkan ekstensi gambar yang diperbolehkan", HttpStatusCode.BadRequest);

            if (request.ImageUrl.SizeValidate() is false)
                throw new NotFoundException("Ukuran berkas melebihi 2MB", HttpStatusCode.BadRequest);

            var dateName = DateTime.Now.ToString("yyyy-MM-dd");
            var fileExtension = Path.GetExtension(request.ImageUrl!.FileName);
            var fileName = $"{request.Id}-menu-{dateName}{fileExtension}";
            var myPath = Path.Combine("menu", fileName);

            if (File.Exists(target.Image_Url.GetFullPath()))
            {
                File.Delete(target.Image_Url.GetFullPath());
            }

            using (var stream = File.Create(myPath.GetFullPath()))
            {
                await request.ImageUrl.CopyToAsync(stream, cancellationToken);
            }

            target.Image_Url = myPath;

            await _context.SaveChangesAsync(cancellationToken);

            return await _context.FoodDrinkMenus
               .Where(x => x.Id.Equals(request.Id))
               .AsNoTracking()
               .ProjectTo<FoodDrinkMenuDto>(_mapper.ConfigurationProvider)
               .SingleAsync(cancellationToken);
        }
    }
}