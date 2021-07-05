$(".form-holder #eyeIcon").click(function () {

    $(this).toggleClass("fa-eye fa-eye-slash");
    var input = $($(this).attr("password"));
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});

//$(function () {
//    $("#eyeIcon").click(function () {
//        $(this).toggleClass("fa-eye fa-eye-slash");
//        var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
//        $("#txtPassword").attr("type", type);
//    });
//});
