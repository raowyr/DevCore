using com.devevil.MVP.View;
using com.devevilz.Example.MVPExample.Presenter;

namespace com.devevilz.Example.MVPExample.View
{
    public interface IAccountView : IView<IAccountPresenter>
    {
        string Username { set; get; }
        string Password { set; get; }
        bool LogState { set; get; }
        void showMessage(string msg);
    }
}
