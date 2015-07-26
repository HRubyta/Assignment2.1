<%@ Page Title="Cat Helth Tracker - Main Menu" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="CatHealthTracker.main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="well">
            <h3>Feline calories calculator</h3>
            <p>This calculator is useful to know how much calories your cat needs.</p>
            <div class="form-group">
                <label for="txtWeight">Weight in pounds(lb):</label>
                <asp:TextBox ID="txtWeight" runat="server" required type="number" />
            </div>
            <div class="col-sm-offset-2">
                <asp:Button ID="btnSubmit" runat="server" Text="Calculate" CssClass="btn btn-primary"
                    OnClick="btnSubmit_Click" />
            </div>

            <asp:Panel ID="panelResults" runat="server">
                <h3>Result</h3>
                <div>
                    <label for="txtMass">Weight in kg: </label>
                    <asp:TextBox ID="txtMass" runat="server" />
                </div>
                <div>
                    <label for="txtCalorie">Calories per day: </label>
                    <asp:TextBox ID="txtCalorie" runat="server" />
                </div>
            </asp:Panel>
        </div>
        <div class="well">
            <h3>Exercise</h3>
            <ul class="list-group">
                <li class="list-group-item"><a href="/PrivateUser/exercise.aspx">Exercise Log</a></li>
                <li class="list-group-item"><a href="/PrivateUser/exercise-details.aspx">Add Exercise</a></li>
            </ul>
        </div>

         <div class="well">
            <h3>Food</h3>
            <ul class="list-group">
                <li class="list-group-item"><a href="/PrivateUser/foodlog.aspx">Food Log</a></li>
                <li class="list-group-item"><a href="/PrivateUser/foodlog-details.aspx">Add Food</a></li>
            </ul>
        </div>

         <div class="well">
            <h3>Goals</h3>
            <ul class="list-group">
                <li class="list-group-item"><a href="/PrivateUser/goals.aspx">Goals Log</a></li>
                <li class="list-group-item"><a href="/PrivateUser/goals-details.aspx">Add Goals</a></li>
            </ul>
        </div>
    </div>
</asp:Content>
