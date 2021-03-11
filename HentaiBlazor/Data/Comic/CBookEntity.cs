using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Comic
{
    [Table("c_book")]
    public class CBookEntity
    {
        [Key]
        [Column("b_id")]
        public string Id { get; set; }

        [Column("b_name")]
        public string Name { get; set; }

        [Column("b_title")]
        public string Title { get; set; }

        [Column("b_author")]
        public string Author { get; set; }

        [Column("b_tags")]
        public string Tags { get; set; }

        [Column("b_count")]
        public int Count { get; set; }

        [Column("b_index")]
        public int Index { get; set; }

        [Column("b_cover")]
        public string Cover { get; set; }

        [Column("b_preview")]
        public string Preview { get; set; }

        [Column("x_insert_")]
        public DateTime XInsert_ { get; set; }

        [Column("x_update_")]
        public DateTime XUpdate_ { get; set; }
    }
}
