﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CyptoCloud_WS
{
    [ServiceContract]
    public interface ITest
    {
        [OperationContract]
        string Hello();
    }
}
