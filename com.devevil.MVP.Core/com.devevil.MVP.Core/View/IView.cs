using com.devevil.MVP.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.devevil.MVP.View
{
    public interface IView<P> where P :IPresenter
    {
        P Presenter { get; set; }
        //void showMessage(string _message);
        //void showMessage(string _message, MVPEnumerations.MessageType _msgType);
    }
}
