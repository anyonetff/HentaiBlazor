using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Basic
{
    [Table("b_tag")]
    public class BTagEntity : AbstractEntity
    {
        [Key]
        [Column("t_id")]
        public new string Id
        {
            get => base.Id;
            set
            {
                base.Id = this.Id;
            }
        }

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
