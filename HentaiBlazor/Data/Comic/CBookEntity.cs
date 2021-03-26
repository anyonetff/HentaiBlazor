using HentaiBlazor.Data.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Comic
{
    [Index(nameof(Path), nameof(Name), IsUnique = true)]
    [Table("c_book")]
    public class CBookEntity : AbstractEntity
    {
        [Key]
        [Column("b_id")]
        public override string Id { get; set; }

        [Required]
        [Column("b_path")]
        public string Path { get; set; }

        [Required]
        [Column("b_name")]
        public string Name { get; set; }

        [Column("b_title")]
        public string Title { get; set; }

        [Column("b_author")]
        public string Author { get; set; }

        [Column("b_language")]
        public string Language { get; set; }

        [Column("b_tags")]
        public string Tags { get; set; }

        [Column("b_length", TypeName = "BIGINT")]
        public long Length { get; set; }

        [Column("b_count")]
        public int Count { get; set; }

        [Column("b_index")]
        public int Index { get; set; }

        [Column("b_cover")]
        public string Cover { get; set; }

        [Column("b_preview")]
        public string Preview { get; set; }

        [Column("b_note")]
        public string Note { get; set; }


    }
}
