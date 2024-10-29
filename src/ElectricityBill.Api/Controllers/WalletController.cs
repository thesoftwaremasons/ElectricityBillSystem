using ElectricityBill.Api.Models;
using ElectricityBill.Application.Commands;
using ElectricityBill.Application.CQRS;
using ElectricityBill.Application.DTOs;
using ElectricityBill.Application.Queries;
using ElectricityBill.Common;
using Microsoft.AspNetCore.Mvc;

namespace ElectricityBill.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly ICommandHandler<AddWalletFundsCommand, Unit> _addFundsHandler;
        private readonly IQueryHandler<GetWalletQuery, WalletDto> _getWalletQueryHandler;
        private readonly ICommandHandler<VerifyBillCommand, string> _verifyBillHandler;

        public WalletController(
            ICommandHandler<AddWalletFundsCommand,Unit> addFundsHandler,
            IQueryHandler<GetWalletQuery, WalletDto> getWalletQueryHandler)
        {
            _addFundsHandler = addFundsHandler;
            _getWalletQueryHandler = getWalletQueryHandler;
        }

        [HttpPost("{id}/add-funds")]
        public async Task<IActionResult> AddFunds(Guid id, [FromBody] AddFundsRequest request)
        {
            var command = new AddWalletFundsCommand
            {
                WalletId = id,
                Amount = request.Amount
            };
            //await _verifyBillHandler.HandleAsync(request);
            await _addFundsHandler.HandleAsync(command);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWallet(Guid id)
        {
            var query = new GetWalletQuery { WalletId = id };
            var wallet = await _getWalletQueryHandler.HandleAsync(query);
            return Ok(wallet);
        }
    }
}
