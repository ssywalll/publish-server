using MediatR;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Context;
using AutoMapper;
using System.Net;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace CleanArchitecture.Application.Carts.Queries.GetCarts
{
    public record CheckoutPreview : UseAprizax, IRequest<CheckoutPreviewVm>;

    public class CheckoutPreviewHandler : IRequestHandler<CheckoutPreview, CheckoutPreviewVm>
    {
        private readonly IApplicationDbContext _context;
        private IMapper _mapper;
        public CheckoutPreviewHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CheckoutPreviewVm> Handle(CheckoutPreview request, CancellationToken cancellationToken)
        {

            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);

            var tokenInfo = request.GetTokenInfo();

            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token tidak ditemukan", HttpStatusCode.BadRequest);

            var carts = await _context.Carts
                .Where(y => y.IsChecked && y.User_Id.Equals(tokenInfo.Owner_Id))
                .AsNoTracking()
                .ProjectTo<CheckoutCartDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (carts is null)
                throw new NotFoundException("Keranjang anda bermasalah", HttpStatusCode.BadRequest);

            if (carts.Count == 0)
                return new CheckoutPreviewVm
                {
                    Status = "Ok",
                    Data = new CheckoutPreviewDto()
                };

            var usedBank = await _context.BankAccounts
                .Where(x => x.UserId.Equals(tokenInfo.Owner_Id) && x.IsChoosen)
                .AsNoTracking()
                .ProjectTo<CheckoutBankDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            var totalCartsPrice = await _context.Carts
                .Where(x => x.IsChecked && x.User_Id.Equals(tokenInfo.Owner_Id))
                .AsNoTracking()
                .SumAsync(y => y.Quantity * y.FoodDrinkMenu!.Price);

            return new CheckoutPreviewVm
            {
                Status = "Ok",
                Data = new CheckoutPreviewDto
                {
                    TotalPrice = totalCartsPrice,
                    UsedBank = usedBank,
                    Carts = carts
                }
            };
        }
    }
}