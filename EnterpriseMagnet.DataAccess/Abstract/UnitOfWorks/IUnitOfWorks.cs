﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseMagnet.DataAccess.Abstract.UnitOfWorks
{
    public interface IUnitOfWorks
    {

        Task CommitAsync();

        void Commit();
    }
}
