
function addItem(articleId, quantity) {

    // Just ot check if the cart still exists
    var cartGuid = (sessionStorage.getItem("CartGuid") !== null && sessionStorage.getItem("CartGuid").length > 0) ?
        sessionStorage.getItem("CartGuid") : "0";

    $.ajax({
        type: 'GET',
        url: './api/Cart/' + cartGuid,
        cache: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            // Invalid cart, pass 0 to create a new one
            if (eval(data) === null)
            {
                sessionStorage.setItem("CartGuid", "0");
                addToCart(articleId, quantity);
                return;
            }
            
            sessionStorage.setItem("CartGuid", eval(data).Guid);

            $.ajax({
                type: 'PUT',
                url: './api/Cart/' + sessionStorage.getItem("CartGuid") + '/Items/' + articleId + '/' + quantity,
                cache: false,
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                }
            });
        }
    });
}

function removeItem(articleId, quantity) {

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
                addToCart(articleId, quantity);
                return;
            }

            sessionStorage.setItem("CartGuid", eval(data).Guid);

            $.ajax({
                type: 'DELETE',
                url: './api/Cart/' + sessionStorage.getItem("CartGuid") + '/Items/' + articleId + '/' + quantity,
                cache: false,
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                }
            });
        }
    });
}

function getCart() {
    // Just ot check if the cart still exists
    var cartGuid = sessionStorage.getItem("CartGuid") !== null ?
        sessionStorage.getItem("CartGuid") : "0";

    $.ajax({
        type: 'GET',
        url: './api/Cart/' + cartGuid,
        cache: false,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            // Invalid cart, pass 0 to create a new one
            if (eval(data) === null) {
                sessionStorage.setItem("CartGuid", "0");
                return;
            }

            return eval(data);
        }
    });
}