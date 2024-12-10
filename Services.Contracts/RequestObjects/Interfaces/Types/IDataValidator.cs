﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.RequestObjects.Interfaces.Types
{
    public interface IDataValidator<T>
    {
        public bool Validate(object obj);
    }
}
