using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrSystem.Application.Exceptions
{
    public class DuplicateEmailException : BusinessRuleException
    {
        public DuplicateEmailException(string message) : base(message)
        {
        }
    }
}