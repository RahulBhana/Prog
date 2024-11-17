using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipleServiceApp.Models
{
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdated { get; set; }

        public int CompareTo(ServiceRequest other)
        {
            return ID.CompareTo(other.ID);
        }
    }
}