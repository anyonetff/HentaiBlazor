using HentaiBlazor.Data.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Anime
{
    [Index(nameof(Path), nameof(Name), IsUnique = true)]
    [Table("a_video")]
    public class AVideoEntity : AbstractEntity
    {
        [Key]
        [Column("v_id")]
        public override string Id { get; set; }

        [Required]
        [Column("v_path")]
        public string Path { get; set; }

        [Required]
        [Column("v_name")]
        public string Name { get; set; }

        [Column("v_title")]
        public string Title { get; set; }

        [Column("v_author")]
        public string Author { get; set; }

        [Column("v_language")]
        public string Language { get; set; }

        [Column("v_tags")]
        public string Tags { get; set; }

        [Column("v_length", TypeName = "BIGINT")]
        public long Length { get; set; }

        [Column("v_cover")]
        public string Cover { get; set; }

        [Column("v_subtitle")]
        public string Subtitle { get; set; }

        [Column("v_favorite", TypeName = "BOOLEAN")]
        public bool Favorite { get; set; }

        [Column("v_note")]
        public string Note { get; set; }
    }
}
