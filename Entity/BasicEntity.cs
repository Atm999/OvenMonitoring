using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class BasicEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
    }
}
