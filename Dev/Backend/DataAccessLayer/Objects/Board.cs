using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer.Objects;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{
    class Board
    {
        public List<Column> Columns { get; set; }
        public string EmailAssignee { get; set; }
        public int BoardId { get; set; }
    }
}
