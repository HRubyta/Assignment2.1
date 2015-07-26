<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="goals.aspx.cs" Inherits="CatHealthTracker.goals" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Goals log</h1>

    <a href="/PrivateUser/goals-details.aspx">Add a goal for your cat</a>

    <asp:TextBox ID="ErrorMsgTextBox" runat="server" Visible="false"/>

    <div>
        <label for="ddlPageSize">Records Per Page: </label>
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true"
             OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" OnLoad="ddlPageSize_SelectedIndexChanged">
            <asp:ListItem Value="3" Text="3" />
            <asp:ListItem Value="7" Text="7" />
            <asp:ListItem Value="14" Text="14" />
            <asp:ListItem Value="30" Text="30" />
        </asp:DropDownList>
    </div>

    <asp:GridView ID="grdGoals" runat="server" AutoGenerateColumns="false" 
         CssClass="table table-striped table-hover" DataKeyNames="GoalID" OnRowDeleting="grdGoals_RowDeleting"
         AllowPaging="true" OnPageIndexChanging="grdGoals_PageIndexChanging" PageSize="3">

        <Columns>
            <asp:BoundField DataField="GoalID" HeaderText="Goal ID" />
            <asp:BoundField DataField="GoalName" HeaderText="Goal Name" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="Goal Time" HeaderText="Goal Time" />
            <asp:HyperLinkField HeaderText="Edit" NavigateUrl="goals-details.aspx" Text="Edit"
                 DataNavigateUrlFormatString="goals-details.aspx?GoalID={0}" DataNavigateUrlFields="GoalID" />
            <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" HeaderText="Delete" />
        </Columns>
    </asp:GridView>
</asp:Content>
