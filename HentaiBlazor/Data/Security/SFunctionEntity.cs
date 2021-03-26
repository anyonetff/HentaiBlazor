using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Security
{
    [Index(nameof(Parent))]
    [Table("s_function")]
    public class SFunctionEntity : AbstractEntity
    {
        // [Required]
        [Key]
        [Column("f_id")]
        public override string Id { get; set; }

        [Required]
        [Column("f_parent")]
        public string Parent { get; set; }

        [Column("f_path")]
        public string Path { get; set; }

        [Column("f_name")]
        public string Name { get; set; }

        [Column("f_icon")]
        public string Icon { get; set; }

        [Column("f_note")]
        public string Note { get; set; }
    }
}
