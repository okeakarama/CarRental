using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cars.Models
{
    public class log
    {
        public int logid { get; set; }
        public string customername { get; set; }
        public string customeremail { get; set; }
        public string customertel { get; set; }
        public string leasedate { get; set; }
        public string retudate { get; set; }
        public int carid { get; set; }
        public string carname { get; set; }
    }
}