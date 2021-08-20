using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContragentsCheck.Infrastructure.Models
{
    public class Request
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указан ИНН")]
        [RegularExpression(@"^(\d{10}|\d{12})$", ErrorMessage = "ИНН должен содержать 10 или 12 цифр")]
        public string Inn { get; set; }
        public int StatusId { get; set; }
        public int? ReportId { get; set; }

        public virtual Report Report { get; set; }
        public virtual Status Status { get; set; }
    }
}
