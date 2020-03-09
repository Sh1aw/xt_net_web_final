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