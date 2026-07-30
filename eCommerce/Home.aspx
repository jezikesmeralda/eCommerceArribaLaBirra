<%@ Page Title="Home" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="eCommerce.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <h2 class="mb-4 text-center">Catálogo de Coleccionables</h2>

        <div class="row">
            <!-- Usamos un Repeater para iterar la lista de productos en formato de Cards -->
            <asp:Repeater ID="repProductos" runat="server">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card h-100 shadow-sm">
                            
                            <!-- Imagen del Producto (Si tiene imágenes asociadas, mostramos la primera, sino una por defecto) -->
                            <img src='<%# Eval("Imagenes") != null && ((List<dominio.Imagen>)Eval("Imagenes")).Count > 0 ? ((List<dominio.Imagen>)Eval("Imagenes"))[0].ImagenUrl : "https://via.placeholder.com/300x200?text=Sin+Imagen" %>' 
                                 class="card-img-top" alt="Imagen del producto" style="height: 220px; object-fit: cover;" />

                            <div class="card-body d-flex flex-column">
                                <!-- Categoría o Marca como etiqueta pequeña -->
                                <span class="badge bg-secondary mb-2 align-self-start">
                                    <%# Eval("Marca") != null ? Eval("Marca.Nombre") : "Sin Marca" %>
                                </span>

                                <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                <p class="card-text text-muted small"><%# Eval("Descripcion") %></p>
                                
                                <div class="mt-auto">
                                    <div class="d-flex justify-content-between align-items-center mb-2">
                                        <span class="fs-5 fw-bold text-success">$<%# Eval("Precio") %></span>
                                        <span class="text-secondary small">Stock: <%# Eval("Stock") %></span>
                                    </div>

                                    <!-- Botones de acción -->
                                    <div class="d-grid gap-2">
                                        <a href='DetalleProducto.aspx?id=<%# Eval("Id") %>' class="btn btn-outline-primary btn-sm">Ver Detalle</a>
                                        <asp:Button ID="btnAgregarCarrito" runat="server" Text="Agregar al Carrito" CssClass="btn btn-primary btn-sm" CommandArgument='<%# Eval("Id") %>' OnClick="btnAgregarCarrito_Click" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>
