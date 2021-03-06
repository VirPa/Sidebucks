﻿using System.Data;
using System.Data.SqlClient;

namespace Sidebucks.BLL.v1.DataManagers {
    public abstract class BaseDataManager {

        protected IDbConnection GetSysDbConnection(string dbConnection) {
            return new SqlConnection(dbConnection);
        }
    }
}
