using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace acumaticaHardware.Data_Access
{
    class clsUtility
    {
        SqlParameter _sqlParam;

        public static bool isAlpha(string strInput)
        {
            /*
             * Does string contain only alphabetic characters and or a space?
             * Use for validating a persons name
             */
            bool bln;

            bln = Regex.IsMatch(strInput, "^[a-zA-Z ]+$");
            return bln;
        }

        public static bool isAlphaNumeric(string strInput)
        {
            /*
             * Does string contain only alphabetic and number character and/or a space?
             * Potentially use for validating an address, but be careful if address field
             * Might contain hypen(-) or double qoutes(" ") or any other punctuation
             */
            bool bln;

            bln = Regex.IsMatch(strInput, "^[a-zA-Z0-0 ]+$");
            return bln; 
        }

        public static string nullToEmptyString(object myInput)
        {
            /* 
             * If the input is NULL then return an empty string
             * Otherwise return the original value as string
             */
            if (System.Convert.IsDBNull(myInput))
            {
                return "";
            }
            else if (myInput == null)
            {
                return "";
            }
            else
            {
                return (string)myInput;
            }
        }

        public static string nullToCurrentDate(object myInput)
        {
            /*
             * If the input value is NULL then return an empty string
             * Otherwise return the original value as string
             */
            if (System.Convert.IsDBNull(myInput))
            {
                return getServerDate().ToString();
            }
            else
            {
                return (string)myInput;
            }
        }

        public static decimal nullToZero(object myInput)
        {
            /*
             * If the input value is NULL then return an empty string
             * Otherwise return the original value as decimal
             */
            if (System.Convert.IsDBNull(myInput))
            {
                return 0;
            }
            else
            {
                return (decimal)myInput;
            }
        }

        public static double emptyToZero(object myInput)
        {
            /*
             * If the input valueis NULL then return an empty string
             * Else return the original value as a string
             */
        
            if(System.Convert.IsDBNull(myInput))
            {
                return 0;
            }
            else if ((string)myInput == "")
            {
                return 0;
            }
            else
            {
                return (double)myInput;
            }
            
        }

        public static string zeroToEmpty(object myInput)
        {
            /*
             * If the input value is NULL then return an empty string
             * Otherwise return the original value as string
             */
            if ((int)myInput == 0)
            {
                return "";
            }
            else
            {
                return (string)myInput;
            }
        }

        public static DateTime getServerDate()
        {
            string[] strPar = { "Operation" };
            string[] strVal = { "0" };
            DataTable dt = (DataTable)genericDA.manageQuery(strPar, strVal, "sp_getSystemDate", 0);
            DateTime vServerDate = (DateTime)dt.Rows[0]["serverDateTime"];
            dt = null;
            return vServerDate;
        }

        public static string concatFailToEmptyString(object myInput)
        {
            if (myInput == ",")
            {
                return "None";
            }
            else if (myInput == null)
            {
                return "";
            }
            else
            {
                return (string)myInput;
            }
        }

        public static string concatFailToCustomString(string myInput)
        {
            if (myInput == ",")
            {
                return "None";
            }
            else if (myInput == null)
            {
                return "";
            }
            else
            {
                return (string)myInput;
            }
        }

        public static DateTime nullToDefaultDateFormat(object myInput)
        {
            if (System.Convert.IsDBNull(myInput) || myInput == "" || myInput == null)
            {
                return DateTime.ParseExact("1/1/1900", "dd-MMM-yyyy",null);
            }
            else
            {
                return (DateTime)myInput;
            }
        }

    }
}
