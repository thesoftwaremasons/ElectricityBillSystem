using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Common.Exceptions
{
    public abstract class ElectrictyBillExceptions:Exception
    {
         public string Type { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public ElectrictyBillExceptions(string message)
            : base(message)
        {
            Detail = Message;
        }
    }
}
