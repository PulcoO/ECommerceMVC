// button add and remove
function addToCart(path) {
    $.ajax({
        type: "GET",
        url: path,
        contentType: "html",
        success: function (html) {
            $('#cartSection').html(html);
        },
        error: function (msg) {
            console.log("error")
        }
    });
    $(".cart").addClass("active")
};
function removeToCart(path) {
    $.ajax({
        type: "GET",
        url: path,
        contentType: "html",
        success: function (html) {
            $('#cartSection').html(html);
        },
        error: function (msg) {
            console.log("error")
        }
    });
    $(".cart").addClass("active")
};




        //$(document).ready(function () {
        //    $(".cart__").click(function (event) {
        //        console.log(event)
        //        $(".cart").removeClass("active");
        //        $(".overlay").removeClass("active");
        //    });
        //    $(".icon-close").click(function (event) {
        //        console.log(event)
        //        $(".cart").removeClass("active");
        //        $(".overlay").removeClass("active");
        //    });
        //});
        //rattache le card a sa donnée a chaque chargement de la page
        //$(document).ready(function () {
        //    $("#cartSection").load('<%= Url.Action("OnGetPartial", "Cart") %>');
        //});