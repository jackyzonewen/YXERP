

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

        //更换行业
        $("#industry").change(function () {
            $("#industryName").val("");
            if ($(this).val() == "-1") {
                $("#otherIndustry").show();
                $("#saveIndustry").hide();
            } else {
                $("#otherIndustry").hide();
            }
        });

        $("#industryName").blur(function () {
            if ($(this).val() == "") {
                $("#saveIndustry").hide();
            } else {
                var ele = $("#industry option[data-name='" + $(this).val() + "']");
                if (ele.length > 0) {
                    ele.prop("selected", "selected");
                    $("#otherIndustry").hide();
                } else {
                    $("#saveIndustry").show();
                }
            }
        });
        //添加行业
        $("#saveIndustry").click(function () {
            var name = $("#industryName").val();
            Global.post("/Client/CreateIndustry", { name: name }, function (data) {
                if (data.ID) {
                    var option = "<option value=\"" + data.ID + "\" selected=\"selected\" data-name=\"" + name + "\">" + name + "</option>";
                    $("#industry").prepend(option);
                    $("#otherIndustry").hide();
                }
            })
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