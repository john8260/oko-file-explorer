<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RemoteFileForm.aspx.cs"
    Inherits="CSRemoteUploadAndDownload.RemoteFileForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family: Calibri;">
        <table>
            <tr>
                <th colspan="2">
                    <p style="font-size: x-large">
                        Remote Upload:</p>
                </th>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"
                        Height="100%" Width="40%" />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
