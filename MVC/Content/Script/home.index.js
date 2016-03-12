function loadArticles(pageNumber) {
    $.ajax({
        type: 'GET',
        url: './api/Article/page/' + pageNumber,
        cache: false,
        contentType: 'application/json; charset=utf-8',
        //data: JSON.stringify({ page: "fghfdhgfdgfd" }),
        success: function (data) {
            $('#articles > tbody > tr').remove();
            $.each(eval(data), function (index, item) {
                //alert(item.Id);
                $('#articles > tbody').append("<tr><td>" + item.Description + "</td><td>" + item.Price + "</td><td><button onclick='javascript:addItem(" + item.Id + ", 1, loadCart)'>Add to cart</button></td></tr>");
            });
        }
    });
}

function loadCart() {
    // Just ot check if the cart still exists
    var cartGuid = sessionStorage.getItem("CartGuid") !== null ?
        sessionStorage.getItem("CartGuid") : "0";

    $.ajax({
        type: 'GET',
        url: './api/Cart/' + cartGuid,
        cache: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            // Invalid cart, pass 0 to create a new one
            if (eval(data) === null) {
                sessionStorage.setItem("CartGuid", "0");
                return;
            }

            var cart = eval(data);
            $('#cart > tbody > tr').remove();

            $.each(cart.Items, function (index, item) {
                //alert(item.Id);
                $('#cart > tbody').append("<tr><td>" + item.Product.Description + "</td><td>" + item.Product.Price + "</td><td>" + item.Quantity + "</td><td>" + item.Total + "</td><td><button onclick='javascript:removeItem(" + item.Product.Id + ", 1, loadCart)'>Remove</button></td></tr>");
            });

            $('#cart > tbody').append("<tr><td colspan='2'>Total<td><td>" + cart.Total + "</td></tr>");
        }
    });
    
}

$(document).ready(function () {
    loadArticles(1);
    loadCart();
});
