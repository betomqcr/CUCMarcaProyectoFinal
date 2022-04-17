<%@ Page Title="" Language="C#" MasterPageFile="~/CUC.Master" AutoEventWireup="true" CodeBehind="FrmUsers.aspx.cs" Inherits="CUCMarca.Administracion.Matenimientos.FrmUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                <div class="alert alert-danger" role="alert">
                    <asp:Literal runat="server" ID="FailureText" />
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="SuccessMessage" Visible="false">
                <div class="alert alert-success" role="alert">
                    <asp:Literal runat="server" ID="SuccessText" />
                </div>
            </asp:PlaceHolder>
            <asp:MultiView ID="mvMain" runat="server">
                <asp:View ID="viewDatos" runat="server">
                    <div class="card mb-4 py-3 border-left-primary">
                        <div class="card-body">
                            <asp:LinkButton ID="btnNew" CssClass="btn btn-primary btn-icon-split" Text="<span class='icon text-white-50'><i class='fas fa-file'></i></span><span class='text'>Nuevo</span>" runat="server" OnClick="btnNew_Click" ToolTip="Nuevo usuario"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="card shadow mb-4">
                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Informaci&oacute;n</h6>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <asp:GridView CssClass="table table-borderless table-sm table-hover table-striped" HeaderStyle-CssClass="thead-dark" ID="gvMain" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound" AllowPaging="True" OnRowEditing="gvMain_RowEditing" OnRowCancelingEdit="gvMain_RowCancelingEdit" OnRowDeleting="gvMain_RowDeleting" OnRowUpdating="gvMain_RowUpdating" OnPageIndexChanging="gvMain_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="Id" HeaderText="ID" ReadOnly="True" />
                                        <asp:BoundField DataField="UserName" HeaderText="Usuario" />
                                        <asp:BoundField DataField="Email" HeaderText="Correo" />
                                        <asp:TemplateField HeaderText="Borrar?" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" CssClass="btn btn-primary btn-circle" Text="&lt;i class='fas fa-trash'&gt;&lt;/i&gt;" CommandName="Delete"  OnClientClick="return confirm('¿Está seguro que desea borrar el usuario?');" CausesValidation="False" ID="btnDelete"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="viewForm" runat="server">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger"/>
                    <div class="card mb-4 py-3 border-left-primary">
                        <div class="card-body">
                            <asp:LinkButton ID="btnCancel" CssClass="btn btn-primary btn-circle" Text="<span class='icon text-white-50'><i class='fas fa-arrow-left'></i></span><span class='text'></span>" runat="server" OnClick="btnCancel_Click" ToolTip="Cancelar" CausesValidation="False"></asp:LinkButton>
                            <asp:LinkButton ID="btnSave" CssClass="btn btn-primary btn-circle" Text="<span class='icon text-white-50'><i class='fas fa-save'></i></span><span class='text'></span>" runat="server" OnClick="btnSave_Click" ToolTip="Guardar"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="card shadow mb-4">
                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Formulario usuario</h6>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <div class="form-group">
                                    <asp:TextBox ID="txtNombre" CssClass="form-control form-control-user" runat="server" placeholder="Nombre del usuario (Email)" TextMode="Email" MaxLength="256"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe ingresar el nombre del usuario" Display="Dynamic" 
                                        SetFocusOnError="true" CssClass="text-danger" ControlToValidate="txtNombre"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtPassword" CssClass="form-control form-control-user" runat="server" placeholder="Contraseña" TextMode="SingleLine" MaxLength="20"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton CssClass="btn btn-outline-dark" ID="lnkAutogenerate" runat="server" CausesValidation="False" OnClick="lnkAutogenerate_Click">Autogenerar</asp:LinkButton>
                                        </span>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe ingresar la contraseña del usuario" Display="Dynamic"
                                        SetFocusOnError="true" CssClass="text-danger" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvMain" EventName="RowDeleting" />
            <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvMain" EventName="PageIndexChanging" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
