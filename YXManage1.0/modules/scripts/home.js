

define(function (require, exports, module) {

    require("jquery");
    var Verify = require("verify"),
        VerifyObject,
        Global = require("global");

    var Home = {};
    //
    Home.loginInit = function () {
        Home.bindEvent();
    }
    Home.bindEvent = function () {
        VerifyObject = Verify.createVerify({
            element: ".verify",
            emptyAttr: "data-empty",
            verifyType: "data-type",
            regText: "data-text"
        });

        $(document).on("keypress", function (e) {
            if (e.keyCode == 13) {
                $("#btnLogin").click();
            }
        });

        $("#btnLogin").click(function () {
            if (!VerifyObject.isPass()) {
                return;
            }
            Global.post("/Home/UserLogin", { userName: $("#iptUserName").val(), pwd: $("#iptPwd").val() }, function (data) {
                if (data.result) {
                    location.href = "/Home/Index";
                } else {
                    alert("账户或密码不正确！")
                }
            });
        });

    }

    module.exports = Home;
});