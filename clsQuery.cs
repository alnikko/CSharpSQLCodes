using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.InteropServices;

class clsQuery : clsDBConnection
{
    #region Constructor
    public clsQuery()
        : base()
    {
    }
    #endregion

    #region Variables
    private SqlCommand sqlComm = new SqlCommand();
    private SqlDataAdapter sqlDataAdapter;
    private SqlTransaction sqlTrans;
    #endregion

    #region Global Variables
    public string[] par = new string[0];
    public object[] parValue = new object[0];
    public string newPK;
    #endregion

    #region Methods
    
    public void Dispose()
    {
        sqlComm.Dispose();
        sqlTrans.Dispose();
    }

    //Fetch Data as DataSet
    public DataSet FetchRecords(string[] arrFieldName, object[] arrFieldValue, string strStoredProcName)
    {
        bool fOk = true;
        clsQuery _clsQuery = new clsQuery();
        DataSet fDataSet = new DataSet();
        _clsQuery.par = arrFieldName;
        _clsQuery.parValue = arrFieldValue;
        try
        {
            _clsQuery.CreateOpenConnection();

            if (fOk == _clsQuery.ExecuteQueryDB(strStoredProcName))
            {
                fDataSet = _clsQuery.GetDataSet();
            }
            else
            {
                _clsQuery.RollBackTransaction(sqlTrans);
            }
            _clsQuery.CloseDestroyConnection();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            _clsQuery.Dispose();
            _clsQuery = null;
        }
        return fDataSet;
    }

    //Store Data
    public bool ExecuteQueryDB(string p_ProcedureQuery)
    {
        bool fOk = true;
        clsDBConnection _clsDBConnection = new clsDBConnection();
        try
        {
            _clsDBConnection.CreateOpenConnection();
            sqlComm.Connection = _clsDBConnection.GDBConn;
            sqlComm.CommandText = p_ProcedureQuery;
            sqlComm.CommandType = CommandType.StoredProcedure;
            sqlTrans = _clsDBConnection.GDBConn.BeginTransaction();
            sqlComm.Transaction = sqlTrans;
            if (par.Length > 0)
            {
                sqlComm.Parameters.Clear();
                for (int ctr = 0; ctr <= parValue.Length - 1; ctr++)
                {
                    var _sqlComm = sqlComm;
                    _sqlComm.Parameters.AddWithValue(par[ctr], parValue[ctr]);
                }
            }
            sqlComm.ExecuteNonQuery();
            sqlTrans.Commit();
            fOk = true;
        }
        catch (Exception ex)
        {
            fOk = false;
            MessageBox.Show(ex.Message);
        }
        return fOk;
    }

    //Store Data
    public bool ExecuteQueryDB(string p_ProcedureQuery, int returnType)
    {
        bool fOk = true;
        clsDBConnection _clsDBConnection = new clsDBConnection();
        int mark = 0;

        try
        {
            _clsDBConnection.CreateOpenConnection();
            sqlComm.Connection = _clsDBConnection.GDBConn;
            sqlComm.CommandText = p_ProcedureQuery;
            sqlComm.CommandType = CommandType.StoredProcedure;
            sqlTrans = _clsDBConnection.GDBConn.BeginTransaction();
            sqlComm.Transaction = sqlTrans;
            if (par.Length > 0)
            {
                sqlComm.Parameters.Clear();
                for (int ctr = 0; ctr <= parValue.Length - 1; ctr++)
                {
                    var _sqlComm = sqlComm;
                    if (par[ctr] == "newPK")
                    {
                        _sqlComm.Parameters.Add("newPK", SqlDbType.VarChar, 15).Direction = ParameterDirection.Output;
                        mark = int.Parse(parValue[ctr].ToString());
                    }
                    else
                    {
                        _sqlComm.Parameters.AddWithValue(par[ctr], parValue[ctr]);
                    }
                }
            }

            sqlComm.ExecuteNonQuery();
            if (mark == 1)
            {
                newPK = clsUtility.nullToEmptyString(sqlComm.Parameters["newPK"].Value);
            }
            sqlTrans.Commit();
        }
        catch (Exception ex)
        {
            fOk = false;
            MessageBox.Show(ex.Message);
        }
        return fOk;
    }
    
    /*
     * -- Gets Data from Database
     * -- Must Execute the ExecuteQueryDB() method first
     * before using GetDataSet() method.
    */

    public DataSet GetDataSet()
    {
        DataSet ds = new DataSet();

        try
        {
            sqlDataAdapter = new SqlDataAdapter(sqlComm);
            sqlDataAdapter.Fill(ds);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        finally
        {
            sqlDataAdapter.Dispose();
        }

        return ds;
    }
    #endregion
}