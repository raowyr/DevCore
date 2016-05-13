using com.devevil.MVP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.devevil.MVP.Presenter
{
    public abstract class Presenter<V> : IPresenter where V : IView
    {
        public V View { get; set; }

        public Presenter(V _view)
        {
            if (_view == null)
                throw new ArgumentNullException();
            View = _view;
        }

        public abstract void initView();

        public abstract void handleException(Exception ex, bool rethrow);
    }
}
