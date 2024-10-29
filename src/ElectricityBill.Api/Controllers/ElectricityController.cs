using ElectricityBill.Api.Models;
using ElectricityBill.Application.Commands;
using ElectricityBill.Common;
using ElectricityBill.Application.CQRS;
using ElectricityBill.Application.DTOs;
using ElectricityBill.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Asn1.X509;

namespace ElectricityBill.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElectricityController : ControllerBase
    {
        private readonly ICommandHandler<VerifyBillCommand,string> _verifyBillHandler;
        private readonly ICommandHandler<ProcessPaymentCommand,Unit> _processPaymentHandler;
        private readonly ICommandHandler<AddWalletCommand, Guid> _addWalletCommand;
        private readonly IQueryHandler<GetBillQuery, BillDto> _getBillQueryHandler;

        public ElectricityController(
            ICommandHandler<VerifyBillCommand, string> verifyBillHandler,
            ICommandHandler<ProcessPaymentCommand, Unit> processPaymentHandler,
            IQueryHandler<GetBillQuery, BillDto> getBillQueryHandler,
            ICommandHandler<AddWalletCommand, Guid> addWalletCommand)
        {
            _verifyBillHandler = verifyBillHandler;
            _processPaymentHandler = processPaymentHandler;
            _getBillQueryHandler = getBillQueryHandler;
            _addWalletCommand = addWalletCommand;
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] VerifyBillCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validationReference = await _verifyBillHandler.HandleAsync(command);
            var bill = await _getBillQueryHandler.HandleAsync(new GetBillQuery { ValidationReference = validationReference });
            return Ok(bill);
        }

        [HttpPost("vend/{validationRef}/pay")]
        public async Task<IActionResult> ProcessPayment(string validationRef, [FromBody] ProcessPaymentRequest request)
        {
            var command = new ProcessPaymentCommand
            {
                ValidationReference = validationRef,
                WalletId = request.WalletId,
                PhoneNumber = request.PhoneNumber
            };
            await _processPaymentHandler.HandleAsync(command);
            return Ok();
        }
        [HttpPost("wallet")]
        public async Task<ActionResult> AddWallet(AddWalletRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var command = new AddWalletCommand() { OwnerName = request.OwnerName,PhoneNumber=request.PhoneNumber,InitialBalance=request.InitialBalance };
              
              var id=  await _addWalletCommand.HandleAsync(command);
               ;

                return Ok(new AddWalletResponse
                {
                    WalletId = id
                });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
