$(function () {
    var PostCall = function (url, data) { 
        var json_data = JSON.stringify(data);
        return $.ajax({
            type: "POST",
            url: url,
            data: json_data,
            dataType: "json",
            contentType: "application/json;charset=utf-8"
        });
    }
   
    $("button.login").click(function () {
        $(".error_message").html("loading...");
        var username = $("#username").val();
        var password = $("#password").val();
        if (username == "") {
            $(".error_message").html("<font color=red>Error! Username /or password required</font>");
            return;
        }
        if (password == "") {
            $(".error_message").html("<font color=red>Error! Username /or password required</font>");
            return;
        }
        PostCall("https://healthassessmentapi.herokuapp.com/api/Account/authenticate",
            {
                "email": "superadmin@gmail.com",
                "password": "Password@123"
            }).success(function (data) {
                // treat the READUSERS data returned
            }).fail(function (sender, message, details) {
                alert("Sorry, something went wrong!");
            });
    });
});