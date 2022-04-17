<%@ Page Title="" Language="C#" MasterPageFile="~/CUC.Master" AutoEventWireup="true" CodeBehind="FrmHorarios.aspx.cs" Inherits="CUCMarca.Administracion.Matenimientos.FrmHorarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="alert alert-dark" role="alert">
                Horario del funcionario: <asp:Literal ID="ltlFuncData" runat="server"></asp:Literal>
            </div>
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
                            <asp:LinkButton ID="btnCancel" CssClass="btn btn-primary btn-circle" Text="<span class='icon text-white-50'><i class='fas fa-arrow-left'></i></span><span class='text'></span>" runat="server" OnClick="btnCancel_Click" ToolTip="Cancelar" CausesValidation="False"></asp:LinkButton>
                            <asp:LinkButton ID="btnNew" CssClass="btn btn-primary btn-icon-split" Text="<span class='icon text-white-50'><i class='fas fa-file'></i></span><span class='text'>Nuevo</span>" runat="server" OnClick="btnNew_Click" ToolTip="Nuevo Funcionario"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="card shadow mb-4">
                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Horarios</h6>
                        </div>
                        <div class="card-body">
                            <asp:PlaceHolder runat="server" ID="SelectedMessage" Visible="false">
                                <span class="badge badge-secondary"><asp:Literal runat="server" ID="SelectedText" /></span>
                            </asp:PlaceHolder>
                            <div class="table-responsive">
                                <asp:GridView CssClass="table table-borderless table-sm table-hover table-striped" HeaderStyle-CssClass="thead-dark" ID="gvMain" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvMain_PageIndexChanging" OnSelectedIndexChanging="gvMain_SelectedIndexChanging" OnRowDataBound="gvMain_RowDataBound" OnRowCommand="gvMain_RowCommand" OnRowDeleting="gvMain_RowDeleting">
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" HeaderText="S." SelectText="&lt;i class='fas fa-arrow-circle-right'&gt;&lt;/i&gt;" />
                                        <asp:BoundField DataField="HorarioID" ReadOnly="true" HeaderText="ID" />
                                        <asp:BoundField DataField="CodigoFuncionario" ReadOnly="true" HeaderText="Codigo" />
                                        <asp:BoundField DataField="NombreArea" ReadOnly="true" HeaderText="Área" />
                                        <asp:BoundField DataField="Anio" ReadOnly="true" HeaderText="Año" />
                                        <asp:BoundField DataField="Periodo" ReadOnly="true" HeaderText="Periodo" />
                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# ObtenerNombreEstado(int.Parse(Eval("Estado").ToString()))  %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEstadoGrid"  CssClass="form-control form-control-user" runat="server">
                                                    <asp:ListItem Text="Vigente" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Vencido" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ControlStyle-CssClass="btn btn-primary btn-circle" HeaderText="Borrar?" CancelText="&lt;i class='fas fa-window-close'&gt;&lt;/i&gt;" DeleteText="&lt;i class='fas fa-trash'&gt;&lt;/i&gt;" EditText="&lt;i class='fas fa-edit'&gt;&lt;/i&gt;" ShowDeleteButton="True" UpdateText="&lt;i class='fas fa-save'&gt;&lt;/i&gt;" ShowCancelButton="False" >
                                        <ControlStyle CssClass="btn btn-primary btn-circle" />
                                        </asp:CommandField>
                                        <asp:TemplateField HeaderText="Habilitar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEnableGrid" CssClass="btn btn-primary btn-circle" CommandName="Habilitar" CommandArgument='<%# int.Parse(Eval("HorarioID").ToString()) %>' runat="server">
                                                    <i class='fas fa-check'></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deshabilitar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDisableGrid" CssClass="btn btn-primary btn-circle" CommandName="Deshabilitar" CommandArgument='<%# int.Parse(Eval("HorarioID").ToString()) %>' runat="server">
                                                    <i class='fas fa-times'></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="thead-dark" />
                                </asp:GridView>
                                <asp:Panel ID="phDetalle" Visible="false" runat="server">
                                    <div class="alert alert-dark" role="alert">
                                        Detalle del horario: <asp:Literal ID="ltlHorarioID" runat="server"></asp:Literal>
                                    </div>
                                    <div class="form-inline">
                                        <asp:DropDownList ID="ddlDia" CssClass="form-control form-control-user" runat="server">
                                            <asp:ListItem Value="0" Text="Seleccione el día"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Lunes"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Martes"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Miércoles"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Jueves"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="Viernes"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="Sábado"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtHoraIngreso" CssClass="form-control form-control-user" runat="server" placeholder="Hora ingreso" TextMode="Number" MaxLength="2"></asp:TextBox>
                                        <asp:TextBox ID="txtMinutoIngreso" CssClass="form-control form-control-user" runat="server" placeholder="Minuto ingreso" TextMode="Number" MaxLength="2"></asp:TextBox>
                                        <asp:TextBox ID="txtHoraSalida" CssClass="form-control form-control-user" runat="server" placeholder="Hora salida" TextMode="Number" MaxLength="2"></asp:TextBox>
                                        <asp:TextBox ID="txtMinutoSalida" CssClass="form-control form-control-user" runat="server" placeholder="Minuto salida" TextMode="Number" MaxLength="2"></asp:TextBox>
                                        <asp:LinkButton ID="btnAddSchedule" CssClass="btn btn-primary btn-icon-split" Text="<span class='icon text-white-50'><i class='fas fa-plus'></i></span><span class='text'>Agregar</span>" runat="server" OnClick="btnAddSchedule_Click" ToolTip="Nuevo detalle"></asp:LinkButton>
                                    </div>
                                    <hr />
                                    <div class="table-responsive">
                                        <asp:GridView CssClass="table table-borderless table-sm table-hover table-striped" HeaderStyle-CssClass="thead-dark" ID="gvDetail" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvDetail_PageIndexChanging" OnRowDeleting="gvDetail_RowDeleting">
                                            <Columns>
                                                <asp:BoundField DataField="HorarioID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" ReadOnly="true" HeaderText="ID" />
                                                <asp:BoundField DataField="Dia" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" ReadOnly="true" HeaderText="Dia" />
                                                <asp:TemplateField HeaderText="Dia">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# ObtenerNombreDia(int.Parse(Eval("Dia").ToString())) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="HoraIngreso" ReadOnly="true" HeaderText="H. Ingreso" />
                                                <asp:BoundField DataField="MinutoIngreso" ReadOnly="true" HeaderText="M. Ingreso" />
                                                <asp:BoundField DataField="HoraSalida" ReadOnly="true" HeaderText="H. Salida" />
                                                <asp:BoundField DataField="MinutoSalida" ReadOnly="true" HeaderText="M. Salida" />
                                                <asp:CommandField ControlStyle-CssClass="btn btn-primary btn-circle" HeaderText="Borrar?" CancelText="&lt;i class='fas fa-window-close'&gt;&lt;/i&gt;" DeleteText="&lt;i class='fas fa-trash'&gt;&lt;/i&gt;" EditText="&lt;i class='fas fa-edit'&gt;&lt;/i&gt;" ShowDeleteButton="True" UpdateText="&lt;i class='fas fa-save'&gt;&lt;/i&gt;" ShowCancelButton="False">
                                                    <ControlStyle CssClass="btn btn-primary btn-circle" />
                                                </asp:CommandField>
                                            </Columns>
                                            <HeaderStyle CssClass="thead-dark" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="viewForm" runat="server">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger"/>
                    <div class="card mb-4 py-3 border-left-primary">
                        <div class="card-body">
                            <asp:LinkButton ID="lnkCancel" CssClass="btn btn-primary btn-circle" Text="<span class='icon text-white-50'><i class='fas fa-arrow-left'></i></span><span class='text'></span>" runat="server" OnClick="lnkCancel_Click" ToolTip="Cancelar" CausesValidation="False"></asp:LinkButton>
                            <asp:LinkButton ID="btnSave" CssClass="btn btn-primary btn-circle" Text="<span class='icon text-white-50'><i class='fas fa-save'></i></span><span class='text'></span>" runat="server" OnClick="btnSave_Click" ToolTip="Guardar"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="card shadow mb-4">
                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Formulario nuevo horario</h6>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <div class="alert alert-success" role="alert">
                                    *Si crea el nuevo horario con estado vigente, los demás horarios del código - área indicados pasaran a estado vencido.
                                </div>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCodigoFuncionario" CssClass="form-control form-control-user" runat="server">
                                    </asp:DropDownList>
                                    <asp:CompareValidator ControlToValidate="ddlCodigoFuncionario" ID="CompareValidator1"
                                        CssClass="text-danger" ErrorMessage="Seleccione el código de funcionario"
                                        runat="server" Display="Dynamic"
                                        Operator="NotEqual" ValueToCompare="Seleccione el código" Type="String" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtAnio" CssClass="form-control form-control-user" runat="server" placeholder="Año (Ej: 2020)" TextMode="Number" MaxLength="4"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe ingresar el año" Display="Dynamic" 
                                        SetFocusOnError="true" CssClass="text-danger" ControlToValidate="txtAnio"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlPeriodo" CssClass="form-control form-control-user" runat="server">
                                        <asp:ListItem Value="0" Text="Seleccione el periodo"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:CompareValidator ControlToValidate="ddlPeriodo" ID="CompareValidator2"
                                        CssClass="text-danger" ErrorMessage="Seleccione el periodo"
                                        runat="server" Display="Dynamic"
                                        Operator="NotEqual" ValueToCompare="0" Type="Integer" />
                                </div>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlEstadoForm" CssClass="form-control form-control-user" runat="server">
                                        <asp:ListItem Value="0" Text="Seleccione el estado"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Vigente"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Vencido"></asp:ListItem>
                                    </asp:DropDownList>*
                                    <asp:CompareValidator ControlToValidate="ddlEstadoForm" ID="CompareValidator3"
                                        CssClass="text-danger" ErrorMessage="Seleccione el estado"
                                        runat="server" Display="Dynamic"
                                        Operator="NotEqual" ValueToCompare="0" Type="Integer" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAddSchedule" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkCancel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvMain" EventName="SelectedIndexChanging" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
