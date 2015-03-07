

define(function (require, exports, module) {

    require("jquery");
    var Verify = require("verify"),
        VerifyObject;

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
        $("#btnLogin").click(function () {
            if (!VerifyObject.isPass()) {
                return;
            }
            
        });
    }

    module.exports = Home;
});