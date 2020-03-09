$("#adminContainer").click(function (e) {
    let urle = "";
    if ($(e.target).hasClass("saveEditType")) {
        urle = "/Pages/ActionHandlers/TypeActions/EditType";
    }
    else if ($(e.target).hasClass("saveEditCat")) {
        urle = "/Pages/ActionHandlers/CategoryActions/EditCategory";
    }
    else {
        return;
    }
    let target = e.target;
    let newTittle = target.closest("tr").querySelector("[type = 'text']").value;
    let id = target.closest("tr").id.split('-')[1];
    $.ajax({
        type: "POST",
        url: urle,
        data: { "itemId": id, "itemTittle": newTittle },
        statusCode: {
            400: function () {
                alert("Ошибка изменения");
            },
            200: function () {
                alert("Успешно изменен!");
            },
            404: function () {
                alert("Запрашиваемый объект не найден!");
            },
        },
    });
});

$("#adminContainer").click(function (e) {
    let urle = "";
    if ($(e.target).hasClass("removeType")) {
        urle = "/Pages/ActionHandlers/TypeActions/RemoveType";
    }
    else if ($(e.target).hasClass("removeCat")){
        urle = "/Pages/ActionHandlers/CategoryActions/RemoveCategory";
    }
    else {
        return;
    }
    let target = e.target;
    let id = target.closest("tr").id.split('-')[1];
    $.ajax({
        type: "POST",
        url: urle,
        data: { "itemId": id },
        statusCode: {
            404: function () {
                alert("Запрашиваемый объект не найден!");
            },
            200: function () {
                target.closest("tr").remove();
            },
            302: function () {
                alert("Данную категорию/тип уже имеют некоторые товары, удаление невозможно!");
            },
            500: function () {
                alert("Ошибка на стороне сервера!");
            },
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
        data: { "itemTittle": newTittle },
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
$("#addType").click(function (e) {
    let newTittle = $("#inputTypeTittle").val();
    if (!newTittle.trim().length > 0) {
        alert("Наименование типа не может быть пустым!");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Pages/ActionHandlers/TypeActions/AddType",
        data: { "itemTittle": newTittle },
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