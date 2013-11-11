using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class clsDBConnection
{

#region "Variables"
    SqlConnection DBCon = new SqlConnection();
    string connectionString = "YourConnectionString";
	
#endregion

#region "Properties"
    public SqlConnection GDBConn
    {
        get { return DBCon; }
    }
#endregion

#region "Methods"
    public void CreateOpenConnection()
    {
        DBCon.ConnectionString = connectionString;
        DBCon.Open();
    }

    public void CloseDestroyConnection()
    {
        if (!(DBCon.State == ConnectionState.Closed))
        {
            DBCon.Close();
        }
    }

    /**************************************************************
     *Author: Anthony Trasporto VB.net
     *Added Date: March 21, 2013
     **************************************************************/
    public SqlTransaction BeginTransaction()
    {
        SqlTransaction myTrans = null;
        if (!(DBCon.State == ConnectionState.Closed))
        {
            myTrans = DBCon.BeginTransaction();
        }
        return myTrans;
    }

    public void CommitTransaction(SqlTransaction myTrans)
    {
        if (!(DBCon.State == ConnectionState.Closed))
        {
            myTrans.Commit();
        }
    }

    public void RollbackTransaction(SqlTransaction myTrans)
    {
        if (!(DBCon.State == ConnectionState.Closed))
        {
            myTrans.Rollback();
        }
    }
#endregion

}