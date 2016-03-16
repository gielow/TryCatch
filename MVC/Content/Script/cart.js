
function addItem(articleId, quantity) {

    itemAction(articleId, quantity, 'PUT', 'Article has been successfully added!');
}

function removeItem(articleId, quantity) {

    itemAction(articleId, quantity, 'DELETE', 'Article has been successfully removed!');

    window.location.reload();
}

function itemAction(articleId, quantity, action, message) {
    var cartGuid = getCartGuid();
    
    $.ajax({
        type: action,
        url: './api/Cart/' + cartGuid + '/Items/' + articleId + '/' + quantity,
        cache: false,
        contentType: 'application/json',
        headers: { Accept: "application/json" },
        success: function (data) {
            alert(message);
        },
        error: function () {
            return false;
        }
    });
}

function getCartGuid() {
    if (sessionStorage.getItem("CartGuid") !== null && sessionStorage.getItem("CartGuid").length > 0)
        return sessionStorage.getItem("CartGuid");

    $.ajax({
        type: 'GET',
        url: './api/Cart/Index/New',
        cache: false,
        contentType: 'application/json',
        headers: { Accept: "application/json" },
        success: function (data) {
            sessionStorage.setItem("CartGuid", eval(data).Guid);

            return eval(data).Guid;
        },
        error: function () {
            console.error('Error at creating new session cart');
        }
    });
}

getCartGuid();