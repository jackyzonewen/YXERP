

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
        //行业为空
        if ($("#industry option").length == 1) $("#industry").change();
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
            if ($(this).val() == "") {
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
        //保存行业
        $("#saveIndustry").click(function () {
            var name = $("#industryName").val();
            Global.post("/Client/CreateIndustry", { name: name }, function (data) {
                if (data.ID) {
                    var option = "<option value=\"" + data.ID + "\" selected=\"selected\" data-name=\"" + name + "\">" + name + "</option>";
                    $("#industry").prepend(option);
                    $("#otherIndustry").hide();
                }
            });
        });
        //判断账号是否存在
        $("#loginName").blur(function () {
            var value = $(this).val();
            if (!value) {
                return;
            }
            Global.post("/Client/IsExistLoginName", { loginName: value }, function (data) {
                if (data.Result) {
                    $("#loginName").val("");
                    alert("登录账号已存在!");
                }
            });
        });
        //保存客户端
        $("#saveClient").click(function () {
            if (!VerifyObject.isPass()) {
                return false;
            };
            if ($("#industry").val() == "") {
                $("#industryName").css("borderColor", "red");
                return false;
            }
            var modules = [];
            $(".modules-item").each(function () {
                var _this = $(this);
                if (_this.hasClass("active")) {
                    modules.push({
                        ModulesID: _this.data("value")
                    });
                }
            });
            var client = {
                CompanyName: $("#name").val(),
                ContactName: $("#contact").val(),
                MobilePhone: $("#mobile").val(),
                Industry: $("#industry").val(),
                CityCode: CityObject.getCityCode(),
                Address: $("#address").val(),
                Description: $("#description").val(),
                Modules: modules
            };

            Global.post("/Client/CreateClient", { client: JSON.stringify(client), loginName: $("#loginName").val() }, function (data) {
                if (data.Result == "1") {
                    location.href = "/Client/Index";
                }else if (data.Result == "2") {
                    alert("登陆账号已存在!");
                    $("#loginName").val("");
                }
            })
        });
    }

    module.exports = Clients;
});