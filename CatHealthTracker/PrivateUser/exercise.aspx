<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="exercise.aspx.cs" Inherits="CatHealthTracker.exercise" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Exercise log</h1>

    <a href="/PrivateUser/exercise-details.aspx">Add an exercise log</a>

    <asp:TextBox ID="ErrorMsgTextBox" runat="server" Visible="false"/>

    <div>
        <label for="ddlPageSize">Records Per Page: </label>
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true"
             OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" OnLoad="ddlPageSize_SelectedIndexChanged">
            <asp:ListItem Value="4" Text="4" />
            <asp:ListItem Value="7" Text="7" />
            <asp:ListItem Value="14" Text="14" />
            <asp:ListItem Value="30" Text="30" />
        </asp:DropDownList>
    </div>

    <asp:GridView ID="grdExerciselog" runat="server" AutoGenerateColumns="false" 
         CssClass="table table-striped table-hover" DataKeyNames="ExerciseID"
         OnRowDeleting="grdExerciselog_RowDeleting" AllowPaging="true"
         OnPageIndexChanged="grdExerciselog_PageIndexChanged" PageSize="4" AllowSorting="true"
         OnSorting="grdExerciselog_Sorting" OnRowDataBound="grdExerciselog_RowDataBound">
        <Columns>
            <asp:BoundField DataField="ExerciseID" HeaderText="Exercise ID" SortExpression="Exercise ID" />
            <asp:BoundField DataField="ExerciseType" HeaderText="Exercise Type" SortExpression="Exercise Type" />
            <asp:BoundField DataField="Duration" HeaderText="Exercise Duration" SortExpression="Exercise Duration" />
            <asp:BoundField DataField="CaloriesBurn" HeaderText="Calories Burned" SortExpression="Calories Burned" />
            <asp:BoundField DataField="Daylist.Dayname" HeaderText="Day" SortExpression="Day"/>
            <asp:HyperLinkField HeaderText="Edit" NavigateUrl="exercise-details.aspx" Text="Edit"
                 DataNavigateUrlFormatString="exercise-details.aspx?ExerciseID={0}" DataNavigateUrlFields="ExerciseID" />
            <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" HeaderText="Delete" />
        </Columns>
    </asp:GridView>
</asp:Content>
