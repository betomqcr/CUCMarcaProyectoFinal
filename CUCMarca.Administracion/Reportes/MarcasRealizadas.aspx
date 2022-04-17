<%@ Page Title="" Language="C#" MasterPageFile="~/CUC.Master" AutoEventWireup="true" CodeBehind="MarcasRealizadas.aspx.cs" Inherits="CUCMarca.Administracion.Reportes.MarcasRealizadas" %>

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
            <div class="card mb-4 py-3 border-left-primary">
                <div class="card-body">
                    <div class="form-inline">
                        <asp:TextBox ID="txtFechaIni" CssClass="form-control form-control-user" placeholder="Fecha de inicio" TextMode="Date" runat="server" MaxLength="10"></asp:TextBox>
                        <asp:TextBox ID="txtFechaFin" CssClass="form-control form-control-user" runat="server" placeholder="Fecha de fin" TextMode="Date" MaxLength="10"></asp:TextBox>
                    </div>
                    <asp:LinkButton ID="btnGenerate" CssClass="btn btn-primary btn-icon-split" Text="<span class='icon text-white-50'><i class='fas fa-caret-right'></i></span><span class='text'>Ejecutar</span>" runat="server" OnClick="btnGenerate_Click" ToolTip="Nuevo Funcionario"></asp:LinkButton>
                </div>
            </div>
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Marcas realizadas</h6>
                </div>
                <div class="card-body">
                    <asp:PlaceHolder runat="server" ID="SelectedMessage" Visible="false">
                        <span class="badge badge-secondary">
                            <asp:Literal runat="server" ID="SelectedText" /></span>
                    </asp:PlaceHolder>
                    <div class="table-responsive">
                        <asp:GridView CssClass="table table-borderless table-sm table-hover table-striped" HeaderStyle-CssClass="thead-dark" ID="gvMain" runat="server" AutoGenerateColumns="False" AllowPaging="True">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" HeaderText="S." SelectText="&lt;i class='fas fa-arrow-circle-right'&gt;&lt;/i&gt;" />
                                <asp:BoundField DataField="CodigoFuncionario" ReadOnly="true" HeaderText="Codigo" />
                                <asp:BoundField DataField="Identificacion" ReadOnly="true" HeaderText="Id." />
                                <asp:BoundField DataField="Nombre" ReadOnly="true" HeaderText="Nombre" />
                                <asp:BoundField DataField="Apellidos" ReadOnly="true" HeaderText="Apellidos" />
                                <asp:BoundField DataField="FechaAsistencia" ReadOnly="true" HeaderText="Fecha" />
                                <asp:BoundField DataField="TipoMarca" ReadOnly="true" HeaderText="Tipo" />
                                <asp:BoundField DataField="DireccionIP" ReadOnly="true" HeaderText="IP" />
                                <asp:BoundField DataField="Latitud" ReadOnly="true" HeaderText="Lat." />
                                <asp:BoundField DataField="Longitud" ReadOnly="true" HeaderText="Long." />
                            </Columns>
                            <HeaderStyle CssClass="thead-dark" />
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
