using System;
using System.Collections.Generic;
using System.Text;

namespace Harlem.Core.Tools
{
   public  class MagicString
    {
        private static MagicString _singletonObject;
        private List<string> stringClauses;

        private MagicString()
        {
           
        }
        public MagicString Instance()
        {
            if (_singletonObject==null)
            {
                _singletonObject = new MagicString();
                
            }
            return _singletonObject;
        }
        
    }
    
}
