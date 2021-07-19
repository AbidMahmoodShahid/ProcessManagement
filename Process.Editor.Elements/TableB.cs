using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process.Editor.Elements
{
    public class TableB
    {
        public int TableBId { get; set; }
        public string Name { get; set; }


        public IList<TableAB> TableAB { get; set; }
    }
}
