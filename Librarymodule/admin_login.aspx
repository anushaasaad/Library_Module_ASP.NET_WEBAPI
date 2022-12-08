<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="admin_login.aspx.cs" Inherits="Librarymodule.admin_login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentplaceholder1" runat="server">
    <section class="login">
        <div class="title">
            <div class="max-width">
                <div class="title-box1">
                 <div class="title-content">
                     <h2>Log In with you ID Password</h2>
                     <div class="row">
                         <div class="col">
                             
                             <div class="form-group">
                                 <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" PlaceHolder="ID"></asp:TextBox>
                             </div>

                         </div>
                         </div>
                     <div class="row">
                         <div class="col">
                            
                             <div class="form-group">
                                 <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" PlaceHolder="Password"></asp:TextBox>
                             </div>

                         </div>

                     </div>
                           <div class="form-group">
                           <asp:Button class="btn" ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
                        </div>
                     <div class="text">
                      <a href="homepage.aspx"> Back to Home<< </a>
                     </div>
               
                  </div>
               
               </div>
                   </div>
        </div>
  

    </section>
</asp:Content>
