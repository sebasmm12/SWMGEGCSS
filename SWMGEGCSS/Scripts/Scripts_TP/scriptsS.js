window.onload = function () {
    function ValidarDatos() {
        var user = document.getElementById("username");
        var $password = $("#password");
        if (user.value === null || user.length === 0 || user.value === " " || user.value === "") {
            return false;
        }
        if ($password.val() === null || $password.val() === "" || $password.val()=== " ") {
            return false;
        }
        return true;
    }
    function ValidarBd() {
        var $form = $("#form");
        var options = {
            url: form
        };
        
    }
    $("#btnlogin").click(ValidarDatos);
};