<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebFormsClient.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>
    <asp:TextBox runat="server" ID="txtEntry" ></asp:TextBox>
    <asp:DropDownList runat="server" ID="ddlSelection">
        <asp:ListItem>
            ABC
        </asp:ListItem>
        <asp:ListItem>
            PQR
        </asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="btnClick" runat="server" Text="Button" OnClick="btnClick_Click" />

</asp:Content>
