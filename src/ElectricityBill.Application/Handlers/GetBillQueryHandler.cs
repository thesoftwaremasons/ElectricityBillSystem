using ElectricityBill.Application.CQRS;
using ElectricityBill.Application.DTOs;
using ElectricityBill.Application.Queries;
using ElectricityBill.Common.Exceptions;
using ElectricityBill.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Application.Handlers
{
    public class GetBillQueryHandler : IQueryHandler<GetBillQuery, BillDto>
    {
        private readonly IBillRepository _billRepository;

        public GetBillQueryHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public async Task<BillDto> HandleAsync(GetBillQuery query)
        {
            var bill = await _billRepository.GetByValidationReferenceAsync(query.ValidationReference);
            if (bill == null)
                throw new NotFoundException($"Bill with validation reference {query.ValidationReference} not found");

            return new BillDto
            {
                Id = bill.Id,
                Amount = bill.Amount,
                Status = bill.Status.ToString(),
                ValidationReference = bill.ValidationReference,
                CreatedAt = bill.CreatedAt,
                PaidAt = bill.PaidAt
            };
        }
    }
}
