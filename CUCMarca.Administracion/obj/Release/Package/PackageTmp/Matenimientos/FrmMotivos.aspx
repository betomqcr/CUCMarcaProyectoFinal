﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CUC.Master" AutoEventWireup="true" CodeBehind="FrmMotivos.aspx.cs" Inherits="CUCMarca.Administracion.Matenimientos.FrmMotivos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                <%--<p class="text-danger">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>--%>
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
                            <asp:LinkButton ID="btnNew" CssClass="btn btn-primary btn-icon-split" Text="<span class='icon text-white-50'><i class='fas fa-file'></i></span><span class='text'>Nuevo</span>" runat="server"  ToolTip="Nuevo Motivo" OnClick="btnNew_Click"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary btn-icon-split" Text="<span class='icon text-white-50'><i class='fas fa-file'></i></span><span class='text'>Modificar</span>" runat="server" ToolTip="Modificar Motivo" OnClick="LinkButton1_Click"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" CssClass="btn btn-primary btn-icon-split" Text="<span class='icon text-white-50'><i class='fas fa-file'></i></span><span class='text'>Eliminar</span>" runat="server" ToolTip="Eliminar Motivo" OnClick="LinkButton2_Click"></asp:LinkButton>
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
                                        <asp:BoundField DataField="MotivoID" HeaderText="ID" ReadOnly="True" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre Motivo" />
                                        <asp:CommandField ShowEditButton="True" ControlStyle-CssClass="btn btn-primary btn-circle" CancelText="&lt;i class='fas fa-window-close'&gt;&lt;/i&gt;" DeleteText="&lt;i class='fas fa-trash'&gt;&lt;/i&gt;" EditText="&lt;i class='fas fa-edit'&gt;&lt;/i&gt;" ShowDeleteButton="True" UpdateText="&lt;i class='fas fa-save'&gt;&lt;/i&gt;" />
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
                            <h6 class="m-0 font-weight-bold text-primary">Formulario Motivos</h6>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <div class="form-group">
                                    <asp:TextBox ID="txtNombre" CssClass="form-control form-control-user" runat="server" placeholder="Ingrese el nombre del motivo" TextMode="SingleLine" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Motivo esta siendo Utiizado" Display="Dynamic" 
                                        SetFocusOnError="true" CssClass="text-danger" ControlToValidate="txtNombre"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
       </ContentTemplate>
       <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvMain" EventName="RowEditing" />
            <asp:AsyncPostBackTrigger ControlID="gvMain" EventName="RowUpdating" />
            <asp:AsyncPostBackTrigger ControlID="gvMain" EventName="RowCancelingEdit" />
            <asp:AsyncPostBackTrigger ControlID="gvMain" EventName="RowDeleting" />
            <asp:AsyncPostBackTrigger ControlID="gvMain" EventName="PageIndexChanging" />
       </Triggers>
    </asp:UpdatePanel>
</asp:Content>
