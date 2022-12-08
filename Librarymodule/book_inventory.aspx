<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="book_inventory.aspx.cs" Inherits="Librarymodule.book_inventory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
       $(document).ready(function () {
           $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
       });

       function readURL(input) {
           if (input.files && input.files[0]) {
               var reader = new FileReader();

               reader.onload = function (e) {
                   $('#imgview').attr('src', e.target.result);
               };

               reader.readAsDataURL(input.files[0]);
           }
       }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentplaceholder1" runat="server">
     <div class="container-fluid">
      <div class="row">
         <div class="col-md-5">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>Book Details</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <img id="imgview" Height="150px" Width="100px" src="book_inventory/1.png" />
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
                         <asp:FileUpload onChange= "readURL(this);" class="form-control" ID="FileUpload1" runat="server" />

                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-6">
                       
                        <div class="form-group">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Book ID"></asp:TextBox>
                              <asp:Button class="btn btn-primary" ID="Button1" runat="server" Text="Go" OnClick="Button1_Click" />
                           </div>
                        </div>
                     </div>
                     <div class="col-md-6">
                        
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Full Name"></asp:TextBox>
                        </div>
                     </div>
                     
                  </div>
                  <div class="row">
                     <div class="col-md-4">
                        
                        <div class="form-group">
                            <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                                <asp:ListItem Text="language" value="language" />
                                <asp:ListItem Text="English" value="English" />
                                <asp:ListItem Text="Urdu" value="Urdu" />
                                <asp:ListItem Text="Arabic" value="Arabic" />

                            </asp:DropDownList></div>
                     </div>
                     <div class="col-md-4">
                       
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server"  placeholder="Author name" OnTextChanged="TextBox7_TextChanged1"></asp:TextBox>
                           </div>
                         </div>
                      <div class="col-md-4">
                       
                        <div class="form-group">
                             <asp:DropDownList ID="DropDownList3" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Type" value="Select" />
                                <asp:ListItem Text="Programming" value="Programming" />
                                <asp:ListItem Text="Novel" value="Novel" />
                                <asp:ListItem Text="Accounting" value="Accounting" />
                                 <asp:ListItem Text="Engineering" value="Engineering" />
                                 <asp:ListItem Text="Finance" value="Finance" />
                                  <asp:ListItem Text="History" value="History" />
                                 <asp:ListItem Text="Novel" value="Novel" />
                                



                            </asp:DropDownList></div>
                           </div>

                  </div>
                  <div class="row">
                     <div class="col-md-4">
                       
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server" placeholder="Edition"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-4">
                       
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" placeholder="Book Cost" ReadOnly="False" Visible="True"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-4">
                        
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server" placeholder="Pages"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                <div class="row">
                     <div class="col-md-4" visible="True">
                        
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Actual Stock" TextMode="Number"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-4">
                        
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Current Stock" TextMode="Number"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-4">
                       
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Issued Books" TextMode="Number"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                 
                  <div class="row">
                     <div class="col-12">
                        
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Book description" TextMode="MultiLine" Rows="2" ReadOnly="False"></asp:TextBox>
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
                 

                 

               </div>
            </div>
            <a href="homepage.aspx"><< Back to Home</a><br>
            <br>
         </div>
         <div class="col-md-7">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>Book Inventory List</h4>
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
                         <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                         </asp:GridView>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>
</asp:Content>
