$("table").click(function (e) {
    e.stopPropagation();
    let target = e.target;
    if (target.className != 'remove_item') {
        return;
    }
    let id = target.id;
    $.ajax({
        type: "POST",
        url: "/Pages/ActionHandlers/CartActions/CartRemove",
        data: { "IdProduct": id },
        success: function (response) {
            response = JSON.parse(response);
            console.log(response);
            if (response.success) {
                $("#c_count").text(response.responseText);
                $("#fin_cost").text(response.newCost);
                target.closest("tr").remove();
                if (response.responseText === 0) {
                    $("#orderComplete").remove();
                }
            } else {
                alert(response.responseText);
            }
        },
    });
})