﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.devevil.DAL.Core.Transaction
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}
