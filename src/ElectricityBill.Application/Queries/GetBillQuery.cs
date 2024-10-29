using ElectricityBill.Application.CQRS;
using ElectricityBill.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Application.Queries
{
    public class GetBillQuery : IQuery<BillDto>
    {
        public string ValidationReference { get; set; }
    }
}
