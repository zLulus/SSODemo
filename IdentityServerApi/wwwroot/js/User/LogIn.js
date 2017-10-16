function tryLogIn() {
    var account = $("#account").val();
    var password = $("#password").val();
    $.ajax({
        type: "post",
        url: '/User/TryLogIn',
        data:
        {
            account: account,
            password: password
        },
        success: function (data) {
            console.log(data);
            if (data.logResult) {
                window.location.href = data.msg; 
            }
            else {
                alert(data.msg);
            }
        }
    });
}