$("#stateSort").change(function (e) {
    let idState = $("#stateSort").val();
    $.ajax({
        url: '/Pages/Partials/PartialOrders.cshtml',
        data: { "state": idState },
        type: "GET",
        success: function (data) {
            $('#target').html(data);
        },
    });
});

$("#target").click(function (e) {
    if (e.target.id != "dateSort") {
        return;
    }
    let dateSort = $("#dateSort").val();
    console.log(dateSort);
    $.ajax({
        url: '/Pages/Partials/PartialOrders.cshtml',
        data: { "dateSort": dateSort },
        type: "GET",
        success: function (data) {
            $('#target').html(data);
            if (dateSort === "desc") {
                $("#dateSort").val("asc");
            }
            else {
                $("#dateSort").val("desc");
            }
        },
    });
});