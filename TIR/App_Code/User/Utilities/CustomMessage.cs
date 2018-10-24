using System;
using System.Data;
using System.Configuration;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

/// <summary>
/// Summary description for CustomMessage
/// </summary>
public class CustomMessage
{
    public enum MessageType
    {
        WARNING = 1,
        ERROR = 2,
        SUCCESS = 3,
        INFO = 4,
        CLEAR
    }
	public CustomMessage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void DisplayMessage(Page p,string message)
    {
        DisplayMessage(p, message, MessageType.INFO);
    }
    public static void DisplayMessage(Page p, string message, MessageType mType)
    {
        AjaxControlToolkit.ModalPopupExtender MPEMsg = (AjaxControlToolkit.ModalPopupExtender)p.Master.FindControl("MPEMsg");
        Label lblInfo = (Label)p.Master.FindControl("lblMsg");
        switch (mType)
        {
            case MessageType.ERROR:
                lblInfo.Text = message;
                lblInfo.CssClass = "errmsg";
                break;
            case MessageType.SUCCESS:
                lblInfo.Text = message;
                lblInfo.CssClass = "successmsg";
                break;
            case MessageType.WARNING:
                lblInfo.Text = message;
                lblInfo.CssClass = "warningmsg";
                break;
            case MessageType.INFO:
                lblInfo.Text = message;
                lblInfo.CssClass = "infomsg";
                break;

        }
        MPEMsg.Show();
    }
    public static void ClearMessage(Page p)
    {
        Label lblInfo = (Label)p.Master.FindControl("lblMsg");
        lblInfo.Text = string.Empty;
        lblInfo.CssClass = "clearmsg";
    }
    public static void SetModuleName(Page p, string message)
    {
        Label lblInfo = (Label)p.Master.FindControl("lblModuleTitle");
        lblInfo.Text = message;
    }
}
