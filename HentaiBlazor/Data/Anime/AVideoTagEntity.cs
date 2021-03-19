using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Anime
{
    [Table("a_video_tag")]
    public class AVideoTagEntity : AbstractEntity
    {
        [Key]
        [Column("vt_id")]
        public new string Id
        {
            get => base.Id;
            set
            {
                base.Id = this.Id;
            }
        }

        [Column("vt_book_id")]
        public string bookId { get; set; }

        [Column("vt_tag_name")]
        public string tagName { get; set; }
    }
}
