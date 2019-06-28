using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarlCoreBot2.Models
{
    public class InteractResponse
    {

        public DarlVar response { get; set; }

        public string darl { get; set; }

        public List<object> matches { get; set; }
        
    }
}
