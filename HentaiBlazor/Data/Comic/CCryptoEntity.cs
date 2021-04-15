using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Comic
{
    [Index(nameof(Secret), IsUnique = true)]
    [Table("c_crypto")]
    public class CCryptoEntity : AbstractEntity
    {

        [Column("c_secret")]
        public string Secret { get; set; }

        [Column("c_note")]
        public string Note { get; set; }
    }
}
