<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="book_lists.aspx.cs" Inherits="Librarymodule.book_lists" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentplaceholder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>Book List</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                         <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover" PageSize="10"  AllowPaging="true" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1">
                             
                            
                         </asp:GridView>
                     </div>
                  </div>
               </div>
            </div>
         </div>
        </div>
     </div>
</asp:Content>
