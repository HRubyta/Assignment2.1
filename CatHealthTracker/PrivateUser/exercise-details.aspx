<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="exercise-details.aspx.cs" Inherits="CatHealthTracker.exercise_details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Exercise log form</h1>
    <h5>All fields are required</h5>

    <div class="form-group">
        <label for="txtExercise" class="col-sm-2">Exercise type: </label>
        <asp:TextBox ID="txtExercise" runat="server" required MaxLength="50" />
    </div>
    <div>
        <label for="txtDuration" class="col-sm-2">Exercise duration (minutes): </label>
        <asp:TextBox ID="txtDuration" runat="server" required type="number" />
    </div>
    <div>
        <label for="txtCaloriesburn" class="col-sm-2">Calories burned: </label>
        <asp:TextBox ID="txtCaloriesburn" runat="server" required type="number" />
    </div>
    <div class="form-group">
        <label for="ddlDays" class="col-sm-2">Day: </label>
        <asp:DropDownList ID="ddlDays" runat="server" DataTextField="Dayname"
             DataValueField="DayID"></asp:DropDownList>
    </div>
    <div class="col-sm-offset-2">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
             OnClick="btnSave_Click"/>
    </div>
</asp:Content>
