$("#categoryTable").click(function (e) {
    if (!$(e.target).hasClass("saveEditCat")) {
        return;
    }
    let target = e.target;
    let newTittle = target.closest("tr").querySelector("[type = 'text']").value;
    let id = target.closest("tr").id.split('-')[1];
    $.ajax({
        type: "POST",
        url: "/Pages/ActionHandlers/CategoryActions/EditCategory",
        data: { "categoryId": id, "categoryTittle": newTittle },
        statusCode: {
            400: function () {
                alert("Ошибка изменения");
            },
            200: function () {
                alert("Категория успешно изменена!");
            },
        },
    });
});

$("#categoryTable").click(function (e) {
    if (!$(e.target).hasClass("removeCat")) {
        return;
    }
    let target = e.target;
    let id = target.closest("tr").id.split('-')[1];
    $.ajax({
        type: "POST",
        url: "/Pages/ActionHandlers/CategoryActions/RemoveCategory",
        data: { "categoryId": id },
        statusCode: {
            400: function () {
                alert("Ошибка удаления");
            },
            200: function () {
                target.closest("tr").remove();
            },
            302: function () {
                alert("Данную категорию уже имеют некоторые товары, удаление невозможно!");
            }
        },
    });
});

$("#addCategory").click(function (e) {
    let newTittle = $("#inputCategoryTittle").val();
    if (!newTittle.trim().length > 0) {
        alert("Наименование категории не может быть пустым!");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Pages/ActionHandlers/CategoryActions/AddCategory",
        data: { "categoryTittle": newTittle },
        statusCode: {
            400: function () {
                alert("Ошибка добавления");
            },
            200: function () {
                location.reload();
            },
        },
    });
});