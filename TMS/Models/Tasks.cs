using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Models
{
    public partial class Tasks
    {
        public Tasks()
        {
            //SubTasks = new HashSet<SubTasks>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [StringLength(50)]
        public string State { get; set; }

        [InverseProperty("Task")]
        public virtual ICollection<SubTasks> SubTasks { get; set; }
    }
}
