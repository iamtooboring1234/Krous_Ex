//$(document).ready(function () {
//    $(".form-holder #eyeIcon").click(function () {

//        $(this).toggleClass("fa-eye fa-eye-slash");
//        var input = $($(this).attr("password"));
//        if (input.attr("type") == "password") {
//            input.attr("type", "text");
//        } else {
//            input.attr("type", "password");
//        }
//    });
//}); still cant do it***

$(document).ready(function () {
    $(function () {
        $("#eyeIcon").click(function () {
            $(this).toggleClass("fa-eye fa-eye-slash");
            var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
            $("#txtPassword").attr("type", type);
        });
    });
});
