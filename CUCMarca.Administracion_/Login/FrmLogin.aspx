<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmLogin.aspx.cs" Inherits="CUCMarca.Administracion.Login.FrmLogin" %>
<!DOCTYPE html>
<html lang="en">

<head>

  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">

  <title>CUC Marca - Login</title>

  <!-- Custom fonts for this template-->
  <link href="../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">

  <!-- Custom styles for this template-->
  <link href="../sbadmin/css/sb-admin-2.min.css" rel="stylesheet">
<style>
    .bg-login-image-cuc {
    background: url("../img/img_home.jpg");
    background-position: center;
    background-size: cover;
}

</style>
</head>

<body class="bg-gradient-primary">

  <div class="container">

    <!-- Outer Row -->
    <div class="row justify-content-center">

      <div class="col-xl-10 col-lg-12 col-md-9">

        <div class="card o-hidden border-0 shadow-lg my-5">
          <div class="card-body p-0">
            <!-- Nested Row within Card Body -->
            <div class="row">
              <div class="col-lg-6 d-none d-lg-block bg-login-image-cuc"></div>
              <div class="col-lg-6">
                <div class="p-5">
                  <div class="text-center">
                    <h1 class="h4 text-gray-900 mb-4">Bienvenido</h1>
                  </div>
                  <form runat="server" class="user">
                      <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                          <p class="text-danger">
                              <asp:Literal runat="server" ID="FailureText" />
                          </p>
                      </asp:PlaceHolder>
                    <div class="form-group">
                      <%--<input type="text" class="form-control form-control-user" id="exampleInputEmail" aria-describedby="emailHelp" placeholder="Ingrese el nombre de usuario">--%>
                        <asp:TextBox ID="txtUsername" CssClass="form-control form-control-user" runat="server" placeholder="Ingrese el nombre de usuario" TextMode="Email"></asp:TextBox>
                    </div>
                    <div class="form-group">
                      <asp:TextBox ID="txtPassword" CssClass="form-control form-control-user" runat="server" placeholder="Contrase&ntilde;a" TextMode="Password"></asp:TextBox>
                      <%--<input type="password" class="form-control form-control-user" id="exampleInputPassword" placeholder="Contraseña">--%>
                    </div>
                    <div class="form-group">
                      <div class="custom-control custom-checkbox small">
                        <%--<input type="checkbox" class="custom-control-input" id="customCheck">--%>
                          <asp:CheckBox CssClass="custom-control-input" ID="chkRemember" runat="server" Text="Recuerdame" />
                        <%--<label class="custom-control-label" for="customCheck">Recuerdame</label>--%>
                      </div>
                    </div>
<%--                    <a href="index.html" class="btn btn-primary btn-user btn-block">
                      Ingresar
                    </a>--%>
                      <asp:LinkButton ID="btnLogin" CssClass="btn btn-primary btn-user btn-block" runat="server" OnClick="btnLogin_Click">Ingresar</asp:LinkButton>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>

      </div>

    </div>

  </div>

  <!-- Bootstrap core JavaScript-->
  <script src="../Scripts/jquery-3.4.1.min.js"></script>
  <script src="../Scripts/bootstrap.bundle.min.js"></script>

  <!-- Core plugin JavaScript-->
  <script src="../Scripts/jquery.easing.1.3.js"></script>

  <!-- Custom scripts for all pages-->
  <script src="../sbadmin/js/sb-admin-2.min.js"></script>

</body>

</html>
