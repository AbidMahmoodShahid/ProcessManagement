using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process.Editor.Elements
{
    public class TableAB
    {
        public int TableAId { get; set; }
        public TableA TableA { get; set; }

        public int TableBId { get; set; }
        public TableB TableB { get; set; }
    }
}
