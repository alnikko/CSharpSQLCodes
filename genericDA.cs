using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectName.DataAccessClass
{
    class clsGenericDA
    {
        
        public static object manageQuery(string[] arrFieldName, object[] arrFieldValue, string strStoredProcName, byte operation)
        {
            clsQuery _clsQuery = new clsQuery();
            bool fOk;

            switch (operation)
            {
                case 0:
                    //Generic Fetch
                    return _clsQuery.fetchRecords(arrFieldName, arrFieldValue, strStoredProcName);
                case 1:
                    //Generic Insert
                    _clsQuery.par = arrFieldName;
                    _clsQuery.parValue = arrFieldValue;
                    fOk = _clsQuery.executeQueryDB(strStoredProcName);
                    _clsQuery.closeDestroyConnection();
                    return fOk;
                case 2:
                    //Generic Update
                    _clsQuery.createOpenConnection();
                    _clsQuery.par = arrFieldName;
                    _clsQuery.parValue = arrFieldValue;
                    _clsQuery.executeQueryDB(strStoredProcName);
                    _clsQuery.closeDestroyConnection();
                    break;
            }
            return 0;
        }
    }
}
