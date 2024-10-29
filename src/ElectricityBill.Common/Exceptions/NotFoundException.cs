using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Common.Exceptions
{
    public  class NotFoundException:ElectrictyBillExceptions
    {
        public NotFoundException(string message) : base(message)
        {
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4";
            Status = (int)HttpStatusCode.NotFound;
            Title = "Not FOund";
        }
    }
}
