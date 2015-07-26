<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="foodlist-details.aspx.cs" Inherits="CatHealthTracker.foodlist_details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Food list form</h1>
    <h5>All fields are required</h5>

    <div class="form-group">
        <label for="txtFood" class="col-sm-2">Food type: </label>
        <asp:TextBox ID="txtFood" runat="server" required MaxLength="50" />
    </div>
    <div>
        <label for="txtBrand" class="col-sm-2">Food brand: </label>
        <asp:TextBox ID="txtBrand" runat="server" required MaxLength="50" />
    </div>
    <div>
        <label for="txtNotes" class="col-sm-2">Notes: </label>
        <asp:TextBox ID="txtNotes" runat="server" required MaxLength="100" />
    </div>
    <div class="col-sm-offset-2">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
             OnClick="btnSave_Click"/>
    </div>
</asp:Content>
