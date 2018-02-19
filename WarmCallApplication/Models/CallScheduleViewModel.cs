using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WarmCallApplication.Models
{
    public class CallScheduleViewModel
    {
        public int Id { get; set; }
        public string ScheduleDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}