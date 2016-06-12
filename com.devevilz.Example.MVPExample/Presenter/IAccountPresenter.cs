using com.devevil.MVP.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.devevilz.Example.MVPExample.Presenter
{
    public interface IAccountPresenter : IPresenter
    {
        void handleLogin();
        void handleCreateAccount();
        void handleModfyAccount();
    }
}
