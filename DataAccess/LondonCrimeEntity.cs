using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
   public class LondonCrimeEntity
    {
        public long Id { get; set; }
        public string lsoa_code { get; set; }
        public string borough { get; set; }
        public string major_category { get; set; }
        public string minor_category { get; set; }
        public string value { get; set; }
        public string year { get; set; }
        public string month { get; set; }
    }
}
