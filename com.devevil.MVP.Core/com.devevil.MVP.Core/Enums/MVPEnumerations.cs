using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.devevil.MVP.Enums
{
    public static class MVPEnumerations
    {
        public enum MessageType
        {
            Information,
            Error,
            Warning,
            Debug
        }

        public enum LayoutType
        {
            MessageBox,
            PlainText
        }
    }
}
