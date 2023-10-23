<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="RedeJogos_37652.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="css/style.css" rel="stylesheet" />
    <title>GameNexus</title>
</head>
<body>
    <asp:Panel ID="pnlErroconexao" runat="server"><asp:Label ID="lblerro" runat="server" Text=""></asp:Label></asp:Panel>
    <form id="form1" runat="server">
    <header>
        <div class="headercontent">
            <img class="logo" src="Icon/icons8-controle-64.png"/>


            <div class="entrar">
                <input type="text" placeholder="Pesquisar" class="input"/>
                <a href="#">Entrar</a>
                <div class="avatar"><img src="image/avatar.jpg" alt=""/></div>
            </div>
        </div>
    </header>


    <section class="perfis-destaque">
        <div class="titulo">
            <p>Perfis em destaque</p>
        </div>

        <div class="perfis">
            <asp:Literal ID="litperfil" runat="server"></asp:Literal>
        </div>

        <div class="carregar_perfil">
            <asp:Button ID="btncarregarperfil" CssClass="carregar" runat="server" Text="● ● ●"  />
        </div>

    </section>

    <section class="jogos-destaque">
        <div class="titulo-jogos">
            <p>Jogos em destaque</p>
        </div>

        <div class="jogos">
            <asp:Literal ID="litjogo" runat="server"></asp:Literal>
        </div>

        <div class="carregar_jogo">
            <asp:Button ID="btncarregarjogo" CssClass="carregar" runat="server" Text="● ● ●" />
        </div>
    </section>
    </form>
</body>
</html>
