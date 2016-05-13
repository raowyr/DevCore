using com.devevil.MVP.View;
using MVPExample.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPExample.View
{
    public interface IAccountView : IView
    {
        string Username { set; get; }
        string Password { set; get; }
        bool LogState { set; get; }
        void showMessage(string msg);
    }
}
