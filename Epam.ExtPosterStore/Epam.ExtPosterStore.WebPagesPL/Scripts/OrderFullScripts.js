$("#abort_order").click(function (e) {
    if (confirm("Вы уверены что хотите отменить этот заказ?")) {
        let idOrder = $("#order_id").val();
        $.ajax({
            type: "POST",
            url: "/Pages/ActionHandlers/OrderActions/AbortOrder",
            data: { "IdOrder": idOrder },
            success: function (response) {
                response = JSON.parse(response);
                console.log(response);
                if (response.success) {
                    $("#orderState").text(response.responseText);
                    alert("Заказ успешно отменен!");
                } else {
                    alert("Ошибка отмены заказа!");
                }
            },
        });
    }
});

$("#change_status").click(function (e) {
    if (confirm("Вы уверены что хотите изменить статус этого заказа?")) {
        let idOrder = $("#order_id").val();
        let idState = $("#order_states").val();
        $.ajax({
            type: "POST",
            url: "/Pages/ActionHandlers/OrderActions/ChangeState",
            data: { "IdOrder": idOrder, "IdState": idState },
            success: function (response) {
                response = JSON.parse(response);
                if (response.success) {
                    $("#orderState").text(response.responseText);
                    alert("Статус успешно измене!");
                } else {
                    alert("Ошибка!");
                }
            },
        });
    }
});