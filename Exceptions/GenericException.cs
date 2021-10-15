using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    public abstract class GenericException: Exception
    {
        public override string Message
        {
            get { return "API." + this.GetType().Name; }
        }
        
    }
}
