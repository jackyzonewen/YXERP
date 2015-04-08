

define(function (require, exports, module) {

    require("jquery");
    var Verify = require("verify"),
        Global = require("global"),
        City = require("city");
    var VerifyObject, CityObject;
    var Clients = {};
    //
    Clients.createInit = function () {
        Clients.bindEvent();
    }
    Clients.bindEvent = function () {
        //验证插件
        VerifyObject = Verify.createVerify({
            element: ".verify",
            emptyAttr: "data-empty",
            verifyType: "data-type",
            regText: "data-text"
        });
        //城市插件
        CityObject = City.createCity({
            elementID: "citySpan"
        });
        //选择模块
        $(".modules-item").first().addClass("active");
        $(".modules-item:gt(0)").click(function () {
            var _this = $(this);
            if (_this.hasClass("active")) {
                _this.removeClass("active");
            } else {
                _this.addClass("active");
            }
        });

        $("#saveClient").click(function () {
            if (!VerifyObject.isPass()) {
                return false;
            };
            var client = {
                CompanyName: $("#").val(),

            };

            Global.post("/Client/CreateClient", { client: JSON.stringify(client) }, function (data) {

            })
        });
    }

    module.exports = Clients;
});