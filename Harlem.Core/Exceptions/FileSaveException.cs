using System;
using System.Collections.Generic;
using System.Text;

namespace Harlem.Core.Exceptions
{
    [Serializable]
    public class FileSaveException:Exception
    {
        public FileSaveException() : base("File not found check this path.Maybe use wrong path")
        {
        }
    }
}
