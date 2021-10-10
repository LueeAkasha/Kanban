using IntroSE.Kanban.Backend.DataAccessLayer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{
    class Column : ColumnController //to deal with database
    {
        public List<Task> Tasks { get; set; }
        public string Name { get; set; }
        public int Limit { get; set; }
        public int ColumnOrdinal { get; set; }
        
    }
}
