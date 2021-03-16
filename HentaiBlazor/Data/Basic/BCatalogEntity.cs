using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Basic
{
    [Table("b_catalog")]
    public class BCatalogEntity : AbstractEntity
    {
        [Key]
        [Column("c_id")]
        public new string Id { get; set; }

        [Required]
        [Column("c_usage")]
        public string Usage { get; set; }

        [Required]
        [Column("c_path")]
        public string Path { get; set; }


    }
}
