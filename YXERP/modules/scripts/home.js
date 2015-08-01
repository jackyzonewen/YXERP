

define(function (require, exports, module) {

    require("jquery");
    var Global = require("global");

    var Home = {};
    //登陆初始化
    Home.loginInit = function () {
        Home.bindEvent();
        
    }
    //绑定事件
    Home.bindEvent = function () {

        $(document).on("keypress", function (e) {
            if (e.keyCode == 13) {
                $("#btnLogin").click();
            }
        });

        $("#btnLogin").click(function () {
            if (!$("#iptUserName").val()) {
                alert("请输入账号！");
                return;
            }
            if (!$("#iptPwd").val()) {
                alert("请输入密码！");
                return;
            }
            Global.post("/Home/UserLogin", { userName: $("#iptUserName").val(), pwd: $("#iptPwd").val() }, function (data) {
                if (data) {
                    location.href = "/Home/Index";
                } else {
                    alert("账号或密码不正确！")
                }
            });
        });

        $("#iptUserName").focus();

    }

    module.exports = Home;
});