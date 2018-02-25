using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    /// <summary>
    /// 透传数据返回的类型   广岱http
    /// </summary>
    class PassthroughResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public string hex { get; set; }

        /// <summary>
        /// 返回的数据是否有误
        /// </summary>
        /// <returns></returns>
        public bool isError() {
            if (code != 0 || hex == null)
            {
                return true;
            }
            else {
                return false;
            }
        }
    }

    
}
