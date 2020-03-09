$("#addToCart").click(function (e) {
    e.stopPropagation();
    let id = $(e.target).val();
    $.ajax({
        type: "POST",
        url: "/Pages/ActionHandlers/CartActions/CartAddHandler",
        data: { "IdProduct": id },
        success: function (response) {
            response = JSON.parse(response);
            console.log(response);
            if (response.success) {
                $("#c_count").text(response.responseText);
            }
        },
    });
});
$("#addComment").click(function (e) {
    let text = $("#textComment").val();
    if (!text.trim().length > 0) {
        alert("Заполните поле сообщения!");
        return;
    }
    let idProduct = $("#productId").val();
    console.log(text);
    console.log(idProduct);
    $.ajax({
        type: "POST",
        url: "/Pages/ActionHandlers/CommentActions/AddComment",
        data: { "TextComment": text, "IdProduct": idProduct },
        statusCode: {
            200: function () {
                $("#targetComments").load("/Pages/Partials/PartialComments.cshtml?ItemId=" + idProduct);
                $("#textComment").val("");
            },
            500: function () {
                alert("Ошибка на стороне сервера!");
            },
            404: function () {
                alert("Обьект не найден!");
            },
            400: function () {
                alert("Не верно заполнены поля");
            },
        }
    });
});
$("#targetComments").click(function (e) {
    if (!$(e.target).hasClass("removeComment")) {
        return;
    }
    let idComment = e.target.closest(".commentaryContainer").id.split('-')[1];
    $.ajax({
        type: "POST",
        url: "/Pages/ActionHandlers/CommentActions/RemoveComment",
        data: { "IdComment": idComment },
        statusCode: {
            200: function () {
                e.target.closest(".commentaryContainer").remove();
            },
            500: function () {
                alert("Ошибка на стороне сервера!");
            },
            404: function () {
                alert("Обьект не найден!");
            },
        }
    });
});