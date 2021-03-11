using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Basic
{
    [Table("b_tag")]
    public class BTagEntity
    {
        [Key]
        [Column("t_id")]
        public string Id { get; set; }

        [Column("t_name")]
        public string Name { get; set; }

    }
}
