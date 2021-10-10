using IntroSE.Kanban.Backend.DataAccessLayer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{
    class Task : TaskController //to deal with database
    {
        public int Id { get; set; }
        public string CreationTime { get; set; }
        public string DueDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string EmailAssignee { get; set; }
    }
}
