using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Basic
{
    [Table("b_author")]
    public class BAuthorEntity : AbstractEntity
    {

        [Key]
        [Column("a_id")]
        public new string Id
        {
            get => base.Id;
            set
            {
                base.Id = this.Id;
            }
        }

        [Required]
        [Column("a_name")]
        public string Name { get; set; }

        [Required]
        [Column("a_alias")]
        public string Alias { get; set; }

        [Column("a_valid", TypeName = "BOOLEAN")]
        public bool Valid { get; set; }

        [Column("a_items")]
        public int Items { get; set; }

        [Column("a_note")]
        public string Note { get; set; }

    }
}
