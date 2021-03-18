using HentaiBlazor.Data.Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Anime
{
    [Table("a_video")]
    public class AVideoEntity : AbstractEntity
    {
        [Key]
        [Column("v_id")]
        public new string Id { get; set; }

        [Column("v_path")]
        public string Path { get; set; }

        [Column("v_name")]
        public string Name { get; set; }

        [Column("v_title")]
        public string Title { get; set; }

        [Column("v_author")]
        public string Author { get; set; }

        [Column("v_tags")]
        public string Tags { get; set; }

        [Column("v_count")]
        public int Length { get; set; }

        [Column("v_cover")]
        public string Cover { get; set; }

        [Column("v_subtitle")]
        public string Subtitle { get; set; }

        [Column("v_note")]
        public string Note { get; set; }
    }
}
