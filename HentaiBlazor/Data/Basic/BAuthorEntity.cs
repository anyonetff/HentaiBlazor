using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Basic
{
    [Table("b_author")]
    public class BAuthorEntity
    {

        [Key]
        [Column("a_id")]
        public string Id { get; set; }

        [Column("a_name")]
        public string Name { get; set; }

    }
}
