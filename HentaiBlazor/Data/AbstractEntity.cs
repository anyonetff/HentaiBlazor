using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Data
{
    /**
     * 实体基类.
     * <p>
     * 为简化DAO层的一些固定封装，定义了实体基类.
     * </p>
     */
    public abstract class AbstractEntity : ICloneable
    {
        // 子类继承后可以定义字段名字
        [Key]
        [Comment("主键编号")]
        public virtual string Id { get; set; }

        [Column("x_insert_", TypeName = "DATETIME")]
        [Comment("创建时间")]
        public DateTime XInsert_ { get; set; }

        [Column("x_update_", TypeName = "DATETIME")]
        [Comment("修改时间")]
        public DateTime XUpdate_ { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
