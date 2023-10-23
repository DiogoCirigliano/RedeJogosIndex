using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Google.Protobuf.WellKnownTypes;
using MySqlX.XDevAPI;
using Microsoft.Win32;

namespace RedeJogos_37652
{
    public partial class index : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string SVGFavoritar = "<svg class=\"favoritar\" xmlns=\"http://www.w3.org/2000/svg\" x=\"0px\" y=\"0px\" width=\"32\" height=\"32\"\r\n                    viewBox=\"0 0 32 32\">\r\n                    <path\r\n                        d=\"M 7 5 L 7 28 L 8.59375 26.8125 L 16 21.25 L 23.40625 26.8125 L 25 28 L 25 5 Z M 9 7 L 23 7 L 23 24 L 16.59375 19.1875 L 16 18.75 L 15.40625 19.1875 L 9 24 Z\">\r\n                    </path>\r\n                </svg>";
            string linhaConexao = "SERVER=localhost;UID=root;PASSWORD=root;DATABASE=RedeJogos";
            MySqlConnection conexao = new MySqlConnection(linhaConexao);
            MySqlDataReader dados = null;
            try
            {
                conexao.Open();
                pnlErroconexao.Visible = false;
                form1.Visible = true;

                if (!IsPostBack)
                {

                    #region dados usuário
                    string dadouser = "SELECT \r\n    u.nm_usuario,\r\n    u.ds_usuario,\r\n    ROUND(COALESCE(avg(qt_avaliacao), 0), 1) AS media_avaliacao,\r\n    (SELECT COUNT(*) FROM favorito WHERE nm_email_favoritado = u.nm_email) AS total_favoritos\r\nFROM\r\n    usuario u\r\nLEFT JOIN\r\n    avaliacao a ON u.nm_email = a.nm_email\r\nGROUP BY\r\n    u.nm_email, u.nm_usuario, u.ds_usuario\r\nORDER BY\r\n    media_avaliacao DESC\r\nLIMIT 5;";
                    MySqlCommand cSQLperfil = new MySqlCommand(dadouser, conexao);
                    dados = cSQLperfil.ExecuteReader();

                    string perfilHtml = "";

                    while (dados.Read())
                    {
                        string nomeuser = dados.GetString(0);
                        string dsuser = dados.GetString(1);
                        double mediaValor = dados.GetDouble(2);
                        mediaValor = mediaValor / 5 * 100;

                        #region Avaliação usuário
                        string avaliacao = $@"<div class=""progress-container"">
                                            <div class=""progress-bar"">
                                            <div class=""progress"" id=""progress"" style=""width:{mediaValor.ToString("")}%""></div>
                                            </div>
                                            <img class=""estrelas"" src=""image/estrelas.png"">
                                        </div>";
                        #endregion

                        #region Perfil usuário
                        perfilHtml += $@"<main class=""perfil"" onclick=""window.location.href = ""perfil.html"";"">
                                            {SVGFavoritar}
                                            <div class=""conteudo"">
                                                <div class=""imglabel"">
                                                 <img name=""perfil""
                                                 src=""https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRSABPZ8GZJBVIzqQKWKZFfy1LnTN9bUEsrUCrzPKSE9Hw1OuOFuTYFmv_6M5b-4ePI1cI&usqp=CAU""
                                                 alt=""foto de perfil"">
                                                <label for=""perfil"">{nomeuser}</label>
                                                </div>
                                                {avaliacao}
                                                <p class=""ds"">{dsuser}</p>
                                            </div>
                                        </main>";
                        #endregion


                    }
                    dados.Close();

                    litperfil.Text = perfilHtml;

                    #endregion

                    #region dados do jogo
                    string dadojogo = "select\r\n    j.nm_jogo,\r\n    count(b.nm_email) as qtd_jogadores\r\nfrom jogo j, biblioteca b\r\nwhere j.cd_jogo = b.cd_jogo\r\ngroup by j.cd_jogo\r\norder by qtd_jogadores desc\r\nlimit 0,5;";
                    MySqlCommand cSQLjogo = new MySqlCommand(dadojogo, conexao);
                    dados = cSQLjogo.ExecuteReader();

                    string jogoHtml = "";

                    #region jogo html
                    while (dados.Read())
                    {
                        string nmjogo = dados.GetString(0);
                        jogoHtml += $@"  <main class=""jogo"" onclick=""window.location.href = 'jogo.html';"">
                                        <div class=""fotojogo""><img src=""https://www.fepam.rs.gov.br/themes/padrao2019/images/outros/GD_imgSemImagem.png""
                                        alt=""""></div>
                                        <div class=""nmjogo"">
                                        <p>{nmjogo}</p>
                                        </div>
                                    </main>";

                    }
                    #endregion

                    dados.Close();
                    litjogo.Text = jogoHtml;
                    #endregion

                }

            }
            catch
            {
                lblerro.Text = "Erro inesperado. Recarregue a página";
                form1.Visible = false;
                pnlErroconexao.Visible = true;
                lblerro.ForeColor = Color.Red;
            }
            finally
            {
                if (dados != null)
                {
                    if (!dados.IsClosed)
                    {
                        dados.Close();
                    }
                }

                if (conexao != null)
                    if (conexao.State == System.Data.ConnectionState.Open)
                        conexao.Close();
            }
        }
    }
}