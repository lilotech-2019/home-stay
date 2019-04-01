using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class TypeNotify : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public int PriorityOrder{get;set;}
        public bool IsDelete { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

    }
}
