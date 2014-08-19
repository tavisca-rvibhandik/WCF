using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EmployeeService
{
    [DataContract]
    public class FaultExceptionContract
    {
        [DataMember]
        public string Message { get; set; }
    }
}
