function loadArticles(pageNumber) {
    
    $.ajax({
        type: 'GET',
        url: './api/Article/page/' + pageNumber,
        cache: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#articles > tbody > tr').remove();
            $.each(eval(data), function (index, item) {
                $('#articles > tbody').append("<tr><td>" + item.Description + "</td><td>" + item.Price + "</td>" +
                    "<td><a href='#' onclick='javascript:addItem(" + item.Id + ", 1)'>Add to cart</a></td></tr>");
            });
        }
    });
}

$(document).ready(function () {
    loadArticles(1);
});
