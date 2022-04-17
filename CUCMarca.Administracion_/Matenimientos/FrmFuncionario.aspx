<%@ Page Title="" Language="C#" MasterPageFile="~/CUC.Master" AutoEventWireup="true" CodeBehind="FrmFuncionario.aspx.cs" Inherits="CUCMarca.Administracion.Matenimientos.FrmFuncionario" %>
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
                <%-- ********************GRID PRINCIPAL********************** --%>
                <asp:View ID="viewDatos" runat="server">
                    <div class="card mb-4 py-3 border-left-primary">
                        <div class="card-body">
                            <asp:LinkButton ID="btnNew" CssClass="btn btn-primary btn-icon-split" Text="<span class='icon text-white-50'><i class='fas fa-file'></i></span><span class='text'>Nuevo</span>" runat="server" OnClick="btnNew_Click" ToolTip="Nuevo Funcionario"></asp:LinkButton>
                            <asp:LinkButton ID="btnChangePassword" CssClass="btn btn-primary btn-icon-split" Text="<span class='icon text-white-50'><i class='fas fa-unlock'></i></span><span class='text'>Cambio clave</span>" runat="server" OnClick="btnChangePassword_Click" ToolTip="Cambio de clave"></asp:LinkButton>
                            <asp:LinkButton ID="lnkArea" CssClass="btn btn-primary btn-icon-split" Text="<span class='icon text-white-50'><i class='fas fa-laptop-house'></i></span><span class='text'>Áreas</span>" runat="server" OnClick="lnkArea_Click" ToolTip="Áreas del funcionario"></asp:LinkButton>
                            <asp:LinkButton ID="lnkSchedule" CssClass="btn btn-primary btn-icon-split" Text="<span class='icon text-white-50'><i class='fas fa-calendar'></i></span><span class='text'>Horarios</span>" runat="server" OnClick="lnkSchedule_Click" ToolTip="Horarios del funcionario"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="card shadow mb-4">
                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Informaci&oacute;n</h6>
                        </div>
                        <div class="card-body">
                            <asp:PlaceHolder runat="server" ID="SelectedMessage" Visible="false">
                                <span class="badge badge-secondary"><asp:Literal runat="server" ID="SelectedText" /></span>
                            </asp:PlaceHolder>
                            <div class="table-responsive">
                                <asp:GridView CssClass="table table-borderless table-sm table-hover table-striped" HeaderStyle-CssClass="thead-dark" ID="gvMain" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound" AllowPaging="True" OnRowEditing="gvMain_RowEditing" OnRowCancelingEdit="gvMain_RowCancelingEdit" OnRowDeleting="gvMain_RowDeleting" OnRowUpdating="gvMain_RowUpdating" OnPageIndexChanging="gvMain_PageIndexChanging" OnSelectedIndexChanging="gvMain_SelectedIndexChanging">
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" HeaderText="S." SelectText="&lt;i class='fas fa-arrow-circle-right'&gt;&lt;/i&gt;" />
                                        <asp:BoundField DataField="FuncionarioID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" HeaderText="ID" ReadOnly="True" />
                                        <asp:TemplateField HeaderText="Tipo ID">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# ObtenerNombreTipoIdentificacion(int.Parse(Eval("TipoIdentificacionID").ToString()))  %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlTipoIdentificacionGrid" DataSource='<%# tiposId %>' DataTextField="Nombre" DataValueField="TipoIdentificacionID" CssClass="form-control form-control-user" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="Apellido" HeaderText="Apellidos" />
                                        <asp:TemplateField HeaderText="Tipo Func.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# ObtenerNombreTipoFuncionario(int.Parse(Eval("TipoFuncionarioID").ToString()))  %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlTipoFuncionarioGrid" DataSource='<%# tiposFuncionario %>' DataTextField="Nombre" DataValueField="TipoFuncionarioID" CssClass="form-control form-control-user" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Correo" HeaderText="Correo" />
                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# ObtenerNombreEstado(int.Parse(Eval("Estado").ToString()))  %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEstadoFuncionarioGrid" CssClass="form-control form-control-user" runat="server">
                                                    <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Inactivo" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" ControlStyle-CssClass="btn btn-primary btn-circle" CancelText="&lt;i class='fas fa-window-close'&gt;&lt;/i&gt;" DeleteText="&lt;i class='fas fa-trash'&gt;&lt;/i&gt;" EditText="&lt;i class='fas fa-edit'&gt;&lt;/i&gt;" ShowDeleteButton="True" UpdateText="&lt;i class='fas fa-save'&gt;&lt;/i&gt;" >
                                        <ControlStyle CssClass="btn btn-primary btn-circle" />
                                        </asp:CommandField>
                                    </Columns>
                                    <HeaderStyle CssClass="thead-dark" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <%-- ***************************NUEVO FUNCIONARIO****************************** --%>
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
                            <h6 class="m-0 font-weight-bold text-primary">Formulario Funcionario</h6>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <%-- FORMULARIO --%>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlTipoIdentificacion" CssClass="form-control form-control-user" runat="server">
                                    </asp:DropDownList>
                                    <asp:CompareValidator ControlToValidate="ddlTipoIdentificacion" ID="CompareValidator1"
                                        CssClass="text-danger" ErrorMessage="Seleccione el tipo de identificación"
                                        runat="server" Display="Dynamic"
                                        Operator="NotEqual" ValueToCompare="0" Type="Integer" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtIdentificacion" CssClass="form-control form-control-user" runat="server" placeholder="Identificación" TextMode="SingleLine" MaxLength="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe ingresar la identificación" Display="Dynamic" 
                                        SetFocusOnError="true" CssClass="text-danger" ControlToValidate="txtIdentificacion"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtNombre" CssClass="form-control form-control-user" runat="server" placeholder="Nombre del Funcionario" TextMode="SingleLine" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe ingresar el nombre del funcionario" Display="Dynamic" 
                                        SetFocusOnError="true" CssClass="text-danger" ControlToValidate="txtNombre"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtApellidos" CssClass="form-control form-control-user" runat="server" placeholder="Apellidos del Funcionario" TextMode="SingleLine" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Debe ingresar los apellidos del funcionario" Display="Dynamic" 
                                        SetFocusOnError="true" CssClass="text-danger" ControlToValidate="txtApellidos"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlTipoFuncionario" CssClass="form-control form-control-user" runat="server">
                                    </asp:DropDownList>
                                    <asp:CompareValidator ControlToValidate="ddlTipoFuncionario" ID="CompareValidator2"
                                        CssClass="text-danger" ErrorMessage="Seleccione el tipo de funcionario"
                                        runat="server" Display="Dynamic"
                                        Operator="NotEqual" ValueToCompare="0" Type="Integer" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtCorreo" CssClass="form-control form-control-user" runat="server" placeholder="Correo del Funcionario" TextMode="SingleLine" MaxLength="300"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Debe ingresar el correo del funcionario" Display="Dynamic" 
                                        SetFocusOnError="true" CssClass="text-danger" ControlToValidate="txtCorreo"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtContrasena" CssClass="form-control form-control-user" runat="server" placeholder="Contraseña del Funcionario" TextMode="SingleLine" MaxLength="50"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton CssClass="btn btn-outline-dark" ID="lnkAutogenerate" runat="server" CausesValidation="False" OnClick="lnkAutogenerate_Click">Autogenerar</asp:LinkButton>
                                        </span>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Debe ingresar la contraseña del funcionario" Display="Dynamic"
                                            SetFocusOnError="true" CssClass="text-danger" ControlToValidate="txtContrasena"></asp:RequiredFieldValidator>
                                </div>

                                <%-- FIN DE FORMULARIO --%>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <%-- **********************CAMBIO DE CONTRASENA******************* --%>
                <asp:View ID="viewChangePassword" runat="server">
                    <div class="card mb-4 py-3 border-left-primary">
                        <div class="card-body">
                            <asp:LinkButton ID="lnkCancelPassword" CssClass="btn btn-primary btn-circle" Text="<span class='icon text-white-50'><i class='fas fa-arrow-left'></i></span><span class='text'></span>" runat="server" OnClick="lnkCancelPassword_Click" ToolTip="Cancelar" CausesValidation="False"></asp:LinkButton>
                            <asp:LinkButton ID="lnkSavePassword" CssClass="btn btn-primary btn-circle" Text="<span class='icon text-white-50'><i class='fas fa-save'></i></span><span class='text'></span>" runat="server" OnClick="lnkSavePassword_Click" ToolTip="Guardar contraseña"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="alert alert-dark" role="alert">
                        <h4 class="alert-heading">Cambio de contraseña</h4>
                        <p>En esta pantalla usted procederá a cambiar con el cambio de contraseña del usuario seleccionado, por favor verifique que desea realizar la acción.</p>
                        <hr>
                        <p class="mb-0">Funcionario seleccionado: <asp:Literal ID="ltlFData" runat="server"></asp:Literal></p>
                    </div>
                    <div class="card-body">
                            <div class="table-responsive">
                                <%-- FORMULARIO --%>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtNewPassword" CssClass="form-control form-control-user" runat="server" placeholder="Contraseña del Funcionario" TextMode="SingleLine" MaxLength="50"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton CssClass="btn btn-outline-dark" ID="lnkBtnGenerateNewPassword" runat="server" CausesValidation="False" OnClick="lnkBtnGenerateNewPassword_Click">Autogenerar</asp:LinkButton>
                                        </span>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Debe ingresar la contraseña del funcionario" Display="Dynamic"
                                            SetFocusOnError="true" CssClass="text-danger" ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator>
                                </div>
                                <%-- FIN DE FORMULARIO --%>
                            </div>
                        </div>
                </asp:View>
                <%-- **************************AREA FUNCIONARIO********************* --%>
                <asp:View ID="viewAreas" runat="server">
                    <div class="card mb-4 py-3 border-left-primary">
                        <div class="card-body">
                            <asp:LinkButton ID="lnkCancelarAreas" CssClass="btn btn-primary btn-circle" Text="<span class='icon text-white-50'><i class='fas fa-arrow-left'></i></span><span class='text'></span>" runat="server" OnClick="LinkButton1_Click" ToolTip="Cancelar" CausesValidation="False"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="alert alert-dark" role="alert">
                        <%--<h4 class="alert-heading">Administrar áreas del funcionario</h4>
                        <p>En esta pantalla usted procederá asociar las áreas de un usuario.</p>
                        <hr>--%>
                        <p class="mb-0">Funcionario seleccionado: <asp:Literal ID="ltlAreasUsuario" runat="server"></asp:Literal></p>
                    </div>
                    <div class="card-body">
                        <div class="table-responsive">
                            <div class="form-group">
                                <asp:TextBox ID="txtCodigoFuncionario" CssClass="form-control form-control-user" runat="server" placeholder="Ingrese el código del funcionario" TextMode="SingleLine" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Debe ingresar el código del funcionario" Display="Dynamic"
                                    SetFocusOnError="true" CssClass="text-danger" ControlToValidate="txtCodigoFuncionario"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlAreaFuncionario" CssClass="form-control form-control-user" runat="server">
                                </asp:DropDownList>
                                <asp:CompareValidator ControlToValidate="ddlAreaFuncionario" ID="CompareValidator3"
                                    CssClass="text-danger" ErrorMessage="Seleccione el tipo de área"
                                    runat="server" Display="Dynamic"
                                    Operator="NotEqual" ValueToCompare="0" Type="Integer" />
                            </div>
                            <div class="form-group">
                                <asp:LinkButton ID="lnkAgregarArea" CssClass="btn btn-primary" OnClick="LinkButton1_Click1" runat="server">Asociar nueva área-código</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                            <div class="table-responsive">
                                <asp:PlaceHolder runat="server" ID="SelectedAreaMessage" Visible="false">
                                    <span class="badge badge-secondary">
                                        <asp:Literal runat="server" ID="ltlAreaSelected" /></span>
                                </asp:PlaceHolder>
                                <asp:GridView CssClass="table table-borderless table-sm table-hover table-striped" HeaderStyle-CssClass="thead-dark" ID="gvAreas" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvAreas_PageIndexChanging" OnRowDataBound="gvAreas_RowDataBound" OnRowDeleting="gvAreas_RowDeleting" OnSelectedIndexChanging="gvAreas_SelectedIndexChanging">
                                    <Columns>
                                        <asp:CommandField  ShowSelectButton="True" HeaderText="S." SelectText="&lt;i class='fas fa-arrow-circle-right'&gt;&lt;/i&gt;"  />
                                        <asp:BoundField DataField="CodigoFuncionario" HeaderText="ID" ReadOnly="True" />
                                        <asp:TemplateField HeaderText="Área">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# ObtenerNombreArea(int.Parse(Eval("AreaID").ToString()))  %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlAreaFuncionarioGrid" DataSource='<%# listaAreas %>' DataTextField="Nombre" DataValueField="AreaID" CssClass="form-control form-control-user" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ControlStyle-CssClass="btn btn-primary btn-circle" HeaderText="Borrar?" DeleteText="&lt;i class='fas fa-trash'&gt;&lt;/i&gt;" ShowDeleteButton="True" >
                                        <ControlStyle CssClass="btn btn-primary btn-circle" />
                                        </asp:CommandField>
                                    </Columns>
                                    <HeaderStyle CssClass="thead-dark" />
                                </asp:GridView>
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
            <asp:AsyncPostBackTrigger ControlID="btnChangePassword" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkAgregarArea" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkAutogenerate" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkSavePassword" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkCancelarAreas" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkBtnGenerateNewPassword" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkCancelPassword" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkArea" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvAreas" EventName="RowDeleting" />
            <asp:AsyncPostBackTrigger ControlID="gvAreas" EventName="PageIndexChanging" />
       </Triggers>
    </asp:UpdatePanel>
</asp:Content>
