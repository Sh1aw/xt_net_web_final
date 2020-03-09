$("#eddit_allow").click(function (e) {
    $(".personal_info input").each(function (r) {
        this.removeAttribute("disabled");
    });
    $("#eddit_allow").hide();
    $("#reset_password_allow").hide();
    $(".personal_info form").append("<input class='btn' type='submit' value='Сохранить'>");
});
$("#reset_password_allow").click(function (e) {
    $(".personal_info").hide();
    $(".reset_password").show();
});