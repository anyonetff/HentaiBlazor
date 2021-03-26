using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Anime
{
    [Index(nameof(VideoId), nameof(TagName), IsUnique = true)]
    [Table("a_video_tag")]
    public class AVideoTagEntity : AbstractEntity
    {
        [Key]
        [Column("vt_id")]
        public override string Id { get; set; }

        [Required]
        [Column("vt_book_id")]
        public string VideoId { get; set; }

        [Required]
        [Column("vt_tag_name")]
        public string TagName { get; set; }
    }
}
