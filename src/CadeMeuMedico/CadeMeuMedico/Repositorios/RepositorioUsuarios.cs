using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CadeMeuMedico.Models;
using CadeMeuMedico.Repositorios;

namespace CadeMeuMedico.Repositorios
{
    public class RepositorioUsuarios
    {
        public static bool AutenticarUsuario(string Login, string Senha)
        {
            var SenhaCriptografada = FormsAuthentication.HashPasswordForStoringInConfigFile(Senha, "sha1");
            try
            {
                using (EntidadesCadeMeuMedicoBD db = new EntidadesCadeMeuMedicoBD())
                {
                    var QueryAutenticaUsuarios = db.Usuarios.Where(x => x.Login == Login && x.Senha == SenhaCriptografada).SingleOrDefault();

                    if (QueryAutenticaUsuarios == null)
                    {
                        return false;
                    }
                    else
                    {
                       RepositorioCookies.RegistraCookieAutenticacao(QueryAutenticaUsuarios.IDUsuario);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Usuario RecuperaUsuarioPorID(long IDUsuario)
        {
            try
            {
                using (EntidadesCadeMeuMedicoBD db = new EntidadesCadeMeuMedicoBD())
                {
                    var Usuario = db.Usuarios.Where(u => u.IDUsuario == IDUsuario).SingleOrDefault();
                    return Usuario;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Usuario VerificaSeOUsuarioEstaLogado()
        {
            var Usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
            if (Usuario == null)
            {
                return null;
            }
            else
            {
                long IDUsuario = Convert.ToInt64(RepositorioCriptografia.Descriptografar(Usuario.Values["IDUsuario"]));

                var UsuarioRetornado = RecuperaUsuarioPorID(IDUsuario);
                return UsuarioRetornado;

            }
        }
    }
}