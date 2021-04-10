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
    [Table("b_producer")]
    public class BProducerEntity : AbstractEntity
    {

        [Key]
        [Column("p_id")]
        public override string Id { get; set; }

        [Required]
        [Column("p_name")]
        public string Name { get; set; }

        [Required]
        [Column("p_alias")]
        public string Alias { get; set; }

        [Column("p_valid", TypeName = "BOOLEAN")]
        public bool Valid { get; set; }

        [Column("p_items")]
        public int Items { get; set; }

        [Column("p_note")]
        public string Note { get; set; }

    }
}
