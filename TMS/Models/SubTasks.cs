using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TMS.Models
{
    public partial class SubTasks
    {
        [Key]
        [Column("ID")]
        public int Id {get; set; }
        [Column("TaskID")]
        public int? TaskId { get; set; }
        [StringLength(15)]
        public string State { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(TaskId))]
        [InverseProperty(nameof(Tasks.SubTasks))]
        public virtual Tasks Task { get; set; }
    }
}
