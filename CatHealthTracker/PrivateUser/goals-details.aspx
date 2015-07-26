<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="goals-details.aspx.cs" Inherits="CatHealthTracker.goals_details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Goal log form</h1>
    <h5>All fields are required</h5>

    <div class="form-group">
        <label for="txtGoals" class="col-sm-2">Goal name: </label>
        <asp:TextBox ID="txtGoals" runat="server" required MaxLength="50" />
    </div>
    <div>
        <label for="txtDescription" class="col-sm-2">Description: </label>
        <asp:TextBox ID="txtDescription" runat="server" required MaxLength="200" />
    </div>
    <div>
        <label for="txtGoaltime" class="col-sm-2">Goal time (weekly): </label>
        <asp:TextBox ID="txtGoaltime" runat="server" required type="number" />
    </div>
    <div class="col-sm-offset-2">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
             OnClick="btnSave_Click"/>
    </div>
</asp:Content>
