using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Basic
{
    [Index(nameof(Secret), IsUnique = true)]
    [Table("b_crypto")]
    public class BCryptoEntity : AbstractEntity
    {

        [Key]
        [Column("c_id")]
        public override string Id { get; set; }

        [Column("c_secret")]
        public string Secret { get; set; }

        [Column("c_note")]
        public string Note { get; set; }
    }
}
