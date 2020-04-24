using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Utility
{
    // SD is static details: want static accessor keyword so we can access this class from other projects.
    // This is where we will store all the constant values that are used around the app to avoid magic strings, ints, etc.
    public static class SD
    {
        #region Stored Procedure Strings
        public const string Proc_CoverType_Create = "usp_CreateCoverType";
        public const string Proc_CoverType_Get = "usp_GetCoverType";
        public const string Proc_CoverType_GetAll = "usp_GetCoverTypes";
        public const string Proc_CoverType_Update = "usp_UpdateCoverType";
        public const string Proc_CoverType_Delete = "usp_DeleteCoverType";
        #endregion

        #region Application User Roles
        public const string Role_User_Individual = "Individual Customer";
        public const string Role_User_Company = "Company Customer";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";
        #endregion

        public const string ssShoppingCart = "Shopping Cart Session";

        public static double GetPriceBasedOnQuanitity(double quantity, double price, double price50, double price100)
        {
            if (quantity < 50)
            {
                return price;
            }
            else
            {
                if (quantity <100)
                {
                    return price50;
                }
                else
                {
                    return price100;
                }
            }
        }

        public static string ConvertToRawHtml(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char letter = source[i];
                if (letter == '<')
                {
                    inside = true;
                    continue;
                }
                if (letter=='>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = letter;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}
