﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data.Basic
{
    [Table("b_catalog")]
    public class BCatalogEntity
    {
        [Key]
        [Column("c_id")]
        public string Id { get; set; }

        [Column("c_usage")]
        public string Usage { get; set; }

        [Column("c_path")]
        public string Path { get; set; }


    }
}
