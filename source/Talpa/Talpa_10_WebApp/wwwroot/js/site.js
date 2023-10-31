function changeLocaleAsync(locale) {
    $.ajax({
        type: "GET",
        url: "/SetLocale",
        data: {
            "locale": locale
        },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (xhr, status, error) {
            console.error(xhr, status, error);
        },
        success: function (result) {
            location.reload();
        }
    });
}