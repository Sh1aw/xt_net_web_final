$(".items_table").click(function (e) {
    e.stopPropagation();
    let target = e.target;
    if (!$(target).hasClass('addToCart')) {
        return;
    }
    let id = target.id;

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
$(".items_table").click(function (e) {
    let target = e.target;
    if (!$(target).hasClass('hideProduct')) {
        return;
    }
    let id = target.closest(".item_container").id.split("-")[1];
    console.log(id);
    $.ajax({
        type: "POST",
        url: "/Pages/ActionHandlers/ProductActions/HideProduct",
        data: { "IdProduct": id },
        statusCode: {
            200: function () {
                $("#targetItemsTable").load("/Pages/Partials/PartialProducts.cshtml");
            },
            500: function () {
                alert("Ошибка на стороне сервера!");
            },
        }
    });
});
$(".items_table").click(function (e) {
    let target = e.target;
    if (!$(target).hasClass('showProduct')) {
        return;
    }
    let id = target.closest(".item_container").id.split("-")[1];
    console.log(id);
    $.ajax({
        type: "POST",
        url: "/Pages/ActionHandlers/ProductActions/ShowProduct",
        data: { "IdProduct": id },
        statusCode: {
            200: function () {
                $("#targetItemsTable").load("/Pages/Partials/PartialProducts.cshtml");
            },
            500: function () {
                alert("Ошибка на стороне сервера!");
            },
        }
    });
});

$(".items_table").click(function (e) {
    let target = e.target;
    if (!$(target).hasClass('deleteProduct')) {
        return;
    }
    if (!confirm("Вы уверены, что хотите удалить данный продукт?")) {
        return;
    }
    let id = target.closest(".item_container").id.split("-")[1];
    console.log(id);
    $.ajax({
        type: "POST",
        url: "/Pages/ActionHandlers/ProductActions/DeleteProduct",
        data: { "IdProduct": id },
        statusCode: {
            200: function () {
                $("#targetItemsTable").load("/Pages/Partials/PartialProducts.cshtml");
            },
            300: function () {
                alert("Данный продукт уже учавствовал в заказах! Его нельзя удалить, но вы можете скрыть продукт от новых покупателей");
            },
            500: function () {
                alert("Ошибка на стороне сервера!");
            }
        }
    });
});

$("#categorySort").change(function (e) {
    let idCategory = $("#categorySort").val();
    let idType = $("#typeSort").val();
    $.ajax({
        url: '/Pages/Partials/PartialProducts.cshtml',
        data: { "category": idCategory, "type": idType },
        type: "GET",
        success: function (data) {
            $('#targetItemsTable').html(data);
        },
    });
});
$("#typeSort").change(function (e) {
    let idType = $("#typeSort").val();
    let idCategory = $("#categorySort").val();
    $.ajax({
        url: '/Pages/Partials/PartialProducts.cshtml',
        data: { "type": idType, "category": idCategory },
        type: "GET",
        success: function (data) {
            $('#targetItemsTable').html(data);
        },
    });
});
$("#visibilitySort").change(function (e) {
    let idVisibility = $("#visibilitySort").val();
    $.ajax({
        url: '/Pages/Partials/PartialProducts.cshtml',
        data: { "visibility": idVisibility },
        type: "GET",
        success: function (data) {
            $('#targetItemsTable').html(data);
        },
    });
});