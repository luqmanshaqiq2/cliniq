using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.MODELS.ENUMs;

namespace Cliniq.MODELS.DTOs.Payment
{
    public class PaymentUpdateDTO
    {
        public decimal? Amount { get; set; }
        public PaymentMethod? Method { get; set; }
        public PaymentStatus? Status { get; set; }
    }
}