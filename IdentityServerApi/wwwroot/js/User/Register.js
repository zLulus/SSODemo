function tryLogIn() {
    var account = $("#account").val();
    var password = $("#password").val();
    var repeatPassword = $("#repeatPassword").val();
    if (password != repeatPassword) {
        alert("两次输入密码不一致!");
        return;
    }
    $.ajax({
        type: "post",
        url: '/User/TryAddUser',
        data:
        {
            account: account,
            password: password
        },
        success: function (data) {
            console.log(data);
            if (data.addUserResult) {
                window.location.href = data.msg;
            }
            else {
                alert(data.msg);
            }
        }
    });
}