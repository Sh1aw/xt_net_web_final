$("table").change(function (e) {
    if (!$(e.target).hasClass("changeRole")) {
        return;
    }
    let target = e.target;
    let userId = target.closest("tr").id.split('-')[1];
    let roleId = $(target).val();
    console.log(roleId);
    $.ajax({
        type: "POST",
        url: "/Pages/ActionHandlers/UserActions/ChangeRole",
        data: { "userId": userId, "roleId": roleId },
        statusCode: {
            400: function () {
                alert("Ошибка изменения");
            },
            200: function () {
                alert("Успешно изменена!");
            },
            302: function () {
                alert("Вы не можете поменять роль самому себе!");
            },
            500: function () {
                alert("Ошибка на стороне сервера!");
            },
        },
    });
});