using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class MyException:Exception
    {
        public string msg { get; set; }

        public MyException(string msg)
            : base(msg)
        {
            this.msg = msg;
        }
    }
}
