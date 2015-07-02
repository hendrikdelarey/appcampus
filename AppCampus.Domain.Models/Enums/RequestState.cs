﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCampus.Domain.Models.Enums
{
    public enum RequestState
    {
        Created,
        Sent,
        Processed,
        Cancelled,
        Failed
    }
}
