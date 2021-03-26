using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Comic
{
    [Index(nameof(BookId), nameof(TagName), IsUnique = true)]
    [Table("c_book_tag")]
    public class CBookTagEntity : AbstractEntity
    {
        [Key]
        [Column("bt_id")]
        public override string Id { get; set; }

        [Required]
        [Column("bt_book_id")]
        public string BookId { get; set; }

        [Required]
        [Column("bt_tag_name")]
        public string TagName { get; set; }

    }
}
