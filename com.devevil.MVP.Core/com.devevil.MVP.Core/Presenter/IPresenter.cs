using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.devevil.MVP.Presenter
{
    public interface IPresenter
    {
        void initView();
        void handleException(Exception ex, bool rethrow);
    }
}
