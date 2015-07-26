<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="foodlist.aspx.cs" Inherits="CatHealthTracker.foodlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Food list</h1>

    <p>Not enough food list for you?</p>
    <a href="foodlist-details.aspx">Add food list</a>

    <asp:TextBox ID="ErrorMsgTextBox" runat="server" Visible="false"/>

    <div>
        <label for="ddlPageSize">Records Per Page: </label>
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true"
             OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" OnLoad="ddlPageSize_SelectedIndexChanged">
            <asp:ListItem Value="5" Text="5" />
            <asp:ListItem Value="10" Text="10" />
            <asp:ListItem Value="20" Text="20" />
            <asp:ListItem Value="30" Text="30" />
        </asp:DropDownList>
    </div>

    <asp:GridView ID="grdFoodlist" runat="server" AutoGenerateColumns="false" 
         CssClass="table table-striped table-hover" DataKeyNames="FoodID" OnRowDeleting="grdFoodlist_RowDeleting"
         AllowPaging="true" OnPageIndexChanging="grdFoodlist_PageIndexChanging" PageSize="5" AllowSorting="true"
         OnSorting="grdFoodlist_Sorting" OnRowDataBound="grdFoodlist_RowDataBound">
        <Columns>
            <asp:BoundField DataField="FoodID" HeaderText="Food ID" />
            <asp:BoundField DataField="FoodType" HeaderText="Food Type" />
            <asp:BoundField DataField="FoodBrand" HeaderText="Food Brand" />
            <asp:BoundField DataField="Notes" HeaderText="Notes" />
            <asp:HyperLinkField HeaderText="Edit" NavigateUrl="foodlist-details.aspx" Text="Edit"
                 DataNavigateUrlFormatString="foodlist-details.aspx?FoodID={0}" DataNavigateUrlFields="FoodID" />
            <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" HeaderText="Delete" />
        </Columns>
    </asp:GridView>
</asp:Content>
