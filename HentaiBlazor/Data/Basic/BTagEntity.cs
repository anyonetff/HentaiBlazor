using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Basic
{
    [Index(nameof(Name), IsUnique = true)]
    [Table("b_tag")]
    public class BTagEntity : AbstractEntity
    {
        [Key]
        [Column("t_id")]
        public override string Id { get; set; }

        [Required]
        [Column("t_name")]
        public string Name { get; set; }

        [Column("t_alias")]
        public string Alias { get; set; }

        [Column("t_items")]
        public int Items { get; set; }

        [Column("t_note")]
        public string Note { get; set; }

    }
}
