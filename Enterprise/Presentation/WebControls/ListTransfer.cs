using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections ;
using System.Data;
using Fluent;

[assembly:TagPrefix("SHMA.Enterprise.Presentation.WebControls","shma")]
namespace SHMA.Enterprise.Presentation.WebControls{
	[ToolboxData("<{0}:listtransfer runat=server></{0}:listtransfer>")]
	public class ListTransfer :Fluent.ListTransfer{
	}
}
