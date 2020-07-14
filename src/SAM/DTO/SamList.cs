using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAM.DTO
{
    public class SamList<T>
    {
        public int count { get; set; }
        public List<T> data { get; set; }
    }
}
