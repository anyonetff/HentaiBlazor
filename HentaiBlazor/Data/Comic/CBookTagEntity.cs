using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Comic
{
    [Table("c_book_tag")]
    public class CBookTagEntity : AbstractEntity
    {
        [Key]
        [Column("bt_id")]
        public new string Id
        {
            get => base.Id;
            set
            {
                base.Id = this.Id;
            }
        }

        [Column("bt_book_id")]
        public string bookId { get; set; }

        [Column("bt_tag_name")]
        public string tagName { get; set; }

    }
}
