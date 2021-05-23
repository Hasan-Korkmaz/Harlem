using System;
using System.Collections.Generic;
using System.Text;

namespace Harlem.Core.Tools
{
    public class Result<TEntity>
    {
        public TEntity Entity { get; set; }
        public Enums.BLLResultType Status { get; set; }
        public string Message{ get; set; }
    }
}
