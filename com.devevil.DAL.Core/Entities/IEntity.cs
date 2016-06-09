using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.devevil.DAL.Core.Entities
{
    public interface IEntity<TId>
    {
        TId Id
        {
            get;
            set;
        }

        bool IsTransient();

        void ValidateState();

        IList<string> WrongStates { get; }
    }
}
