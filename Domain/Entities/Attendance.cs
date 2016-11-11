using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Attendance
    {
        public long Id { get; set; }
        public int FingerPrint { get; set; }
        public DateTime CheckedInAt { get; set; }
    }
}
