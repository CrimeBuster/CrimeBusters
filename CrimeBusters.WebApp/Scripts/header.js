$(function () {
   
    $("#signOut").on("click", function (e) {
        e.preventDefault();
        $.logOffUser();
    });
});

(function ($) {
    $.logOffUser = function () {
        $.ajax({
            type: "POST",
            dataType: "json",
            timeout: 10000,
            contentType: "application/json",
            url: "../Services/Login.asmx/LogOutUser",
            success: function (data) {                
                window.location.href = 'Login.aspx';
            },
            error: function () {
                alert("Unable to communicate with the server. Please try again.");
            }            
        });
    };
})(jQuery);

(function ($) {
    $.getUserName = function () {
        $.ajax({
            type: "POST",
            dataType: "json",
            timeout: 10000,
            contentType: "application/json",
            url: "../Services/Login.asmx/GetUser",
            success: function (data) {
                $("#userLoginName").text(data.d);
            },
            error: function () {
                alert("Unable to communicate with the server. Please try again.");
            }
        });
    };
})(jQuery);


$(document).ready(function () {
    $.getUserName();
})