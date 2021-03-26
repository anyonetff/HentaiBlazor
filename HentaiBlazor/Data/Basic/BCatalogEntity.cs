using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Basic
{
    [Index(nameof(Usage), nameof(Path), IsUnique = true)]
    [Table("b_catalog")]
    public class BCatalogEntity : AbstractEntity
    {
        [Key]
        [Column("c_id")]
        public override string Id { get; set; }

        [Required]
        [Column("c_usage")]
        public string Usage { get; set; }

        [Required]
        [Column("c_path")]
        public string Path { get; set; }

        [Column("c_children", TypeName = "BOOLEAN")]
        public bool Children { get; set; }

        [Column("c_items")]
        public int Items { get; set; }

        [Column("c_note")]
        public string Note { get; set; }
    }
}
