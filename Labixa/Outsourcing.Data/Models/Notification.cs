using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class Notification:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public int TypeNotifyId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool isRead { get; set; }
        public bool IsDelete { get; set; }
        public bool isUrgent { get; set; }

        public virtual TypeNotify TypeNotify { get; set; }

    }
}
