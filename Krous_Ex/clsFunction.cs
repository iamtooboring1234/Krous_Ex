using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Krous_Ex
{
    public class clsFunction
    {
        public static void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = string.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }

    }
}