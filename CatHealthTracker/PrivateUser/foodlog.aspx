<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="foodlog.aspx.cs" Inherits="CatHealthTracker.foodlog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Food log</h1>

    <a href="/PrivateUser/foodlog-details.aspx">Add a food log</a>

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

    <asp:GridView ID="grdFoodlog" runat="server" AutoGenerateColumns="false" 
         CssClass="table table-striped table-hover" DataKeyNames="LogID" OnRowDeleting="grdFoodlog_RowDeleting"
         AllowPaging="true" OnPageIndexChanging="grdFoodlog_PageIndexChanging" PageSize="3" AllowSorting="true"
         OnSorting="grdFoodlog_Sorting" OnRowDataBound="grdFoodlog_RowDataBound">
        <Columns>
            <asp:BoundField DataField="logID" HeaderText="Log ID" />
            <asp:BoundField DataField="Daylist.Dayname" HeaderText="Day" />
            <asp:BoundField DataField="FoodName" HeaderText="Food name" />
            <asp:BoundField DataField="Foodlist.FoodType" HeaderText="Food type" />
            <asp:BoundField DataField="Calories" HeaderText="Food Calories" />
            <asp:HyperLinkField HeaderText="Edit" NavigateUrl="foodlog-details.aspx" Text="Edit"
                 DataNavigateUrlFormatString="foodlog-details.aspx?LogID={0}" DataNavigateUrlFields="LogID" />
            <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" HeaderText="Delete" />
        </Columns>
    </asp:GridView>
</asp:Content>
