using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Security
{
    [Table("s_user")]
    public class SUserEntity : AbstractEntity
    {
        [Key]
        [Column("u_id")]
        public new string Id
        {
            get => base.Id;
            set
            {
                base.Id = this.Id;
            }
        }

        [Column("u_username")]
        public string Username { get; set; }

        [Column("u_password")]
        public string Password { get; set; }

        [Column("u_name")]
        public string Name { get; set; }

        [Column("u_avatar")]
        public string Avatar { get; set; }

        [Column("u_note")]
        public string Note { get; set; }
    }
}
