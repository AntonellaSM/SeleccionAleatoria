<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeleccionAleatoria.aspx.cs" Inherits="SeleccionAleatoria.SeleccionAleatoria" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <style type="text/css">
        .auto-style1 {
            width: 603px;
        }
    </style>

   

</head>
<body>
    <form id="form1" runat="server" class="auto-style1">
    <asp:Panel ID="Panel1" runat="server">
    
        <div>
            <asp:Label ID="Label1" runat="server" Text="Subir Archivo Excel .XLSX" ToolTip="Columna A:CuitEmpleador   Columna B:CuitEmpleado  HeadersFila1:True   Data:Fila2"></asp:Label>
        </div>
        <asp:FileUpload ID="FileUpload1" runat="server" Width="430px" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1" ValidationGroup="1" ErrorMessage="Debe seleccionar el Archivo .XLSX" Font-Italic="true" ForeColor="red"></asp:RequiredFieldValidator>
        <br />
        Cantidad de empleados a tomar
        <asp:TextBox ID="txtCantEmp" runat="server" Width="60px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtCantEmp" ForeColor="Red" Font-Italic="true" ControlToValidate="txtCantEmp" Display="Dynamic" runat="server" ErrorMessage="Se requiere cantidad de empleados a tomar" ValidationGroup="1"></asp:RequiredFieldValidator>
        <br />
        <asp:CustomValidator ID="CustomValidator1" ControlToValidate="txtCantEmp" runat="server" ForeColor="red" Font-Italic="true" Display="Dynamic" ErrorMessage="Campo Invalido. Debe ser numérico" ValidationGroup="1" OnServerValidate="CustomValidator1_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
        <br />
       
   
    </asp:Panel>
         <asp:Button ID="btnProcesar" runat="server" OnClick="Button1_Click" Text="Procesar" ValidationGroup="1" UseSubmitBehavior="False" />
   
         </form>
            
</body>
</html>
