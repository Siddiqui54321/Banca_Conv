using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Presentation;
using ace;

namespace Bancassurance.Presentation
{
    public partial class FormPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (SessionObject.Get("s_USE_TYPE").ToString() == "A") // Admin Login Check
                {
                    lblErrMsg.Text = "";
                    PnlBOP.Visible = true;

                }
                else
                {
                    if (SessionObject.Get("s_CCD_CODE").ToString() != "9")
                    {
                        PnlBOP.Visible = false;
                        lblErrMsg.Text = "For this page, you are not authorised.";

                    }

                    else
                    {
                        // Login only Bank of Punjab users
                        lblErrMsg.Text = "";
                        PnlBOP.Visible = true;

                    }
                }

            }

        }
    }
}