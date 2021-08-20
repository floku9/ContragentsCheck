using System;
using System.Collections.Generic;
using System.Text;

namespace ContragentsCheck.Uipath.Models
{
    public class ErrorResponse
    {
        public string message { get; set; }
        public int errorCode { get; set; }
        public object resourceIds { get; set; }
    }
}
