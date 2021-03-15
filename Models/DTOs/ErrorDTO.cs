using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies_Mistral.Models.DTOs
{
    public class ErrorDTO
    {
        public bool Error { get; set; }
        public bool Successful => !Error;
        public string ErrorMessage { get; set; }
    }
}
