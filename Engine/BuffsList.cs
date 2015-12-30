using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class BuffsList
    {
        public Buff Details { get; set; }
        public int Amount { get; set; }
        public BuffsList (Buff details, int amount)
        {
            Details = details;
            Amount = amount;
        }
    }
}
