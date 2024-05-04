$(document).ready(function () {

    var userLoginButton = $("button[name='']").click(onUserLoginClick)

    function onUserLoginClick() {
        var url = "Account/SignIn";

        var antiForgeryToken = $("input[name='_RequestVerificationToken']").val();

        var email = $("input[name='Email']").val();

        var password = $("input[name='Password']").val();

        var rememberMe = $("input[name='RememberMe']").prop('checked');

        var userInput = {
            _RequestVerificationToken: antiForgeryToken,
            Email: email,
            Password: password,
            RememberMe: rememberMe
        };


        $.ajax({
            type: "Post",
            url: url,
            data: userInput,
            success: function (data) {

                var parsed = $.parsHTMl(data);

                var hasErrors = $(parsed).find  ("input[     ]")
            }

        });
    }
});