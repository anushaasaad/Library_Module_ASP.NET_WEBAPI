<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="authormanage.aspx.cs" Inherits="Librarymodule.authormanage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentplaceholder1" runat="server">
       <div class="container">
      <div class="row">
         <div class="col-md-6">
            <div class="card">
               <div class="card-body">
                    <div class="row">
                     <div class="col">
                        <center>
                           <h4>Author Profile</h4></center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <img width="100px" src="img/book.png"/>
                        </center>
                     </div>
                  </div>
                 
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-4">
                        <label>Autho ID</label>
                         <div class="form-group">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="ID"></asp:TextBox>
                              <asp:Button class="btn btn-primary" ID="Button1" runat="server" Text="Go" OnClick="Button1_Click" />
                           </div>
                        </div>

                    
                        </div>
                     <div class="col-md-8">
                        <label>Author Name</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Name"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                    <div class="row manipulate">
                     <div class="col-4">
                       <asp:Button class="add" ID="Button2" runat="server" Text="Add" OnClick="Button2_Click" />
                        
                     </div>
                        <div class="col-4">
                       <asp:Button class="update" ID="Button3" runat="server" Text="Update" OnClick="Button3_Click" />
                        
                     </div>
                        <div class="col-4">
                       <asp:Button class="delete" ID="Button4" runat="server" Text="Delete" OnClick="Button4_Click" />
                        
                     </div>
                  </div>
                 

                 



                 <div class="row">
                     <div class="col-8 mx-auto">
                        <center>
                           <div class="form-group">
                               </div>
                        </center>
                     </div>
                  </div>
               </div>
            </div>
            <a href="homepage.aspx"><< Back to Home</a><br><br>
         </div>
         <div class="col-md-6">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>Author List</h4>
                           </center>
                     </div>
                  </div>

                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col" >
                         <center>
                         <asp:GridView ID="GridView1" Class="mygridview" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BorderStyle="Solid" CellPadding="10"  >
                             
                         </asp:GridView></center>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>

       </asp:Content>
