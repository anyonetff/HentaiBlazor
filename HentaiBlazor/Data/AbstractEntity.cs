using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data
{
    public class AbstractEntity
    {
        [Key]
        public string Id { get; set; }
    }
}
