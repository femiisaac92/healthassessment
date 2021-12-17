$(function () {
    // Clear session storage
    if (typeof (Storage) !== "undefined") {
        localStorage.setItem("token", "");
        localStorage.setItem("refreshToken", "");
        localStorage.setItem("username", "");
        localStorage.setItem("email", "");       
        // Code for localStorage/sessionStorage.
    } else {
        alert("Sorry Browser not supported")
    }
    //End Clear Session storage

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
                "email": username,
                "password": password
            }).success(function (data) {
                if (data.succeeded === true) {
                    var username = data.data.userName;
                    var email = data.data.email;
                    var token = data.data.jwToken;
                    var refreshToken = data.data.refreshToken;
                    if (typeof (Storage) !== "undefined") {
                        localStorage.setItem("token", token);
                        localStorage.setItem("refreshToken", refreshToken);
                        localStorage.setItem("username", username);
                        localStorage.setItem("email", email);
                        document.location.href="home.html"
                        // Code for localStorage/sessionStorage.
                    } else {
                        alert("Sorry Browser not supported")
                    }
                } else {
                    $(".error_message").html("<font color=red>Error! Username /or password not found</font>");
                }
                // treat the READUSERS data returned
            }).fail(function (sender, message, details) {
                alert("Sorry, no account registered with this crediential");
                $(".error_message").html("<font color=red>Error! Username /or password not found</font>");
            });
    });
});