using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Security
{
    [Index(nameof(Username), IsUnique = true)]
    [Table("s_user")]
    public class SUserEntity : AbstractEntity
    {
        [Key]
        [Column("u_id")]
        public override string Id { get; set; }

        [Required]
        [Column("u_username")]
        public string Username { get; set; }

        [Column("u_password")]
        public string Password { get; set; }

        [Required]
        [NotMapped]
        public string PasswordNew { get; set; }

        [Required]
        [NotMapped]
        public string PasswordConfirm { get; set; }


        [Column("u_name")]
        public string Name { get; set; }

        [Column("u_avatar")]
        public string Avatar { get; set; }

        [Column("u_note")]
        public string Note { get; set; }
    }
}
