<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="foodlog-details.aspx.cs" Inherits="CatHealthTracker.foodlog_details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Food log form</h1>
    <h5>All fields are required</h5>

    <div class="form-group">
        <label for="txtFoodname" class="col-sm-2">Food name:</label>
        <asp:TextBox ID="txtFoodname" runat="server" required MaxLength="50" />
    </div>
    <div class="form-group">
        <label for="txtCalCount" class="col-sm-2">Calories on the Food: </label>
        <asp:TextBox ID="txtCalCount" runat="server" required type="number" />
    </div>
    <div class="form-group">
        <label for="ddlFoodtype" class="col-sm-2">Food type: </label>
        <asp:DropDownList ID="ddlFoodtype" runat="server" DataTextField="FoodType"
             DataValueField="FoodID"></asp:DropDownList>
    </div>
    <div class="form-group">
        <label for="ddlDay" class="col-sm-2">Day: </label>
        <asp:DropDownList ID="ddlDay" runat="server" DataTextField="Dayname"
             DataValueField="DayID"></asp:DropDownList>
    </div>
    <div class="col-sm-offset-2">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
             OnClick="btnSave_Click" />
    </div>
</asp:Content>
