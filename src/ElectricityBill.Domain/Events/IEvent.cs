﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Domain.Events
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime Timestamp { get; }
    }
}
