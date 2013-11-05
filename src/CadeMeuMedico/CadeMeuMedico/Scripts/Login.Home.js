$(document).ready(function () {
    $("#status").hide();
    $("#botao-entrar").click(function () {
        $.ajax({
            url: "/Usuarios/AutenticacaoDeUsuario",
            data: { Login: $("#txtLogin").val(), Senha: $("#txtSenha").val() },
            dataType: "json",
            type: "GET",
            async: true,
            beforeSend: function () {
                $("#status").html("Estamos autenticando o usuário. Só um instante...");
                $("#status").show();
            },
            success: function (dados) {
                if (dados.OK) {
                    $("#status").html(dados.Mensagem)
                    setTimeout(function () { window.location.href = "/Home/Index" }, 5000);
                    $("#status").show();
                }
                else {
                    $("#status").html(dados.Mensagem);
                    $("#status").show();
                }
            },
            error: function () {
                $("#status").html(dados.Mensagem);
                $("#status").show()
            }
        });
    });
});